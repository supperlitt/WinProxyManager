using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.MyFileDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ConsoleDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string uid = "sa";
            string pwd = "Aa123456";
            string md5uid = Tool.GetMD5String(uid);
            string md5pwd = Tool.GetMD5String(pwd);
            string key = Tool.GetMD5String(md5uid + md5pwd);
            string number = Tool.MD5ToNumberStr(key);
            string[] array = Tool.NumberToEncryptKey(number);

            string sqlconnectionStr = "database=C:\test.th;uid=sa;pwd=Aa123456;server=localhost";
            using (MyFileDbConnection sqlcn = new MyFileDbConnection(sqlconnectionStr))
            {
                MyFileDbCommand sqlcm = new MyFileDbCommand();
                sqlcm.CommandText = "create table Test(id int primary key identity(1,1),name varchar(20) not null);";
                sqlcm.Connection = sqlcn;

                sqlcn.Open();
                sqlcm.ExecuteNonQuery();
            }

            string name = "test";
            using (MyFileDbConnection sqlcn = new MyFileDbConnection(sqlconnectionStr))
            {
                MyFileDbCommand sqlcm = new MyFileDbCommand();
                sqlcm.CommandText = "insert into Test(name) values (@name);";
                sqlcm.Connection = sqlcn;
                List<MyFileDbParameter> listParam = new List<MyFileDbParameter>();
                listParam.Add(new MyFileDbParameter("@name", DbType.String) { Value = name });
                sqlcn.Open();
                sqlcm.ExecuteNonQuery();
            }

            int id = 1;
            name = "test2";
            using (MyFileDbConnection sqlcn = new MyFileDbConnection(sqlconnectionStr))
            {
                MyFileDbCommand sqlcm = new MyFileDbCommand();
                sqlcm.CommandText = "update Test set name=@name where id=@id";
                sqlcm.Connection = sqlcn;
                List<MyFileDbParameter> listParam = new List<MyFileDbParameter>();
                listParam.Add(new MyFileDbParameter("@name", DbType.String) { Value = name });
                listParam.Add(new MyFileDbParameter("@id", DbType.String) { Value = id });

                sqlcn.Open();
                sqlcm.ExecuteNonQuery();
            }

            using (MyFileDbConnection sqlcn = new MyFileDbConnection(sqlconnectionStr))
            {
                MyFileDbCommand sqlcm = new MyFileDbCommand();
                sqlcm.CommandText = "select * from Test where id=@id";
                sqlcm.Connection = sqlcn;
                List<MyFileDbParameter> listParam = new List<MyFileDbParameter>();
                listParam.Add(new MyFileDbParameter("@name", DbType.String) { Value = name });
                sqlcn.Open();
                sqlcm.ExecuteNonQuery();
            }
        }
    }
}
