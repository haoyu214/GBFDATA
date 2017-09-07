
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
            //    //Channel channel = new Channel("192.168.30.178:8554", ChannelCredentials.Insecure);

            //    //var client = new Place.Place.PlaceClient(channel);
            //    //String user = "you";

            //    //var reply = client.GetPlaceList(new Place.PlaceListReq() { });
            //    //Console.WriteLine("Greeting: " + reply.PlaceList);

            //    //var list = JsonConvert.DeserializeObject<List<Company>>(reply.PlaceList.ToString());


            //    //channel.ShutdownAsync().Wait();
            //    //Console.WriteLine("Press any key to exit...");
            //    //Console.ReadKey();

            Service1 s = new Service1();
            s.Run();

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);


        }
    }
}
