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
namespace WWBData
{
    public partial class Service1 : ServiceBase
    {
       string  ip = ConfigurationSettings.AppSettings["ip"].ToString();
        public Service1()
        {
            InitializeComponent();


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimedEvent);
            timer.Interval = 1000*24*60*60;//每一天执行一次
          
            timer.Enabled = true;
            Run();

        }

        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        protected override void OnStart(string[] args)
        {
            SqlHelper.Write(DateTime.Now.ToString()+":服务开始启动");
            //System.Diagnostics.Debug.WriteLine("服务开始启动");
        }
        //定时执行事件
        private void TimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            //业务逻辑代码

            Run();


        }


        public void Run()
        {
            try
            {
                SqlHelper.Write(DateTime.Now.ToString()+":开始处理数据");
                Channel channel = new Channel(ip, ChannelCredentials.Insecure);

                var client = new Place.Place.PlaceClient(channel);                         


                var reply = client.GetPlaceList(new Place.PlaceListReq() { });
                // Console.WriteLine("Greeting: " + reply.PlaceList);

                var list = JsonConvert.DeserializeObject<List<Company>>(reply.PlaceList.ToString());

                SqlHelper.Delete();
                SqlHelper.SqlBulkCopyByDatatable("tCompany", ToDataTableTow(list));



            }
            catch (Exception ex)
            {

                SqlHelper.Write(DateTime.Now.ToString() + "系统出错"+ex.Message);
            }
        }
        protected override void OnStop()
        {
            SqlHelper.Write(DateTime.Now.ToString() + ":服务开始停止");
            System.Diagnostics.Debug.WriteLine("服务开始停止");
        }
    }
}
