using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\用户目录\Desktop\qq.txt";
            var bs = File.ReadAllBytes(filePath);
            ZipHelper.Zip(@"qq.txt", bs, @"D:\用户目录\Desktop\qq.zip");

            Console.WriteLine();
            Console.WriteLine("OVER");
            Console.ReadLine();
        }

        static void TestAccount()
        {
            string path1 = @"D:\用户目录\Documents\Tencent Files\464212863\FileRecv\蜀景成功账号.txt";
            string path2 = @"D:\用户目录\Documents\Tencent Files\464212863\FileRecv\蜀景成功账号1.txt";
            string path3 = @"D:\用户目录\Documents\Tencent Files\464212863\FileRecv\1-1000.txt";
            string path4 = @"D:\用户目录\Documents\Tencent Files\464212863\FileRecv\2-1000.txt";

            string[] lines1 = File.ReadAllLines(path1);
            string[] lines2 = File.ReadAllLines(path2);
            string[] lines3 = File.ReadAllLines(path3);
            string[] lines4 = File.ReadAllLines(path4);

            List<string> result1 = new List<string>();
            List<string> result2 = new List<string>();

            List<string[]> dataList = new List<string[]>() { lines1, lines2 };
            foreach (var linearray in dataList)
            {
                foreach (var line in linearray)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        result1.Add(line);
                    }
                }
            }

            dataList = new List<string[]>() { lines3, lines4 };
            foreach (var linearray in dataList)
            {
                foreach (var line in linearray)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        result2.Add(line);
                    }
                }
            }

            List<string> result = new List<string>();
            foreach (var item in result1)
            {
                if (result2.Contains(item))
                {
                    if (!result.Contains(item))
                    {
                        result.Add(item);
                    }
                }
            }

            File.WriteAllLines(@"D:\用户目录\Desktop\tttt.txt", result.ToArray());
        }

        static void TestIP()
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.GetEncoding("gb2312");
            if (true)
            {
                WebProxy proxy = new WebProxy();
                proxy.UseDefaultCredentials = false;
                proxy.Address = new Uri("http://85.143.164.100:81"); // new Uri("http://183.239.167.122:8080");
                client.Proxy = proxy;
            }

            string result = client.DownloadString("http://1212.ip138.com/ic.asp");
            Console.WriteLine(result);
        }
    }
}
