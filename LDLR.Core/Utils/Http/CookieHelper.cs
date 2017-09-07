using System.Web;
using System;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace LDLR.Core.Utils.Http
{

    /// <summary>
    /// Cookie ����������
    /// </summary>
    public class CookieHelper
    {

        #region дcookieֵ
        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="strValue">ֵ</param>
        public static void Write(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// �����Ӷ���дcookies
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="value"></param>
        public static void Write<T>(string strName, T value)
        {
            Write(strName, Utils.SerializeMemoryHelper.SerializeToJson<T>(value));
        }
        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="doMain">��  ����:contoso.com</param>
        public static void WriteWithDomain(string strName, string strValue, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Domain = doMain;
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="key">����</param>
        /// <param name="strValue">ֵ</param>
        public static void Write(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="key">����</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="doMain">��  ����:contoso.com</param>
        public static void WriteWithDomain(string strName, string key, string strValue, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Domain = doMain;
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="strValue">����ʱ��(����)</param>
        public static void Write(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }

            cookie.Value = strValue;
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="strValue">����ʱ��(����)</param>
        /// <param name="doMain">��  ����:contoso.com</param>
        public static void WriteWithDomain(string strName, string strValue, int expires, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Domain = doMain;
            cookie.Value = strValue;
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="key">����</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="expires">����ʱ��(����)</param>
        public static void Write(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// дcookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="key">����</param>
        /// <param name="strValue">ֵ</param>
        /// <param name="expires">����ʱ��(����)</param>
        /// <param name="doMain">��  ����:contoso.com</param>
        public static void WriteWithDomain(string strName, string key, string strValue, int expires, string doMain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Domain = doMain;
            cookie[key] = strValue;
            cookie.Expires = System.DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        #endregion

        #region ��cookieֵ
        /// <summary>
        /// ��cookiesֵ��תΪ����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static T Read<T>(string strName)
        {
            return Utils.SerializeMemoryHelper.DeserializeFromJson<T>(Read(strName));
        }
        /// <summary>
        /// ��cookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <returns>cookieֵ</returns>
        public static string Read(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// ��cookieֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="key">����</param>
        /// <returns>cookieֵ</returns>
        public static string Read(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName][key];
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region Cookie ɾ��
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="name">����</param>
        public static void Remove(string name)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[name] != null)
            {
                HttpCookie myCookie = new HttpCookie(name);
                myCookie.Expires = DateTime.Now.AddMinutes(-1);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="key">����������</param>
        public static void Remove(string name, string key)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies[name] != null
                && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies[name][key]))
            {
                string[] temp = HttpContext.Current.Request.Cookies[name].Value.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> list = new List<string>();
                foreach (string item in temp)
                {
                    if (item.StartsWith(key))
                    {
                        continue;
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                Write(name, string.Join("&", list.ToArray()));
            }
        }
        #endregion
    }
}
