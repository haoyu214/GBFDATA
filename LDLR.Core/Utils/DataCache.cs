using System;
using System.Web;

namespace LDLR.Core.Utils
{

    /// <summary>
    /// ������صĲ�����
    /// ռռ
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];

        }
        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// ���ã�DataCache.SetCache("name", "zzl");
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// ���ã�DataCache.SetCache("name", "zzl", DateTime.Now.AddMinutes(1), TimeSpan.Zero);
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// �Ƴ�ָ��Cache
        /// </summary>
        /// <param name="CacheKey"></param>
        public static void RemoveCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(CacheKey);

        }

        #region Events
        /// <summary>
        /// ����ɾ���¼�
        /// </summary>
        public static event CacheEventHandler CacheDeleted;
        #endregion

        #region Methods
        /// <summary>
        /// ��������ɾ���¼�
        /// </summary>
        public static void OnCacheDeleted(string key)
        {
            if ((CacheDeleted != null))
            {
                CacheDeleted(null, new CacheEventArgs(key));
            }

            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(key);

        }
        #endregion
    }

    #region Delegates
    /// <summary>
    /// ����ί��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CacheEventHandler(object sender, CacheEventArgs e);
    #endregion

    /// <summary>
    /// �����¼�Դ
    /// </summary>
    public class CacheEventArgs
    {
        public CacheEventArgs()
        {

        }
        public CacheEventArgs(string cacheKey)
        {
            this.CacheKey = cacheKey;
        }
        public string CacheKey { get; set; }
    }
}
