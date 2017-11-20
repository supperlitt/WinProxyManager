﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;

namespace WinProxyManager
{
    /// <summary>
    /// Http帮助类
    /// </summary>
    public class HttpHelper
    {
        private static Random rand = new Random((int)DateTime.Now.ToFileTimeUtc());
        private static string userAgent = string.Empty;

        private CookieContainer cc;

        public CookieContainer CC
        {
            get
            {
                return cc;
            }
            set
            {
                this.cc = value;
            }
        }

        public HttpHelper()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            userAgent = UserAgent();
            this.cc = new CookieContainer();
        }

        public HttpHelper(CookieContainer cc)
        {
            userAgent = UserAgent();
            this.cc = cc;
        }

        ///<summary>
        /// 使用post方式访问目标网页，返回stream二进制流
        ///</summary>
        public Stream PostAndGetStream(string targetURL, string formData, string contentType, string referer, bool allowAutoRedirect, Encoding encoding2 = null)
        {
            byte[] data = null;
            if (encoding2 != null)
            {
                data = encoding2.GetBytes(formData);
            }
            else
            {
                //数据编码
                ASCIIEncoding encoding = new ASCIIEncoding();
                //UTF8Encoding encoding = new UTF8Encoding();
                data = encoding.GetBytes(formData);
            }

            //请求目标网页
            HttpWebRequest request = null;
            if (targetURL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(targetURL) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//SecurityProtocolType.Tls1.2;
                ServicePointManager.Expect100Continue = false;
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(targetURL);
            }

            request.CookieContainer = cc;
            request.Method = "POST";    //使用post方式发送数据
            if (string.IsNullOrEmpty(contentType))
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                request.ContentType = contentType;
            }

            request.Referer = referer;
            request.AllowAutoRedirect = allowAutoRedirect;
            request.ContentLength = data.Length;

            request.UserAgent = userAgent;
            //request.UnsafeAuthenticatedConnectionSharing = false;
            //模拟一个UserAgent
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            //获取网页响应结果
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cc.Add(response.Cookies);

            // 302重定向处理
            if (allowAutoRedirect && response.StatusCode == HttpStatusCode.Found)
            {
                // response.ResponseUri.AbsoluteUri
                targetURL = response.Headers["Location"]; ;
                return GetAndGetStream(targetURL, null, referer, allowAutoRedirect);
            }

            Stream stream = response.GetResponseStream();
            return stream;
        }

        ///<summary>
        /// 使用post方式访问目标网页，返回字节数组
        ///</summary>
        public byte[] PostAndGetByte(string targetURL, string formData, string contentType, string referer, bool allowAutoRedirect)
        {
            Stream stream = PostAndGetStream(targetURL, formData, contentType, referer, allowAutoRedirect);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        ///<summary>
        /// 使用post方式访问目标网页，返回图片
        ///</summary>
        public System.Drawing.Image PostAndGetBitmap(string targetURL, string formData, string contentType, string referer, bool allowAutoRedirect)
        {
            Stream stream = PostAndGetStream(targetURL, formData, contentType, referer, allowAutoRedirect);
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            return image;
        }

        ///<summary>
        /// 使用post方式访问目标网页，返回文件
        ///</summary>
        public void PostAndGetBitmap(string targetURL, string formData, string contentType, string referer, bool allowAutoRedirect, string fileName)
        {
            byte[] bytes = PostAndGetByte(targetURL, formData, contentType, referer, allowAutoRedirect);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        ///<summary>
        /// 使用post方式访问目标网页，返回html页面
        ///</summary>
        public string PostAndGetHtml(string targetURL, string formData, string contentType, string referer, bool allowAutoRedirect, Encoding encoding, Encoding encoding2 = null)
        {
            Stream stream = PostAndGetStream(targetURL, formData, contentType, referer, allowAutoRedirect, encoding2);
            string html = new StreamReader(stream, encoding).ReadToEnd();
            return html;
        }

        ///<summary>
        /// 使用get方式访问目标网页，返回stream二进制流
        ///</summary>
        public Stream GetAndGetStream(string targetURL, string contentType, string referer, bool allowAutoRedirect)
        {
            //请求目标网页
            HttpWebRequest request = null;
            if (targetURL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(targetURL) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//SecurityProtocolType.Tls1.2;
                request.KeepAlive = false;
                ServicePointManager.CheckCertificateRevocationList = true;
                ServicePointManager.DefaultConnectionLimit = 100;
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(targetURL);
            }

            request.CookieContainer = cc;
            CC = cc;
            request.Method = "GET";    //使用get方式发送数据
            request.ContentType = contentType;
            request.Referer = referer;
            request.AllowAutoRedirect = allowAutoRedirect;
            request.UserAgent = userAgent;
            request.Accept = "*/*";


            //获取网页响应结果
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cc.Add(response.Cookies);
            if (allowAutoRedirect && response.StatusCode == HttpStatusCode.Found)
            {
                targetURL = response.Headers["Location"];
                return GetAndGetStream(targetURL, contentType, referer, allowAutoRedirect);
            }

            Stream stream = response.GetResponseStream();
            return stream;
        }

        ///<summary>
        /// 使用get方式访问目标网页，返回字节数组
        ///</summary>
        public byte[] GetAndGetByte(string targetURL, string contentType, string referer, bool allowAutoRedirect)
        {
            Stream stream = GetAndGetStream(targetURL, contentType, referer, allowAutoRedirect);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        ///<summary>
        /// 使用get方式访问目标网页，返回图片
        ///</summary>
        public System.Drawing.Image GetAndGetBitmap(string targetURL, string contentType, string referer, bool allowAutoRedirect)
        {
            Stream stream = GetAndGetStream(targetURL, contentType, referer, true);
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            return image;
        }

        ///<summary>
        /// 使用get方式访问目标网页，返回文件
        ///</summary>
        public void GetAndGetFile(string targetURL, string contentType, string referer, bool allowAutoRedirect, string fileName)
        {
            byte[] bytes = GetAndGetByte(targetURL, contentType, referer, allowAutoRedirect);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        ///<summary>
        /// 使用get方式访问目标网页，返回html页面
        ///</summary>
        public string GetAndGetHtml(string targetURL, string contentType, string referer, bool allowAutoRedirect, Encoding encoding)
        {
            Stream stream = GetAndGetStream(targetURL, contentType, referer, allowAutoRedirect);
            string html = new StreamReader(stream, encoding).ReadToEnd();
            return html;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="url">上传地址</param>
        /// <param name="file">文件路径</param>
        /// <param name="uploadName">上传文件的File控件名称</param>
        /// <param name="param">kevvalue参数值</param>
        public string Upload(string url, string file, string uploadName, NameValueCollection param)
        {
            string boundary = DateTime.Now.Ticks.ToString("x");
            HttpWebRequest uploadRequest = (HttpWebRequest)WebRequest.Create(url);//url为上传的地址
            uploadRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            uploadRequest.Method = "POST";
            uploadRequest.Accept = "*/*";
            uploadRequest.KeepAlive = true;
            uploadRequest.Headers.Add("Accept-Language", "zh-cn");
            uploadRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            uploadRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            uploadRequest.CookieContainer = cc;
            HttpWebResponse reponse;

            //创建一个内存流
            Stream memStream = new MemoryStream();

            //确定上传的文件路径
            if (!String.IsNullOrEmpty(file))
            {
                boundary = "--" + boundary;

                //添加上传文件参数格式边界
                string paramFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n";

                //写上参数
                foreach (string key in param.Keys)
                {
                    string formitem = string.Format(paramFormat, key, param[key]);

                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);

                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }

                //添加上传文件数据格式边界
                string dataFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";

                string header = string.Format(dataFormat, uploadName, Path.GetFileName(file));

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);

                //获取文件内容
                FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);

                byte[] buffer = new byte[1024];

                int bytesRead = 0;

                //将文件内容写进内存流
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }

                fileStream.Close();

                //添加文件结束边界
                byte[] boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n\n" + boundary + "\r\nContent-Disposition: form-data; name=\"Upload\"\r\n\nSubmit Query\r\n" + boundary + "--");

                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                //设置请求长度
                uploadRequest.ContentLength = memStream.Length;

                //获取请求写入流
                Stream requestStream = uploadRequest.GetRequestStream();

                //将内存流数据读取位置归零
                memStream.Position = 0;

                byte[] tempBuffer = new byte[memStream.Length];

                memStream.Read(tempBuffer, 0, tempBuffer.Length);

                memStream.Close();

                //将内存流中的buffer写入到请求写入流
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);

                requestStream.Close();
            }

            //获取到上传请求的响应
            reponse = (HttpWebResponse)uploadRequest.GetResponse();
            cc.Add(reponse.Cookies);

            return new StreamReader(reponse.GetResponseStream()).ReadToEnd();
        }

        ///<summary>
        /// 使用get方式访问目标网页，返回stream二进制流
        ///</summary>
        public Stream HeadAndGetStream(string targetURL, string contentType, string referer, bool allowAutoRedirect)
        {
            //请求目标网页
            HttpWebRequest request = null;
            if (targetURL.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(targetURL) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(targetURL);
            }

            request.CookieContainer = cc;
            CC = cc;
            request.Method = "HEAD";    //使用get方式发送数据
            request.ContentType = contentType;
            request.Referer = referer;
            request.AllowAutoRedirect = allowAutoRedirect;
            request.UserAgent = userAgent;

            //获取网页响应结果
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cc.Add(response.Cookies);
            if (allowAutoRedirect && response.StatusCode == HttpStatusCode.Found)
            {
                targetURL = response.Headers["Location"];
                return GetAndGetStream(targetURL, contentType, referer, allowAutoRedirect);
            }

            Stream stream = response.GetResponseStream();
            return stream;
        }

        public string HeadAndGetHtml(string targetURL, string contentType, string referer, bool allowAutoRedirect, Encoding encoding)
        {
            Stream stream = HeadAndGetStream(targetURL, contentType, referer, allowAutoRedirect);
            string html = new StreamReader(stream, encoding).ReadToEnd();
            return html;
        }

        /// <summary>
        /// 检查类型结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        private static string UserAgent()
        {
            List<string> list = new List<string>();
            list.Add("Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)");
            list.Add("Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)");
            list.Add("Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");

            return list[rand.Next(0, list.Count)];
        }
    }
}
