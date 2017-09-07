using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsServicetest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);

            var k=new Test().YieldDemo();

            foreach (var m in k)
            {
                Console.Write(m);
            }
            Console.ReadLine();
        }

       


    }

    public class Test
    {
        public IEnumerable<int> YieldDemo()
        {
            int counter = 0;
            int result = 1;
            while (counter++ < 10)
            {
                result = result * 2;
                yield return result;
            }
        }
    }



}
