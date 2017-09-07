using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;

namespace LDLR.Core.Utils.Http
{
    /// <summary>
    /// Request������
    /// </summary>
    public class RequestHelper
    {
        #region �жϵ�ǰҳ���Ƿ���յ���Post�������Get����
        /// <summary>
        /// �жϵ�ǰҳ���Ƿ���յ���Post����
        /// </summary>
        /// <returns>�Ƿ���յ���Post����</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// �жϵ�ǰҳ���Ƿ���յ���Get����
        /// </summary>
        /// <returns>�Ƿ���յ���Get����</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        #endregion

        #region ������һ��ҳ��ĵ�ַ
        /// <summary>
        /// ������һ��ҳ��ĵ�ַ
        /// </summary>
        /// <returns>��һ��ҳ��ĵ�ַ</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }
        #endregion

        #region �õ���ǰ��������ͷ
        /// <summary>
        /// �õ���ǰ��������ͷ
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }
        #endregion

        #region �õ�����ͷ
        /// <summary>
        /// �õ�����ͷ
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }
        #endregion

        #region ��ȡ��ǰ�����ԭʼ URL(URL ������Ϣ֮��Ĳ���,������ѯ�ַ���(�������))
        /// <summary>
        /// ��ȡ��ǰ�����ԭʼ URL(URL ������Ϣ֮��Ĳ���,������ѯ�ַ���(�������))
        /// </summary>
        /// <returns>ԭʼ URL</returns>
        public static string RawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        #endregion

        #region �жϵ�ǰ�����Ƿ�������������
        /// <summary>
        /// �жϵ�ǰ�����Ƿ�������������
        /// </summary>
        /// <returns>��ǰ�����Ƿ�������������</returns>
        public static bool IsFromBrowser()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region �ж��Ƿ�����������������
        /// <summary>
        /// �ж��Ƿ�����������������
        /// </summary>
        /// <returns>�Ƿ�����������������</returns>
        public static bool IsFromSearchEngines()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region ��õ�ǰ����Url��ַ
        /// <summary>
        /// ��õ�ǰ����Url��ַ
        /// </summary>
        /// <returns>��ǰ����Url��ַ</returns>
        public static string Url()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        #endregion

        #region ��õ�ǰҳ�������
        /// <summary>
        /// ��õ�ǰҳ�������
        /// </summary>
        /// <returns>��ǰҳ�������</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }
        #endregion

        #region ���ر���Url�������ܸ���
        /// <summary>
        /// ���ر���Url�������ܸ���
        /// </summary>
        /// <returns></returns>
        public static int ParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }
        #endregion

        #region ���ָ����������ֵ
        /// <summary>
        /// ���ָ����������ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������ֵ</returns>
        public static string FormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return string.Empty;
            }
            return HttpContext.Current.Request.Form[strName];
        }

        #endregion

        #region ���ָ��Url������ֵ
        /// <summary>
        /// ���ָ��Url������ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static string QueryStrings(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return string.Empty;
            }
            return HttpContext.Current.Request.QueryString[strName];
        }
        #endregion

        #region ��õ�ǰҳ��ͻ��˵�IP
        /// <summary>
        /// ��õ�ǰҳ��ͻ��˵�IP
        /// </summary>
        /// <returns>��ǰҳ��ͻ��˵�IP</returns>
        public static string IP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !ValidatedHelper.IsIP(result))
            {
                return "0.0.0.0";
            }

            return result;

        }
        #endregion

        #region ��ȡUrl�ļ���
        /// <summary>
        /// ��ȡUrl�ļ���
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>Url�ļ���</returns>
        static public string GetUrlFileNameWithoutExtension(string url)
        {
            return System.IO.Path.GetFileNameWithoutExtension(url.Substring(url.LastIndexOf('/') + 1));
        }
        #endregion

        /// <summary>
        /// ����script��iframe
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FilterScriptIFrame(string content)
        {
            return Regex.Replace(content, @"(script|frame)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤脚本
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FilterScript(string content)
        {
            if (content == null || content == "")
            {
                return content;
            }
            string regexstr = @"(?i)<script([^>])*>(\w|\W)*</script([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<script([^>])*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(content, "</script>", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤框架
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string FilterIFrame(string content)
        {
            if (content == null || content == "")
            {
                return content;
            }
            string regexstr = @"(?i)<iframe([^>])*>(\w|\W)*</iframe([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<iframe([^>])*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(content, "</iframe>", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// �ж�Զ��URL�ļ��Ƿ���ڣ�ͨ���ж��ļ�ͷ�ķ�ʽ
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsExistUrlFile(string url)
        {
            try
            {
                //ע�⣺�˷�����Ҫ����Msxml2.dll
                //MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
                //_xmlhttp.open("HEAD", url, false, null, null);
                //_xmlhttp.send("");
                //return (_xmlhttp.status == 200);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
         
        }



    }
}
