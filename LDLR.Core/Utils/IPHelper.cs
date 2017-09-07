using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace LDLR.Core.Utils
{
    public class IPHelper
    {
        /// <summary>
        /// �õ��ɣе�ַ
        /// </summary>
        /// <returns></returns>
        public static string GetRealIPAddress()
        {
            return GetRealIPAddress(null);
        }
        public static string GetRealIPAddress(NameValueCollection serverVariables)
        {
            string result = String.Empty;
            try
            {
                serverVariables = serverVariables ?? System.Web.HttpContext.Current.Request.ServerVariables;
                result = serverVariables["HTTP_X_FORWARDED_FOR"];

                if (result != null && result != String.Empty)
                {
                    //�����д��� 
                    if (result.IndexOf(".") == -1)    //û�С�.���϶��Ƿ�IPv4��ʽ 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //�С�,�������ƶ������ȡ��һ������������IP�� 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IPHelper.IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //�ҵ����������ĵ�ַ 
                                }
                            }
                        }
                        else if (IPHelper.IsIPAddress(result)) //������IP��ʽ 
                            return result;
                        else
                            result = null;    //�����е����� ��IP��ȡIP 
                    }

                }

                string IpAddress = (serverVariables["HTTP_X_FORWARDED_FOR"] != null
                    && serverVariables["HTTP_X_FORWARDED_FOR"] != String.Empty)
                    ? serverVariables["HTTP_X_FORWARDED_FOR"]
                    : serverVariables["REMOTE_ADDR"];

                if (null == result || result == String.Empty)
                    result = serverVariables["REMOTE_ADDR"];
            }
            catch (Exception)
            {

            }

            return result;
        }

        /// <summary> 
        /// ȡ�ÿͻ�����ʵIP������д�����ȡ��һ����������ַ���ܴ������������⣩ 
        /// <![CDATA[��ԭ��ɲ鿴����C#ȡ��ʵIP��ַ��������]]>
        /// </summary> 
        public static string getRealIPAddress
        {
            get
            {
                string result = String.Empty;

                result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //�����д��� 
                    if (result.IndexOf(".") == -1)    //û�С�.���϶��Ƿ�IPv4��ʽ 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //�С�,�������ƶ������ȡ��һ������������IP�� 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IPHelper.IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //�ҵ����������ĵ�ַ 
                                }
                            }
                        }
                        else if (IPHelper.IsIPAddress(result)) //������IP��ʽ 
                            return result;
                        else
                            result = null;    //�����е����� ��IP��ȡIP 
                    }

                }

                string IpAddress = (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (null == result || result == String.Empty)
                    result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = System.Web.HttpContext.Current.Request.UserHostAddress;

                return result;
            }
        }

        /// <summary>
        /// �ж��Ƿ���IP��ַ��ʽ 0.0.0.0
        /// </summary>
        /// <param name="str1">���жϵ�IP��ַ</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str)
        {
            if (str == null || str == string.Empty || str.Length < 7 || str.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";


            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str);
        }

        /// <summary>
        /// ��IPת��Ϊʮ������ʽ
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static ulong IP2Number(string ip)
        {
            byte[] bytes = System.Net.IPAddress.Parse(ip.Trim()).GetAddressBytes();
            ulong ret = 0;
            foreach (byte b in bytes)
            {
                ret <<= 8;
                ret |= b;
            }
            return ret;
        }

    }
}
