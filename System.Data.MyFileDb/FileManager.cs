using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace System.Data.MyFileDb
{
    internal class FileManager
    {
        private FileStream fs = null;
        private string path = string.Empty;
        private string uidMd5 = string.Empty;
        private string pwdMd5 = string.Empty;

        private static JavaScriptSerializer js = new JavaScriptSerializer();

        public FileManager(string path, string uid, string pwd)
        {
            this.path = path;
            this.uidMd5 = Tool.GetMD5String(uid);
            this.pwdMd5 = Tool.GetMD5String(pwd);
        }

        public void Create()
        {
            // 默认创建一个__myfile表
            // 2byte(表个数)+4byte(表结构)+Xbyte(具体表结构)+4byte(表结构)+Xbyte(具体表结构)+4byte(0000则表结构完毕，开始表内容)
            // 4byte表内容长度(最大包含1K行数据)+(长度(加密(Gzip编码(内容))))+4byte(0000表内容长度结束)+下一个表
            fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            var table = GetDefaultTableStructure();
            var tableStruct = ObjectToBytes(table);
            byte[] tableStructLength = IntToBytes(tableStruct.Length, 4);
            var defaultData = GetDefaultTableData();
            var dataStruct = ObjectToBytes(defaultData);
            var dataStructLength = IntToBytes(dataStruct.Length, 4);
            byte[] dbHead = IntToBytes(1, 2);
            byte[] endBytes = new byte[4];

            List<byte> bsList = new List<byte>();
            bsList.AddRange(dbHead);
            bsList.AddRange(tableStructLength);
            bsList.AddRange(tableStruct);
            bsList.AddRange(endBytes);
            bsList.AddRange(dataStructLength);
            bsList.AddRange(dataStruct);
            bsList.AddRange(endBytes);

            File.WriteAllBytes(path, bsList.ToArray());
        }

        public bool Open()
        {
            try
            {
                // 加密设计如下，数据库文件组成=2byte表数量内容+(表数byte内容)+(每200byte一个表名)++zip加密压缩流
                this.fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                byte[] buffer = new byte[2];
                int count = fs.Read(buffer, 0, buffer.Length);

                // 必须要满足1024，1024是所有加密的内容
                int len = buffer[0] << 8 + buffer[1];
                byte[] tableLen = new byte[len];
                count = fs.Read(tableLen, 0, tableLen.Length);

                return false;
            }
            catch
            {
                throw new Exception(StringSource.NameOrPwdIsError);
            }
        }

        public void Close()
        { }

        private byte[] ObjectToBytes(object obj)
        {
            string json = js.Serialize(obj);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress))
                {
                    byte[] result = new byte[gs.Length];
                    gs.Write(result, 0, result.Length);

                    // 加密，result,然后返回
                    string data = AESHelper.AESEncrypt(result, this.pwdMd5, this.uidMd5);

                    return Encoding.UTF8.GetBytes(data);
                }
            }

        }

        private byte[] IntToBytes(int length, int byteCount)
        {
            byte[] result = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                result[i] = (byte)((length >> i * 8) & 0xFF);
            }

            return result;
        }

        private Table GetDefaultTableStructure()
        {
            var table = new Table();
            table.Name = "__myfile";
            var columnsList = new List<Column>();
            columnsList.Add(new Column() { Name = "Author", Type = (int)DbType.String });
            columnsList.Add(new Column() { Name = "Version", Type = (int)DbType.String });
            table.Columns = columnsList;

            return table;
        }

        private object GetDefaultTableData()
        {
            var defaultData = new { Author = "supperlitt", Version = "16.06.18.1" };

            return defaultData;
        }
    }

    public class Table
    {
        public string Name { get; set; }

        private List<Column> columns = new List<Column>();
        public List<Column> Columns
        {
            get { return this.columns; }
            set { this.columns = value; }
        }
    }

    public class Column
    {
        public string Name { get; set; }

        public int Type { get; set; }
    }
}
