using System;
using Grpc.Core;
using Helloworld;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Collections.Generic;
using System.ServiceProcess;

namespace GreeterClient
{
    class Program
    {
        public static void Main(string[] args)
        {
          
            Service1 s = new Service1();
            s.Run();

           

        }
    }
}
