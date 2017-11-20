using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WinProxyManager
{
    public class KunPengHelper
    {
        private string url = "http://www.site-digger.com/html/articles/20110516/proxieslist.html";

        private int type = 0;

        /// <summary>
        /// 初始化代理类型
        /// </summary>
        /// <param name="type">
        /// 0-国内高匿代理
        /// 1-国内普通代理
        /// 2-国外高匿代理
        /// 3-国外普通代理
        /// </param>
        public KunPengHelper(int type)
        {
            this.type = type;
        }

        public int Init()
        {
            return 1;
        }

        public List<string> GetPageData()
        {
            List<string> result = new List<string>();
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string content = client.DownloadString(this.url);
            Regex regex = new Regex(@"decrypt\(""(?<data>[^""]+)""\)\);</script></td>\s+?<td>(?<type>[^<]+)</td>\s+?<td>(?<area>[^<]+)<");
            foreach (Match m in regex.Matches(content))
            {
                string type = m.Groups["type"].Value;
                string area = m.Groups["area"].Value;
                string ipport = m.Groups["data"].Value;

                if (this.type == 0)
                {
                    if (type != "Anonymous" || area != "China")
                    {
                        continue;
                    }
                }
                else if (this.type == 1)
                {
                    if (type != "Transparent" || area != "China")
                    {
                        continue;
                    }
                }
                else if (this.type == 2)
                {
                    if (type != "Anonymous" || area == "China")
                    {
                        continue;
                    }
                }
                else if (this.type == 3)
                {
                    if (type != "Transparent" || area == "China")
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

                string value = GetValue(ipport);

                result.Add(value);
            }

            return result;
        }

        private static string GetValue(string data)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
            System.IO.StreamReader txtStream = new System.IO.StreamReader(asm.GetManifestResourceStream("WinProxyManager.source.aes.js"));
            string str2 = txtStream.ReadToEnd();
            string fun = string.Format(@"decrypt('{0}')", data);
            string result = ExecuteScript(fun, str2);

            return result;
        }

        /// <summary>
        /// 执行JS
        /// </summary>
        /// <param name="sExpression">参数体</param>
        /// <param name="sCode">JavaScript代码的字符串</param>
        /// <returns></returns>
        private static string ExecuteScript(string sExpression, string sCode)
        {
            MSScriptControl.ScriptControl scriptControl = new MSScriptControl.ScriptControl();
            scriptControl.UseSafeSubset = true;
            scriptControl.Language = "JScript";
            scriptControl.AddCode(sCode);
            try
            {
                string str = scriptControl.Eval(sExpression).ToString();
                return str;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return null;
        }
    }
}
