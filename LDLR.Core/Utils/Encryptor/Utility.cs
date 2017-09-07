using System;
using System.Collections.Generic;
using System.Text;

namespace LDLR.Core.Utils.Encryptor
{

    /// <summary>
    /// �ӽ�����
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// ��������
        /// </summary>
        public enum EncryptorType
        {
            /// <summary>
            /// DES����
            /// </summary>
            DES,
            /// <summary>
            /// 3DES����
            /// </summary>
            DES3,
            /// <summary>
            /// MD5����
            /// </summary>
            MD5,
            /// <summary>
            /// Base64����
            /// </summary>
            Base64,
            /// <summary>
            /// ���ܷ���
            /// </summary>
            SHA256,
            /// <summary>
            /// DES���ܣ��Զ�����Կ��
            /// </summary>
            DESByEncryptKey,
        }



        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">�����ַ�</param>
        /// <returns></returns>
        public static string EncryptString(string str)
        {
            return EncryptString(str, 0, EncryptorType.DES, null);
        }

        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">�����ַ���</param>
        /// <param name="code">���ܳ��ȣ�ֻ��MD5�����б�����</param>
        /// <param name="type">������������</param>
        /// <param name="type">��Կ</param>
        /// <returns></returns>
        public static string EncryptString(string str, int code, EncryptorType type, string passKey)
        {
            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    _tempString = new DESEncryptor().EncryptString(str);
                    break;
                case EncryptorType.DES3:
                    break;
                case EncryptorType.MD5:
                    _tempString = MD5Encryptor.MD5(str, code);
                    break;
                case EncryptorType.Base64:
                    _tempString = new Base64Encryptor().EncryptString(str);
                    break;
                case EncryptorType.SHA256:
                    _tempString = SHA256Encryptor.SHA256(str);
                    break;
                case EncryptorType.DESByEncryptKey:
                    _tempString = DESByEncryptKey.EncryptDES(str, passKey);
                    break;
                default:
                    throw new ArgumentException("��Ч�ļ�������");
            }
            return _tempString;
        }
        /// <summary>
        /// ���ܷ���(32λ)
        /// </summary>
        /// <param name="str">�����ַ���</param>
        /// <param name="type">������������</param>
        /// <returns></returns>
        public static string EncryptString(string str, EncryptorType type)
        {
            return EncryptString(str, 32, type, null);
        }
        /// <summary>
        /// ���ܷ���,����Կ
        /// </summary>
        /// <param name="str">�����ַ���</param>
        /// <param name="passKey">��Կ</param>
        /// <returns></returns>
        public static string EncryptString(string str, string passKey)
        {
            return EncryptString(str, 32, EncryptorType.DESByEncryptKey, passKey);
        }

        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">�����ַ�</param>
        /// <returns></returns>
        public static string DecryptString(string str)
        {
            return DecryptString(str, EncryptorType.DES, null);
        }

        /// <summary>
        /// ���ܷ���,����Կ
        /// </summary>
        /// <param name="str">������ַ�</param>
        /// <param name="passKey">��Կ</param>
        /// <returns></returns>
        public static string DecryptString(string str, string passKey)
        {
            return DecryptString(str, EncryptorType.DESByEncryptKey, passKey);
        }
        public static string DecryptString(string str, EncryptorType type)
        {
            return DecryptString(str, type, null);
        }
        /// <summary>
        /// ���ܷ���
        /// </summary>
        /// <param name="str">������ַ�</param>
        /// <param name="type">���ܷ���</param>
        /// <returns></returns>
        public static string DecryptString(string str, EncryptorType type, string passKey)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    _tempString = new DESEncryptor().DecryptString(str);
                    break;
                case EncryptorType.DES3:
                    break;
                case EncryptorType.Base64:
                    _tempString = new Base64Encryptor().DecryptString(str);
                    break;
                case EncryptorType.DESByEncryptKey:
                    _tempString = DESByEncryptKey.DecryptDES(str, passKey);
                    break;
                default:
                    throw new ArgumentException("��Ч�ļ�������");
            }

            return _tempString;
        }

    }
}
