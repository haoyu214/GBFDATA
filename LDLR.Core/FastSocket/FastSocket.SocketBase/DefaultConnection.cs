﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LDLR.Core.FastSocket.SocketBase
{
    /// <summary>
    /// default socket connection
    /// </summary>
    public class DefaultConnection : IConnection
    {
        #region Private Members
        private int _active = 1;//1表示有效
        private IHost _host = null;
        private readonly int _messageBufferSize;

        private Socket _socket = null;

        private SocketAsyncEventArgs _saeSend = null;
        private SendQueue _sendQueue = null;
        private Packet _currSendingPacket = null;

        private SocketAsyncEventArgs _saeReceive = null;
        private MemoryStream _tsStream = null;//temporary storage stream for recieving
        private int _isReceiving = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// new
        /// </summary>
        /// <param name="connectionID"></param>
        /// <param name="socket"></param>
        /// <param name="host"></param>
        /// <exception cref="ArgumentNullException">socket is null</exception>
        /// <exception cref="ArgumentNullException">host is null</exception>
        public DefaultConnection(long connectionID, Socket socket, IHost host)
        {
            if (socket == null) throw new ArgumentNullException("socket");
            if (host == null) throw new ArgumentNullException("host");

            this.ConnectionID = connectionID;
            this._socket = socket;
            this._host = host;
            this._messageBufferSize = host.MessageBufferSize;

            try//fuck...
            {
                this.LocalEndPoint = (IPEndPoint)socket.LocalEndPoint;
                this.RemoteEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            }
            catch (Exception ex) { Log.Trace.Error("get socket endPoint error.", ex); }

            //init for send...
            this._saeSend = host.GetSocketAsyncEventArgs();
            this._saeSend.Completed += new EventHandler<SocketAsyncEventArgs>(this.SendAsyncCompleted);
            this._sendQueue = new SendQueue();

            //init for receive...
            this._saeReceive = host.GetSocketAsyncEventArgs();
            this._saeReceive.Completed += new EventHandler<SocketAsyncEventArgs>(this.ReceiveAsyncCompleted);
        }
        #endregion

        #region IConnection Members
        /// <summary>
        /// packet start sending event
        /// </summary>
        public event StartSendingHandler StartSending;
        /// <summary>
        /// packet send callback event
        /// </summary>
        public event SendCallbackHandler SendCallback;
        /// <summary>
        /// message received event
        /// </summary>
        public event MessageReceivedHandler MessageReceived;
        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event DisconnectedHandler Disconnected;
        /// <summary>
        /// connection error event
        /// </summary>
        public event ErrorHandler Error;

        /// <summary>
        /// return the connection is active.
        /// </summary>
        public bool Active { get { return Thread.VolatileRead(ref this._active) == 1; } }
        /// <summary>
        /// get the connection id.
        /// </summary>
        public long ConnectionID { get; private set; }
        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        public IPEndPoint LocalEndPoint { get; private set; }
        /// <summary>
        /// 获取远程IP地址
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; private set; }
        /// <summary>
        /// 获取或设置与用户数据
        /// </summary>
        public object UserData { get; set; }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="packet"></param>
        public void BeginSend(Packet packet)
        {
            this.SendPacketInternal(packet);
        }
        /// <summary>
        /// 异步接收数据
        /// </summary>
        public void BeginReceive()
        {
            if (Interlocked.CompareExchange(ref this._isReceiving, 1, 0) == 0) this.ReceiveInternal(this._saeReceive);
        }
        /// <summary>
        /// 异步断开连接
        /// </summary>
        /// <param name="ex"></param>
        public void BeginDisconnect(Exception ex = null)
        {
            if (Interlocked.CompareExchange(ref this._active, 0, 1) == 1) this.DisconnectInternal(ex);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// dispose
        /// </summary>
        protected virtual void Free()
        {
            var arrPacket = this._sendQueue.Close();
            this._sendQueue = null;
            if (arrPacket != null && arrPacket.Length > 0)
            {
                foreach (var packet in arrPacket)
                    this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Failed));
            }

            this._saeSend.Completed -= new EventHandler<SocketAsyncEventArgs>(this.SendAsyncCompleted);
            this._saeSend.UserToken = null;
            this._host.ReleaseSocketAsyncEventArgs(this._saeSend);
            this._saeSend = null;

            this._saeReceive.Completed -= new EventHandler<SocketAsyncEventArgs>(this.ReceiveAsyncCompleted);
            this._saeReceive.UserToken = null;
            this._host.ReleaseSocketAsyncEventArgs(this._saeReceive);
            this._saeReceive = null;

            this._socket = null;
            this._host = null;
        }
        #endregion

        #region Private Methods

        #region Fire Events
        /// <summary>
        /// fire StartSending
        /// </summary>
        /// <param name="packet"></param>
        private void OnStartSending(Packet packet)
        {
            if (this.StartSending != null) this.StartSending(this, packet);
        }
        /// <summary>
        /// fire SendCallback
        /// </summary>
        /// <param name="e"></param>
        private void OnSendCallback(SendCallbackEventArgs e)
        {
            if (e.Status != SendCallbackStatus.Success) e.Packet.SentSize = 0;
            if (this.SendCallback != null) this.SendCallback(this, e);
        }
        /// <summary>
        /// fire MessageReceived
        /// </summary>
        /// <param name="e"></param>
        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            if (this.MessageReceived != null) this.MessageReceived(this, e);
        }
        /// <summary>
        /// fire Disconnected
        /// </summary>
        private void OnDisconnected(Exception ex)
        {
            if (this.Disconnected != null) this.Disconnected(this, ex);
        }
        /// <summary>
        /// fire Error
        /// </summary>
        /// <param name="ex"></param>
        private void OnError(Exception ex)
        {
            if (this.Error != null) this.Error(this, ex);
        }
        #endregion

        #region Send
        /// <summary>
        /// internal send packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <exception cref="ArgumentNullException">packet is null</exception>
        private void SendPacketInternal(Packet packet)
        {
            var e = this._saeSend;
            var queue = this._sendQueue;
            if (!this.Active || e == null || queue == null)
            {
                this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Failed)); return;
            }

            switch (queue.TrySend(packet))
            {
                case SendResult.Closed:
                    this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Failed)); break;
                case SendResult.SendCurr:
                    this.OnStartSending(packet);
                    this.SendPacketInternal(packet, e);
                    break;
            }
        }
        /// <summary>
        /// internal send packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="e"></param>
        private void SendPacketInternal(Packet packet, SocketAsyncEventArgs e)
        {
            this._currSendingPacket = packet;

            //按_messageBufferSize大小分块传输
            var length = Math.Min(packet.Payload.Length - packet.SentSize, this._messageBufferSize);

            var completedAsync = true;
            try
            {
                //copy data to send buffer
                Buffer.BlockCopy(packet.Payload, packet.SentSize, e.Buffer, 0, length);
                e.SetBuffer(0, length);
                completedAsync = this._socket.SendAsync(e);
            }
            catch (Exception ex)
            {
                this.BeginDisconnect(ex);
                this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Failed));
                this.OnError(ex);
            }

            if (!completedAsync) this.SendAsyncCompleted(this, e);
        }
        /// <summary>
        /// async send callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            var packet = this._currSendingPacket;
            if (packet == null)
            {
                var ex = new Exception(string.Concat("未知的错误, connection state:",
                    this.Active.ToString(),
                    " conectionID:",
                    this.ConnectionID.ToString(),
                    " remote address:",
                    this.RemoteEndPoint.ToString()));
                this.OnError(ex);
                this.BeginDisconnect(ex);
                return;
            }

            //send error!
            if (e.SocketError != SocketError.Success)
            {
                this.BeginDisconnect(new SocketException((int)e.SocketError));
                this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Failed));
                return;
            }

            packet.SentSize += e.BytesTransferred;

            if (e.Offset + e.BytesTransferred < e.Count)
            {
                //continue to send until all bytes are sent!
                var completedAsync = true;
                try
                {
                    e.SetBuffer(e.Offset + e.BytesTransferred, e.Count - e.BytesTransferred - e.Offset);
                    completedAsync = this._socket.SendAsync(e);
                }
                catch (Exception ex)
                {
                    this.BeginDisconnect(ex);
                    this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Failed));
                    this.OnError(ex);
                }

                if (!completedAsync) this.SendAsyncCompleted(sender, e);
            }
            else
            {
                if (packet.IsSent())
                {
                    this._currSendingPacket = null;
                    this.OnSendCallback(new SendCallbackEventArgs(packet, SendCallbackStatus.Success));

                    //send next packet
                    var queue = this._sendQueue;
                    if (this.Active && queue != null)
                    {
                        Packet nextPacket = queue.TrySendNext();
                        if (nextPacket != null)
                        {
                            this.OnStartSending(nextPacket);
                            this.SendPacketInternal(nextPacket, e);
                        }
                    }
                }
                else this.SendPacketInternal(packet, e);//continue send this packet
            }
        }
        #endregion

        #region Receive
        /// <summary>
        /// receive
        /// </summary>
        /// <param name="e"></param>
        private void ReceiveInternal(SocketAsyncEventArgs e)
        {
            if (!this.Active || e == null) return;

            bool completedAsync = true;
            try { completedAsync = this._socket.ReceiveAsync(e); }
            catch (Exception ex)
            {
                this.BeginDisconnect(ex);
                this.OnError(ex);
            }

            if (!completedAsync) this.ReceiveAsyncCompleted(this, e);
        }
        /// <summary>
        /// async receive callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceiveAsyncCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                this.BeginDisconnect(new SocketException((int)e.SocketError)); return;
            }
            if (e.BytesTransferred < 1)
            {
                this.BeginDisconnect(); return;
            }

            ArraySegment<byte> buffer;
            var ts = this._tsStream;
            if (ts == null || ts.Length == 0) buffer = new ArraySegment<byte>(e.Buffer, 0, e.BytesTransferred);
            else
            {
                ts.Write(e.Buffer, 0, e.BytesTransferred);
                buffer = new ArraySegment<byte>(ts.GetBuffer(), 0, (int)ts.Length);
            }

            this.OnMessageReceived(new MessageReceivedEventArgs(buffer, this.MessageProcessCallback));
        }
        /// <summary>
        /// message process callback
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="readlength"></param>
        /// <exception cref="ArgumentOutOfRangeException">readlength less than 0 or greater than payload.Count.</exception>
        private void MessageProcessCallback(ArraySegment<byte> payload, int readlength)
        {
            if (readlength < 0 || readlength > payload.Count)
                throw new ArgumentOutOfRangeException("readlength", "readlength less than 0 or greater than payload.Count.");

            var ts = this._tsStream;
            if (readlength == 0)
            {
                if (ts == null) this._tsStream = ts = new MemoryStream(this._messageBufferSize);
                else ts.SetLength(0);

                ts.Write(payload.Array, payload.Offset, payload.Count);
                this.ReceiveInternal(this._saeReceive);
                return;
            }

            if (readlength == payload.Count)
            {
                if (ts != null) ts.SetLength(0);
                this.ReceiveInternal(this._saeReceive);
                return;
            }

            //粘包处理
            this.OnMessageReceived(new MessageReceivedEventArgs(
                new ArraySegment<byte>(payload.Array, payload.Offset + readlength, payload.Count - readlength),
                this.MessageProcessCallback));
        }
        #endregion

        #region Disconnect
        /// <summary>
        /// disconnect
        /// </summary>
        private void DisconnectInternal(Exception ex)
        {
            try
            {
                this._socket.Shutdown(SocketShutdown.Both);
                this._socket.BeginDisconnect(false, this.DisconnectCallback, ex);
            }
            catch (Exception ex2)
            {
                Log.Trace.Error(ex2.Message, ex2);
                this.DisconnectCallback(null);
            }
        }
        /// <summary>
        /// disconnect callback
        /// </summary>
        /// <param name="result"></param>
        private void DisconnectCallback(IAsyncResult result)
        {
            if (result != null)
            {
                try
                {
                    this._socket.EndDisconnect(result);
                    this._socket.Close();
                }
                catch (Exception ex) { Log.Trace.Error(ex.Message, ex); }
            }

            //fire disconnected.
            this.OnDisconnected(result == null ? null : result.AsyncState as Exception);

            this.Free();
        }
        #endregion

        #endregion

        #region Private Class
        /// <summary>
        /// packet send queue
        /// </summary>
        private class SendQueue
        {
            #region Private Members
            private bool _isSending = false, _isClosed = false;
            private readonly Queue<Packet> _queue = new Queue<Packet>();
            #endregion

            #region Public Methods
            /// <summary>
            /// try send
            /// </summary>
            /// <param name="packet"></param>
            /// <returns></returns>
            public SendResult TrySend(Packet packet)
            {
                lock (this)
                {
                    if (this._isClosed) return SendResult.Closed;

                    if (this._isSending)
                    {
                        if (this._queue.Count < 500)
                        {
                            this._queue.Enqueue(packet);
                            return SendResult.Enqueued;
                        }
                    }
                    else
                    {
                        this._isSending = true;
                        return SendResult.SendCurr;
                    }
                }

                Thread.Sleep(1);
                return this.TrySend(packet);
            }
            /// <summary>
            /// try sned next packet
            /// </summary>
            /// <returns></returns>
            public Packet TrySendNext()
            {
                lock (this)
                {
                    if (this._queue.Count == 0)
                    {
                        this._isSending = false;
                        return null;
                    }

                    this._isSending = true;
                    return this._queue.Dequeue();
                }
            }
            /// <summary>
            /// close
            /// </summary>
            /// <returns></returns>
            public Packet[] Close()
            {
                lock (this)
                {
                    if (this._isClosed) return null;
                    this._isClosed = true;

                    var packets = this._queue.ToArray();
                    this._queue.Clear();
                    return packets;
                }
            }
            #endregion
        }

        /// <summary>
        /// send result
        /// </summary>
        private enum SendResult : byte
        {
            /// <summary>
            /// closed
            /// </summary>
            Closed = 1,
            /// <summary>
            /// send current
            /// </summary>
            SendCurr = 2,
            /// <summary>
            /// 已入列
            /// </summary>
            Enqueued = 3
        }
        #endregion
    }
}