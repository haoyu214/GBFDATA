using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDLR.Core.Logger.Implements
{
    internal class CatLogger : LoggerBase
    {
        protected override void InputLogger(string message)
        {
          //  PureCat.CatClient.NewEvent("CatLogger", message);
        }

       
    }
}
