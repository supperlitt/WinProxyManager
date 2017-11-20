using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace System.Data.MyFileDb
{
    public class Tool
    {
        private static Random random = new Random((int)DateTime.Now.ToFileTimeUtc());

        /// <summary>
        /// MD5字符串
        /// </summary>
        /// <param name="key">需要加密的字符串</param>
        /// <returns>返回MD5加密后的结果</returns>
        public static string GetMD5String(string key)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");
        }

        /// <summary>
        /// MD5字符串转数字字符串
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public static string MD5ToNumberStr(string md5)
        {
            StringBuilder content = new StringBuilder();
            foreach (var c in md5)
            {
                content.Append(((int)(c - '0')).ToString().PadLeft(2, '0'));
            }

            return content.ToString();
        }

        /// <summary>
        /// 得到加密参数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string[] NumberToEncryptKey(string number)
        {
            // 奇数为key,偶数为Iv
            string key = string.Empty;
            string iv = string.Empty;
            for (int i = 0; i < number.Length; i++)
            {
                if (i % 2 == 0)
                {
                    iv += number[i];
                }
                else
                {
                    key += number[i];
                }
            }

            return new string[2] { key, iv };
        }

        /// <summary>
        /// 随机填充
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeStr"></param>
        /// <returns></returns>
        public static byte[] RandomFill(byte[] data, string rangeStr)
        {
            byte[] result = new byte[data.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(int)rangeStr[random.Next(0, rangeStr.Length)];
            }

            return result;
        }
    }
}
