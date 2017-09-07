using LDLR.Core.OnlinePay.Weixin.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDLR.Core.OnlinePay.Weixin
{
    public class TestWxPayClient : WxPayClient
    {
        public TestWxPayClient(PaymentConfig config)
        {
            PaymentConfig.Init(config);
        }

        public string TestToShowOrderRequestXml(UnifiedOrderParam param)
        {
            UnifiedOrderClient unifiedOrder = new UnifiedOrderClient();
            return unifiedOrder.TestToShowXml(param);
        }
    }
}
