using System;
using System.Collections.Generic;
using System.Web;

namespace LDLR.Core.OnlinePay.WeixinJSApi
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}