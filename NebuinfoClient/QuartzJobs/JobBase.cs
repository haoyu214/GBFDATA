using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Grpc.Core;

namespace GreeterClient.QuartzJobs
{
    public abstract class JobBase : IJob
    {

        public static string _ip = string.Empty;
        public static readonly object _jobs = new object();

        protected log4net.ILog Logger
        {
            get { return log4net.LogManager.GetLogger(this.GetType()); }
        }

        private static Channel _instanceChanel;


        public static Channel InstanceChannel()
        {
            if (_instanceChanel == null)
            {
                lock (_jobs)
                {

                    if (_instanceChanel == null)
                    {
                        var ssl = new SslCredentials();
                        var channOptions = new List<ChannelOption> { };
                        _instanceChanel = new Channel(_ip, ssl, channOptions);//需要证书的
                        return _instanceChanel;
                    }

                }
            }
            return _instanceChanel;
        }


        /// <summary>
        /// 基类去实现接口，由派生类处理
        /// 这个方法里定义算法的骨架，而核心的ExcuteJob抽象方法，由各个具体类去实现
        /// </summary>
        /// <param name="context"></param>
        public void Execute(Quartz.IJobExecutionContext context)
        {
            //操作 一些公用的方法
            //    foreach (var item in context.JobDetail.JobDataMap)
            //    {
            //        Console.WriteLine("{0}.JobDataMap   key={1},value={2}", context.JobDetail.Key.Name, item.Key, item.Value);
            //    }
            ExcuteJob(context);
        }

        /// <summary>
        /// 具体业务层的JOB
        /// 派生类实现这个方法，这类似于模版方法里的具体方法
        /// </summary>
        protected abstract void ExcuteJob(Quartz.IJobExecutionContext context);


    }
}
