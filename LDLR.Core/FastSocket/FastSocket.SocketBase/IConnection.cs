﻿using System;
using System.Net;

namespace LDLR.Core.FastSocket.SocketBase
{
    /// <summary>
    /// a connection interface.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// packet start sending event
        /// </summary>
        event StartSendingHandler StartSending;
        /// <summary>
        /// packet send callback event
        /// </summary>
        event SendCallbackHandler SendCallback;
        /// <summary>
        /// message received event
        /// </summary>
        event MessageReceivedHandler MessageReceived;
        /// <summary>
        /// disconnected event
        /// </summary>
        event DisconnectedHandler Disconnected;
        /// <summary>
        /// connection error event
        /// </summary>
        event ErrorHandler Error;

        /// <summary>
        /// return the connection is active.
        /// </summary>
        bool Active { get; }
        /// <summary>
        /// get the connection id.
        /// </summary>
        long ConnectionID { get; }
        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        IPEndPoint LocalEndPoint { get; }
        /// <summary>
        /// 获取远程IP地址
        /// </summary>
        IPEndPoint RemoteEndPoint { get; }
        /// <summary>
        /// 获取或设置与用户数据
        /// </summary>
        object UserData { get; set; }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="packet"></param>
        void BeginSend(Packet packet);
        /// <summary>
        /// 异步接收数据
        /// </summary>
        void BeginReceive();
        /// <summary>
        /// 异步断开连接
        /// </summary>
         
        void BeginDisconnect(Exception ex = null);
    }
}