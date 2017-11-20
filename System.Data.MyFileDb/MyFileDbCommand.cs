using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace System.Data.MyFileDb
{
    public class MyFileDbCommand : IDbCommand
    {
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public string CommandText
        {
            get;
            set;
        }

        public int CommandTimeout
        {
            get;
            set;
        }

        public CommandType CommandType
        {
            get;
            set;
        }

        public IDbConnection Connection
        {
            get;
            set;
        }

        public IDbDataParameter CreateParameter()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery()
        {
            // 解析sql字符串
            // 构建操作
            // 执行操作，返回结果
            SqlAnalysis analy = new SqlAnalysis(this.CommandText);
            analy.Analysis();



            return 0;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public IDataParameterCollection Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction Transaction
        {
            get;
            set;
        }

        public UpdateRowSource UpdatedRowSource
        {
            get;
            set;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
