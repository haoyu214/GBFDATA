﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDLR.Core.Events
{
    /// <summary>
    /// 异步事件处理
    /// Represents that the event handlers applied with this attribute
    /// will handle the events in a asynchronous process.
    /// </summary>
    /// <remarks>This attribute is only applicable to the message handlers and will only
    /// be used by the message buses or message dispatchers. Applying this attribute to
    /// other types of classes will take no effect.</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesAsynchronouslyAttribute : Attribute
    {

    }
}
