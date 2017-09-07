using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace LDLR.Core.Utils
{
    /// <summary>
    /// ���л��뷴���л����ļ�
    /// </summary>
    public class SerializationHelper
    {
        private static object lockObj = new object();

        #region Binary
        /// <summary>
        /// ���������л�������
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializableToBinary(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                }
            }
        }
        /// <summary>
        /// �����Ʒ����л��Ӵ��̵��ڴ����
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static object DeserializeFromBinary(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        #endregion

        #region XML
        /// <summary>
        /// XML���������л��������ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializeToXml(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    new XmlSerializer(obj.GetType()).Serialize(fs, obj);
                }
            }
        }
        /// <summary>
        /// XML�����л��Ӵ��̵��ڴ����
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    return new XmlSerializer(typeof(object)).Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// ���Ͱ汾��XML���������л��������ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializeToXml<T>(string fileName, T obj) where T : class
        {
            try
            {
                lock (lockObj)
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        new XmlSerializer(typeof(T)).Serialize(fs, obj);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// ���Ͱ汾��XML�����л��Ӵ��̵��ڴ����
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string fileName) where T : class
        {
            try
            {
                if (!File.Exists(fileName))
                    return null;
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    return new XmlSerializer(typeof(T)).Deserialize(fs) as T;
                }
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region JSON
        /// <summary>
        /// ���������л�������
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializableToJson(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        sw.Write(js.Serialize(obj));
                    }
                }
            }
        }
        /// <summary>
        /// �����Ʒ����л��Ӵ��̵��ڴ����
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromJson<T>(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    using (StreamReader sw = new StreamReader(fs, Encoding.Default))
                    {
                        return js.Deserialize<T>(sw.ReadToEnd());
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }

}
