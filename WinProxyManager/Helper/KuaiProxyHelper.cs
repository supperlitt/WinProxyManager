using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WinProxyManager
{
    public class KuaiProxyHelper
    {
        private string type = string.Empty;

        private string url = string.Empty;

        /// <summary>
        /// 初始化代理类型
        /// </summary>
        /// <param name="type">
        /// 0-国内高匿代理
        /// 1-国内普通代理
        /// 2-国外高匿代理
        /// 3-国外普通代理
        /// </param>
        public KuaiProxyHelper(int type)
        {
            if (type == 0)
            {
                this.url = "http://www.kuaidaili.com/free/inha/";
            }
            else if (type == 1)
            {
                this.url = "http://www.kuaidaili.com/free/intr/";
            }
            else if (type == 2)
            {
                this.url = "http://www.kuaidaili.com/free/outha/";
            }
            else if (type == 3)
            {
                this.url = "http://www.kuaidaili.com/free/outtr/";
            }
            else
            {
                throw new Exception("no data");
            }
        }

        /// <summary>
        /// 初始化得到总页数
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string content = client.DownloadString(url);
            Regex regex = new Regex(@"(?<pagecount>\d+)</a></li><li>页</li></ul>");
            int count = int.Parse(regex.Match(content).Groups["pagecount"].Value);

            return count;
        }

        /// <summary>
        /// 得到指定页码的所有代理
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<string> GetPageData(int page)
        {
            List<string> result = new List<string>();
            string currentUrl = string.Format("{0}{1}", this.url, page);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string content = client.DownloadString(currentUrl);
            Regex regex = new Regex(@"<td>(?<ip>[^<]+)</td>\s+?<td>(?<port>\d+)</td>");
            foreach (Match m in regex.Matches(content))
            {
                string ip = m.Groups["ip"].Value;
                string port = m.Groups["port"].Value;

                result.Add(ip + ":" + port);
            }

            return result;
        }
    }
}
