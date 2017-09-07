using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;
using System.Configuration;
using Grpc.Core;
using Newtonsoft.Json;
using GreeterClient.DB;
using System.Threading;
using LDLR.Core;
using LDLR.Core.IoC;
using LDLR.Core.IRepositories;
namespace GreeterClient.QuartzJobs
{
    public class PlaceJob : JobBase
    {
       GreetbEfRepository<Company> db = new GreetbEfRepository<Company>();
        //IRepository<Company> db= ServiceLocator.Instance.GetService<IRepository<Company>>();
     //来测试一下啊
        private static readonly object Com = new object();
        protected override void ExcuteJob(IJobExecutionContext context)
        {
            try
            {
                lock (Com)
                {  
                    Logger.Info("开始营业场所信息处理");
                    _ip = ConfigurationSettings.AppSettings["ip"].ToString();
                    var client = new Place.Place.PlaceClient(InstanceChannel());
                    var reply = client.GetPlaceList(new Place.PlaceListReq() { });
                    var list = JsonConvert.DeserializeObject<List<Company>>(reply.PlaceList.ToString());
                 //   GreetbEfRepository<Company> gd =(GreetbEfRepository<Company>)db;
                    db.DeleteAll();
                    db.BulkInsert(list);
                    Logger.Info("营业场所信息处理完毕");

                }
            }
            catch (Exception exp)
            {
                Logger.Info("同步营业场所失败" + exp.Message);
            }

        }
    }
}
