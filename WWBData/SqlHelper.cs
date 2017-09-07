using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
namespace WWBData
{
   public class SqlHelper
    {
        public static string connnectionString = ConfigurationSettings.AppSettings["constr"].ToString();

        public static void SqlBulkCopyByDatatable( string TableName, DataTable dt)
        {

            using (SqlConnection conn = new SqlConnection(connnectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connnectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dt);
                    }
                    catch (System.Exception ex)
                    {
                        SqlHelper.Write(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        public static void Write(string msg)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"log.txt", FileMode.Append);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(msg+"\r\n");
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
        public static void Delete()
        {
            using (SqlConnection conn = new SqlConnection(connnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "truncate table [dbo].[tCompany]";
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
