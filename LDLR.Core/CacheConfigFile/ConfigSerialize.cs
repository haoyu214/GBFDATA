using System.IO;
using System.Xml.Serialization;
using System;

namespace LDLR.Core.CacheConfigFile
{
    /// <summary>
    /// �������л�������
    /// </summary>
    internal class ConfigSerialize
    {
        #region �����л�ָ������

        /// <summary>
        /// �����л�ָ������
        /// </summary>
        /// <param name="configfilepath">config �ļ���·��</param>
        /// <param name="configtype">��Ӧ������</param>
        /// <returns></returns>
        public static IConfiger DeserializeInfo(string path, Type type)
        {

            IConfiger iconfiginfo;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                iconfiginfo = (IConfiger)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            return iconfiginfo;
        }



        #endregion

        #region ����(���л�)ָ��·���µ������ļ�

        /// <summary>
        /// ����(���л�)ָ��·���µ������ļ�
        /// </summary>
        /// <param name="configFilePath">ָ���������ļ����ڵ�·��(�����ļ���)</param>
        /// <param name="configinfo">������(���л�)�Ķ���</param>
        /// <returns></returns>
        public static bool Serializer(string path, IConfiger Iconfiginfo)
        {
            bool succeed = false;
            FileStream fs = null;
            XmlSerializer serializer = null;
            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                serializer = new XmlSerializer(Iconfiginfo.GetType());
                serializer.Serialize(fs, Iconfiginfo);
                //�ɹ��򽫻᷵��true
                succeed = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    serializer = null;
                }
            }

            return succeed;
        }

        #endregion
    
    }
}
