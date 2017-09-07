using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
namespace LDLR.Core.CacheConfigFile
{
    /// <summary>
    /// �����ļ�������Ϣ������
    /// </summary>
    public class ConfigFilesManager : Singleton<ConfigFilesManager>
    {

        #region ˽��

        /// <summary>
        /// ������
        /// </summary>
        object lockHelper = new object();

        /// <summary>
        /// �����ļ��޸�ʱ��,���ļ���Ϊ�����޸�ʱ��Ϊֵ
        /// </summary>
        public static Dictionary<string, DateTime> fileChangeTime;

        /// <summary>
        /// �����ļ�����
        /// </summary>
        Type configType = null;

        #endregion

        #region ���췽��
        static ConfigFilesManager()
        {
            fileChangeTime = new Dictionary<string, DateTime>();
        }
        /// <summary>
        /// ˽���޲η�����ʵ������ʱ��
        /// </summary>
        private ConfigFilesManager()
        {
        }

        #endregion

        #region ���ò���

        #region ����������
        /// <summary>
        /// �������ļ��ж�ȡ
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        private IConfiger LoadConfigFile(string fileName, Type type)
        {
            this.configType = type;
            fileChangeTime[fileName] = File.GetLastWriteTime(fileName);//�õ������ļ�������޸ĸ�ʱ��    
            return ConfigSerialize.DeserializeInfo(fileName, this.configType);
        }
        /// <summary>
        /// ���������ļ�(ֱ�Ӵ��ļ����أ�
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="type">ʵ������</param>
        /// <returns></returns>
        internal IConfiger LoadConfig(string fileName, Type type)
        {
            return LoadConfig(fileName, type, true);
        }

        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="type">ʵ������</param>
        /// <param name="isCache">�Ƿ�ӻ������</param>
        /// <returns></returns>
        internal IConfiger LoadConfig(string fileName, Type type, bool isCache)
        {
            if (!isCache)
                return LoadConfigFile(fileName, type);
            lock (lockHelper)
            {
                if (DataCache.GetCache(fileName) == null)
                    DataCache.SetCache(fileName, LoadConfigFile(fileName, type));
                DateTime newfileChangeTime = File.GetLastWriteTime(fileName);
                if (!newfileChangeTime.Equals(fileChangeTime[fileName]))
                {
                    DataCache.SetCache(fileName, LoadConfigFile(fileName, type));
                    return LoadConfigFile(fileName, type);
                }
                else
                {
                    return DataCache.GetCache(fileName) as IConfiger;
                }
            }
        }
        #endregion 
        #endregion

    }
}
