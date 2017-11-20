using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinProxyManager
{
    /// <summary>
    /// 数据帮助类
    /// </summary>
    public class DataHelper
    {
        private static readonly string SepStr = "----";
        private static readonly string DefaultName = "默认业务";
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.dat");
        private static List<LineInfo> dataCache = new List<LineInfo>();

        static DataHelper()
        {
        }

        public static void Init()
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }
            else
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);
                foreach (var line in lines)
                {
                    string[] array = line.Split(new string[] { SepStr }, StringSplitOptions.RemoveEmptyEntries);
                    if (array.Length == 2)
                    {
                        dataCache.Add(new LineInfo() { Line = array[0], UpdateTime = DateTime.Parse(array[1]) });
                    }
                }
            }
        }

        public static void ReInit()
        {
            lock (dataCache)
            {
                using (File.Create(filePath))
                {
                    dataCache = new List<LineInfo>();
                }
            }
        }

        public static void AddData(LineInfo info)
        {
            lock (dataCache)
            {
                File.AppendAllText(filePath, info.Line + SepStr + info.UpdateTimeStr + "\r\n", Encoding.UTF8);
                var item = dataCache.Find(p => p.Line == info.Line);
                if (item == null)
                {
                    dataCache.Add(info);
                }
                else
                {
                    item.UpdateTime = info.UpdateTime;
                }
            }
        }

        public static List<LineInfo> GetAll()
        {
            lock (dataCache)
            {
                return dataCache;
            }
        }

        public static LineInfo GetLine(string busiType)
        {
            if (string.IsNullOrEmpty(busiType))
            {
                busiType = DefaultName;
            }

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, busiType + ".txt");
            if (!File.Exists(path))
            {
                File.AppendAllText(path, "", Encoding.UTF8);
            }

            lock (dataCache)
            {
                var lines = File.ReadAllLines(path, Encoding.UTF8);
                var item = dataCache.Find(p => !lines.Contains(p.Line));

                if (item != null)
                {
                    File.AppendAllText(path, item.Line + "\r\n", Encoding.UTF8);
                }

                return item;
            }
        }

        public static void DeleteData(int hours)
        {
            lock (dataCache)
            {
                dataCache.RemoveAll(p => p.UpdateTime < DateTime.Now.AddHours(hours));

                File.WriteAllLines(filePath, (from f in dataCache select f.Line + SepStr + f.UpdateTimeStr).ToArray(), Encoding.UTF8);
            }
        }
    }
}
