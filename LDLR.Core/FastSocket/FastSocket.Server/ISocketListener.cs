﻿using System;
using System.Net;

namespace LDLR.Core.FastSocket.Server
{
    /// <summary>
    /// socket listener
    /// </summary>
    public interface ISocketListener
    {
        /// <summary>
        /// socket accepted event
        /// </summary>
        event Action<ISocketListener, SocketBase.IConnection> Accepted;

        /// <summary>
        /// get name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// get endpoint
        /// </summary>
        EndPoint EndPoint { get; }
        /// <summary>
        /// start listen
        /// </summary>
        void Start();
        /// <summary>
        /// stop listen
        /// </summary>
        void Stop();
    }
}