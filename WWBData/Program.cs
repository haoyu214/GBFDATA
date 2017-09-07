
using System;
using Grpc.Core;
using Helloworld;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.ServiceProcess;
namespace WWBData
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);

            //Service1 s = new Service1();
            //s.Run();
        }
    }
}
