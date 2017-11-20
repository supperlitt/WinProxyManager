using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.MyFileDb
{
    public class MyFileDbConnection : IDbConnection
    {
        #region 解析属性
        private string dbPath = string.Empty;
        public string DbPath
        {
            get { return dbPath; }
            set { dbPath = value; }
        }

        private string uid = string.Empty;
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        private string pwd = string.Empty;
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        private string server = string.Empty;
        public string Server
        {
            get { return this.server; }
            set { this.server = value; }
        }
        #endregion

        private FileManager manager = null;

        public int ConnectionTimeout
        {
            get { return 10; }
        }

        public string Database
        {
            get { return this.dbPath; }
        }

        private ConnectionState state = ConnectionState.Closed;
        public ConnectionState State
        {
            get { return this.state; }
        }

        private string _connectionStr = string.Empty;
        public string ConnectionString
        {
            get
            {
                return this._connectionStr;
            }
            set
            {
                this._connectionStr = value;
            }
        }

        public MyFileDbConnection()
        {
        }

        public MyFileDbConnection(string connectionStr)
        {
            this._connectionStr = connectionStr;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            if (this.manager != null)
            {
                this.manager.Close();
            }
        }

        public IDbCommand CreateCommand()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            Regex databaseRegex = new Regex(@"database=(?<path>[^;]+)", RegexOptions.IgnoreCase);
            Regex uidRegex = new Regex(@"uid=(?<uid>[^;]+)", RegexOptions.IgnoreCase);
            Regex pwdRegex = new Regex(@"pwd=(?<pwd>[^;]+)", RegexOptions.IgnoreCase);
            Regex serverRegex = new Regex(@"server=(?<server>[^;]+)", RegexOptions.IgnoreCase);

            this.dbPath = databaseRegex.Match(this._connectionStr).Groups["path"].Value;
            this.uid = uidRegex.Match(this._connectionStr).Groups["uid"].Value;
            this.pwd = pwdRegex.Match(this._connectionStr).Groups["pwd"].Value;
            this.server = serverRegex.Match(this._connectionStr).Groups["server"].Value;

            string path = this.dbPath;
            // 如果是创库语句就没问题
            //if (path.Contains(":"))
            //{
            //    // 绝对路径
            //    if (!File.Exists(path))
            //    {
            //        throw new Exception(StringSource.NotFoundDataBase);
            //    }
            //}
            //else
            //{
            //    // 相对路径
            //    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.dbPath);
            //    if (!File.Exists(path))
            //    {
            //        throw new Exception(StringSource.NotFoundDataBase);
            //    }
            //}

            this.manager = new FileManager(path, uid, pwd);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
