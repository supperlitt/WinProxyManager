using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace WinProxyManager
{
    public class DbDataHelper
    {
        /// <summary>
        /// 远程数据库默认位置
        /// </summary>
        private static string proxyDBPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "proxy.db");

        private static string pwd = "Aa123456";

        private static string createproxyTable = @"CREATE TABLE IF NOT EXISTS ProxyInfo (id integer primary key AutoIncrement,ipport varchar(20),type INT,UpdateTime INT)";
        private static string createUsedProxyTable = @"CREATE TABLE IF NOT EXISTS UsedProxyInfo (id integer primary key AutoIncrement,ipport varchar(20),busi varchar(10),UpdateTime INT)";

        private static string ConnectionStr
        {
            get
            {
                System.Data.SQLite.SQLiteConnectionStringBuilder connstr = new System.Data.SQLite.SQLiteConnectionStringBuilder();
                connstr.DataSource = proxyDBPath;
                connstr.Password = pwd;

                return connstr.ToString();
            }
        }

        public static void Init()
        {
            if (!File.Exists(proxyDBPath))
            {
                SQLiteHelper.CreateDB(proxyDBPath, pwd);
            }

            SQLiteHelper.CreateTable(ConnectionStr, createproxyTable);
            SQLiteHelper.CreateTable(ConnectionStr, createUsedProxyTable);
        }

        public static void AddLine(string ipport, int type)
        {
            string selectSql = "select count(0) from ProxyInfo where ipport=@ipport;";
            string insertSql = "insert into ProxyInfo(ipport,type,UpdateTime) values(@ipport,@type,@UpdateTime);";
            string updateSql = "update ProxyInfo set UpdateTime=@UpdateTime where ipport=@ipport;";
            List<SQLiteParameter> listParams = new List<SQLiteParameter>();
            listParams.Add(new SQLiteParameter("@ipport", DbType.String) { Value = ipport });

            int count = Convert.ToInt32(SQLiteHelper.ExecuteScalar(ConnectionStr, selectSql, CommandType.Text, listParams.ToArray()));
            if (count == 0)
            {
                listParams.Add(new SQLiteParameter("@type", DbType.Int32) { Value = type });
                listParams.Add(new SQLiteParameter("@UpdateTime", DbType.Int32) { Value = GetNowTimeInt() });
                SQLiteHelper.ExecuteNonQuery(ConnectionStr, insertSql, CommandType.Text, listParams.ToArray());
            }
            else
            {
                listParams.Add(new SQLiteParameter("@UpdateTime", DbType.Int32) { Value = GetNowTimeInt() });
                SQLiteHelper.ExecuteNonQuery(ConnectionStr, updateSql, CommandType.Text, listParams.ToArray());
            }
        }

        public static string GetLine(List<int> type, int seconds, string busi)
        {
            string str = string.Join(",", (from f in type select f.ToString()).ToArray());
            List<SQLiteParameter> listParams = new List<SQLiteParameter>();
            listParams.Add(new SQLiteParameter("@UpdateTime", DbType.Int32) { Value = GetNowTimeInt() - seconds });
            listParams.Add(new SQLiteParameter("@busi", DbType.String) { Value = busi });

            string selectSql = string.Format(@"select ProxyInfo.ipport from ProxyInfo
left join UsedProxyInfo on ProxyInfo.ipport=UsedProxyInfo.ipport 
where (UsedProxyInfo.busi is null or UsedProxyInfo.busi=@busi) and (UsedProxyInfo.UpdateTime is null or UsedProxyInfo.UpdateTime<@UpdateTime) and  ProxyInfo.type in ({0}) limit 1;", str);
            string insertSql = "insert into UsedProxyInfo(ipport,busi,UpdateTime) values (@ipport,@busi,@UpdateTime);";
            DataSet ds = SQLiteHelper.ExecuteDataSet(ConnectionStr, selectSql, System.Data.CommandType.Text, listParams.ToArray());
            var dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string ipport = dr["ipport"] as string;

                listParams.Add(new SQLiteParameter("@ipport", DbType.String) { Value = ipport });
                SQLiteHelper.ExecuteNonQuery(ConnectionStr, insertSql, CommandType.Text, listParams.ToArray());

                return ipport;
            }

            return string.Empty;
        }

        private static int GetNowTimeInt()
        {
            return (int)JsTool.GetIntFromTime();
        }
    }

    public class ProxyInfo
    {
        public int id { get; set; }

        public string ipport { get; set; }

        public int type { get; set; }

        public int UpdateTime { get; set; }
    }

    public class UsedProxyInfo
    {
        public int id { get; set; }

        public string ipport { get; set; }

        public string busi { get; set; }

        public int UpdateTime { get; set; }
    }
}
