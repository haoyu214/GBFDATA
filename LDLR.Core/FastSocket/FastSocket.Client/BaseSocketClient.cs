﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using LDLR.Core.FastSocket.SocketBase;

namespace LDLR.Core.FastSocket.Client
{
    /// <summary>
    /// socket client
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class BaseSocketClient<TResponse> : SocketBase.BaseHost where TResponse : class, Response.IResponse
    {
        #region Private Members
        private int _requestSeqId = 0;
        private readonly Protocol.IProtocol<TResponse> _protocol = null;

        private readonly int _millisecondsSendTimeout;
        private readonly int _millisecondsReceiveTimeout;

        private readonly PendingSendQueue _pendingQueue = null;
        private readonly RequestCollection _requestCollection = null;
        #endregion

        #region Constructors
        /// <summary>
        /// new
        /// </summary>
        /// <param name="protocol"></param>
        public BaseSocketClient(Protocol.IProtocol<TResponse> protocol)
            : this(protocol, 8192, 8192, 3000, 3000)
        {
        }
        /// <summary>
        /// new
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="socketBufferSize"></param>
        /// <param name="messageBufferSize"></param>
        /// <param name="millisecondsSendTimeout"></param>
        /// <param name="millisecondsReceiveTimeout"></param>
        /// <exception cref="ArgumentNullException">protocol is null</exception>
        public BaseSocketClient(Protocol.IProtocol<TResponse> protocol,
            int socketBufferSize,
            int messageBufferSize,
            int millisecondsSendTimeout,
            int millisecondsReceiveTimeout)
            : base(socketBufferSize, messageBufferSize)
        {
            if (protocol == null) throw new ArgumentNullException("protocol");
            this._protocol = protocol;

            this._millisecondsSendTimeout = millisecondsSendTimeout;
            this._millisecondsReceiveTimeout = millisecondsReceiveTimeout;

            this._pendingQueue = new PendingSendQueue(this, millisecondsSendTimeout);
            this._requestCollection = new RequestCollection(this, millisecondsReceiveTimeout);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 发送超时毫秒数
        /// </summary>
        public int MillisecondsSendTimeout
        {
            get { return this._millisecondsSendTimeout; }
        }
        /// <summary>
        /// 接收超时毫秒数
        /// </summary>
        public int MillisecondsReceiveTimeout
        {
            get { return this._millisecondsReceiveTimeout; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// 处理未知的response
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="response"></param>
        protected virtual void HandleUnknowResponse(IConnection connection, TResponse response)
        {
        }
        /// <summary>
        /// OnResponse
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="response"></param>
        protected virtual void OnResponse(IConnection connection, TResponse response)
        {
        }
        /// <summary>
        /// on request send success
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="request"></param>
        protected virtual void OnSendSucess(IConnection connection, Request<TResponse> request)
        {
        }
        /// <summary>
        /// on request send failed
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="request"></param>
        protected virtual void OnSendFailed(IConnection connection, Request<TResponse> request)
        {
            this.Send(request);
        }
        /// <summary>
        /// on request send timeout
        /// </summary>
        /// <param name="request"></param>
        protected virtual void OnSendTimeout(Request<TResponse> request)
        {
        }
        /// <summary>
        /// on request receive timeout
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="request"></param>
        protected virtual void OnReceiveTimeout(IConnection connection, Request<TResponse> request)
        {
        }
        /// <summary>
        /// send request
        /// </summary>
        /// <param name="request"></param>
        protected abstract void Send(Request<TResponse> request);
        /// <summary>
        /// enqueue to pending queue
        /// </summary>
        /// <param name="request"></param>
        protected void EnqueueToPendingQueue(Request<TResponse> request)
        {
            this._pendingQueue.Enqueue(request);
        }
        /// <summary>
        /// dequeue from pending queue
        /// </summary>
        /// <returns></returns>
        protected Request<TResponse> DequeueFromPendingQueue()
        {
            return this._pendingQueue.Dequeue();
        }
        /// <summary>
        /// dequeue all from pending queue.
        /// </summary>
        /// <returns></returns>
        protected Request<TResponse>[] DequeueAllFromPendingQueue()
        {
            return this._pendingQueue.DequeueAll();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 产生不重复的seqID
        /// </summary>
        /// <returns></returns>
        public int NextRequestSeqID()
        {
            return Interlocked.Increment(ref this._requestSeqId) & 0x7fffffff;
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// OnConnected
        /// </summary>
        /// <param name="connection"></param>
        protected override void OnConnected(IConnection connection)
        {
            base.OnConnected(connection);
            connection.BeginReceive();//异步开始接收数据
        }
        /// <summary>
        /// OnStartSending
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packet"></param>
        protected override void OnStartSending(IConnection connection, Packet packet)
        {
            base.OnStartSending(connection, packet);
            var request = packet as Request<TResponse>;
            if (request != null) this._requestCollection.Add(request);
        }
        /// <summary>
        /// OnSendCallback
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="e"></param>
        protected override void OnSendCallback(IConnection connection, SendCallbackEventArgs e)
        {
            base.OnSendCallback(connection, e);

            var request = e.Packet as Request<TResponse>;
            if (request == null) return;

            if (e.Status == SendCallbackStatus.Success)
            {
                request.CurrConnection = connection;
                request.SentTime = DateTime.UtcNow;
                this.OnSendSucess(connection, request);
                return;
            }

            request.CurrConnection = null;
            request.SentTime = DateTime.MaxValue;
            if (this._requestCollection.Remove(request.SeqID) == null) return;

            if (DateTime.UtcNow.Subtract(request.BeginTime).TotalMilliseconds < this._millisecondsSendTimeout)
            {
                this.OnSendFailed(connection, request);
                return;
            }

            //time out
            this.OnSendTimeout(request);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                var rex = new RequestException(RequestException.Errors.PendingSendTimeout, request.CmdName);
                try { request.SetException(rex); }
                catch (Exception ex) { SocketBase.Log.Trace.Error(ex.Message, ex); }
            });
        }
        /// <summary>
        /// OnMessageReceived
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="e"></param>
        protected override void OnMessageReceived(IConnection connection, MessageReceivedEventArgs e)
        {
            base.OnMessageReceived(connection, e);

            int readlength;
            TResponse response = null;
            try
            {
                response = this._protocol.FindResponse(connection, e.Buffer, out readlength);
            }
            catch (Exception ex)
            {
                this.OnError(connection, ex);
                connection.BeginDisconnect(ex);
                e.SetReadlength(e.Buffer.Count);
                return;
            }

            if (response != null)
            {
                this.OnResponse(connection, response);

                var request = this._requestCollection.Remove(response.SeqID);
                if (request == null)
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try { this.HandleUnknowResponse(connection, response); }
                        catch (Exception ex) { SocketBase.Log.Trace.Error(ex.Message, ex); }
                    });
                else
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try { request.SetResult(response); }
                        catch (Exception ex) { SocketBase.Log.Trace.Error(ex.Message, ex); }
                    });
            }
            e.SetReadlength(readlength);
        }
        /// <summary>
        /// OnDisconnected
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        protected override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);

            var arrRemoved = this._requestCollection.Remove(connection);
            if (arrRemoved.Length == 0) return;

            var ex2 = ex ?? new SocketException((int)SocketError.Disconnecting);
            for (int i = 0, l = arrRemoved.Length; i < l; i++)
            {
                var r = arrRemoved[i]; if (r == null) continue;
                ThreadPool.QueueUserWorkItem(c =>
                {
                    try { r.SetException(ex2); }
                    catch (Exception ex3) { SocketBase.Log.Trace.Error(ex.Message, ex3); }
                });
            }
        }
        #endregion

        #region Class.PendingSendQueue
        /// <summary>
        /// pending send queue
        /// </summary>
        private class PendingSendQueue
        {
            #region Private Members
            private readonly BaseSocketClient<TResponse> _client = null;

            private readonly int _timeout;
            private readonly Timer _timer = null;
            private readonly ConcurrentQueue<Request<TResponse>> _queue = new ConcurrentQueue<Request<TResponse>>();
            #endregion

            #region Constructors
            /// <summary>
            /// new
            /// </summary>
            ~PendingSendQueue()
            {
                this._timer.Change(Timeout.Infinite, Timeout.Infinite);
                this._timer.Dispose();
            }
            /// <summary>
            /// new
            /// </summary>
            /// <param name="client"></param>
            /// <param name="millisecondsSendTimeout"></param>
            public PendingSendQueue(BaseSocketClient<TResponse> client, int millisecondsSendTimeout)
            {
                this._client = client;
                this._timeout = millisecondsSendTimeout;
                this._timer = new Timer(_ =>
                {
                    this._timer.Change(Timeout.Infinite, Timeout.Infinite);
                    this.Loop();
                    this._timer.Change(1000, 0);
                }, null, 1000, 0);
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// 入列
            /// </summary>
            /// <param name="request"></param>
            /// <exception cref="ArgumentNullException">request is null</exception>
            public void Enqueue(Request<TResponse> request)
            {
                if (request == null) throw new ArgumentNullException("request");
                this._queue.Enqueue(request);
            }
            /// <summary>
            /// dequeue
            /// </summary>
            /// <returns></returns>
            public Request<TResponse> Dequeue()
            {
                Request<TResponse> request;
                if (this._queue.TryDequeue(out request)) return request;
                return null;
            }
            /// <summary>
            /// 出列全部
            /// </summary>
            /// <returns></returns>
            public Request<TResponse>[] DequeueAll()
            {
                int count = this._queue.Count;
                List<Request<TResponse>> list = null;
                while (count-- > 0)
                {
                    Request<TResponse> request;
                    if (this._queue.TryDequeue(out request))
                    {
                        if (list == null) list = new List<Request<TResponse>>();
                        list.Add(request);
                    }
                    else break;
                }

                if (list != null) return list.ToArray();
                return new Request<TResponse>[0];
            }
            #endregion

            #region Private Methods
            /// <summary>
            /// loop
            /// </summary>
            private void Loop()
            {
                var dtNow = DateTime.UtcNow;
                List<Request<TResponse>> listSend = null;
                List<Request<TResponse>> listTimeout = null;

                int count = this._queue.Count;
                while (count-- > 0)
                {
                    Request<TResponse> request;
                    if (this._queue.TryDequeue(out request))
                    {
                        if (dtNow.Subtract(request.BeginTime).TotalMilliseconds < this._timeout)
                        {
                            if (listSend == null) listSend = new List<Request<TResponse>>();
                            listSend.Add(request); continue;
                        }

                        if (listTimeout == null) listTimeout = new List<Request<TResponse>>();
                        listTimeout.Add(request);
                    }
                    else break;
                }

                if (listSend != null)
                {
                    for (int i = 0, l = listSend.Count; i < l; i++) this._client.Send(listSend[i]);
                }

                if (listTimeout != null)
                {
                    for (int i = 0, l = listTimeout.Count; i < l; i++)
                    {
                        var r = listTimeout[i];
                        this._client.OnSendTimeout(r);
                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            try { r.SetException(new RequestException(RequestException.Errors.PendingSendTimeout, r.CmdName)); }
                            catch (Exception ex) { SocketBase.Log.Trace.Error(ex.Message, ex); }
                        });
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Class.RequestCollection
        /// <summary>
        /// request collection
        /// </summary>
        private class RequestCollection
        {
            #region Private Members
            private readonly BaseSocketClient<TResponse> _client = null;

            private readonly int _timeout;
            private readonly Timer _timer = null;
            private readonly ConcurrentDictionary<int, Request<TResponse>> _dic = new ConcurrentDictionary<int, Request<TResponse>>();
            #endregion

            #region Constructors
            /// <summary>
            /// new
            /// </summary>
            ~RequestCollection()
            {
                this._timer.Change(Timeout.Infinite, Timeout.Infinite);
                this._timer.Dispose();
            }
            /// <summary>
            /// new
            /// </summary>
            /// <param name="client"></param>
            /// <param name="millisecondsReceiveTimeout"></param>
            public RequestCollection(BaseSocketClient<TResponse> client, int millisecondsReceiveTimeout)
            {
                this._client = client;
                this._timeout = millisecondsReceiveTimeout;

                this._timer = new Timer(_ =>
                {
                    this._timer.Change(Timeout.Infinite, Timeout.Infinite);
                    this.Loop();
                    this._timer.Change(1000, 0);
                }, null, 1000, 0);
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// add
            /// </summary>
            /// <param name="request"></param>
            public void Add(Request<TResponse> request)
            {
                this._dic.TryAdd(request.SeqID, request);
            }
            /// <summary>
            /// remove
            /// </summary>
            /// <param name="seqID"></param>
            /// <returns></returns>
            public Request<TResponse> Remove(int seqID)
            {
                Request<TResponse> removed;
                this._dic.TryRemove(seqID, out removed);
                return removed;
            }
            /// <summary>
            /// clear
            /// </summary>
            /// <param name="connection"></param>
            /// <returns></returns>
            public Request<TResponse>[] Remove(IConnection connection)
            {
                var items = this._dic.Where(c => c.Value.CurrConnection == connection).ToArray();
                var arrRemoved = new Request<TResponse>[items.Length];
                for (int i = 0, l = items.Length; i < l; i++)
                {
                    Request<TResponse> removed;
                    if (this._dic.TryRemove(items[i].Key, out removed)) arrRemoved[i] = removed;
                }
                return arrRemoved;
            }
            #endregion

            #region Private Methods
            /// <summary>
            /// loop
            /// </summary>
            private void Loop()
            {
                if (this._dic.Count == 0) return;

                var dtNow = DateTime.UtcNow;
                var arrTimeout = this._dic.Where(c => dtNow.Subtract(c.Value.SentTime).TotalMilliseconds >
                    (c.Value.MillisecondsReceiveTimeout > 0 ? c.Value.MillisecondsReceiveTimeout : this._timeout)).ToArray();
                if (arrTimeout.Length == 0) return;

                for (int i = 0, l = arrTimeout.Length; i < l; i++)
                {
                    Request<TResponse> removed;
                    if (this._dic.TryRemove(arrTimeout[i].Key, out removed))
                    {
                        this._client.OnReceiveTimeout(removed.CurrConnection, removed);

                        ThreadPool.QueueUserWorkItem(_ =>
                        {
                            try { removed.SetException(new RequestException(RequestException.Errors.ReceiveTimeout, removed.CmdName)); }
                            catch (Exception ex) { SocketBase.Log.Trace.Error(ex.Message, ex); }
                        });
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}