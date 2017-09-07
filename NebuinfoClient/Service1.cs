using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Grpc.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Collections;
using Quartz;
using System.Collections.Specialized;
namespace GreeterClient
{
    public partial class Service1 : ServiceBase
    {
        private log4net.ILog Logger;
        private static IScheduler scheduler = null;
        public Service1()
        {
            InitializeComponent();
            Logger = log4net.LogManager.GetLogger(this.GetType());//得到当前类类型（当前实实例化的类为具体子类）
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4Net.config"));
       
        }

        public void Run()
        {
            OnStart(null);
        }
     

        protected override void OnStart(string[] args)
        {
            Logger.Info("服务开始启动");
            #region job配置
            NameValueCollection properties = new NameValueCollection();
            // 远程输出配置
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "666";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";
            properties["quartz.scheduler.instanceName"] = "DataCenterInstance";

            // set thread pool info
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "2";//单个任务所对应的线程，为1表示只有一个线程工作，这样避免多个线程改同一条数据
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // job initialization plugin handles our xml reading, without it defaults are used
            properties["quartz.plugin.xml.type"] = "Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin, Quartz";
            properties["quartz.plugin.xml.fileNames"] = System.AppDomain.CurrentDomain.BaseDirectory + "quartz_jobs.xml";
            ISchedulerFactory sf = new Quartz.Impl.StdSchedulerFactory(properties);
            scheduler = sf.GetScheduler();
            scheduler.Start();
            #endregion
        }
        //定时执行事件





        protected override void OnStop()
        {
            Logger.Info("停止服务");
        }
    }
}
