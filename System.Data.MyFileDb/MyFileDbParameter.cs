using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace System.Data.MyFileDb
{
    public class MyFileDbParameter : DbParameter
    {
        public MyFileDbParameter(string name, DbType dbType)
        {
            this.ParameterName = name;
            this.DbType = dbType;
        }

        public override DbType DbType
        {
            get;
            set;
        }

        public override ParameterDirection Direction
        {
            get;
            set;
        }

        public override bool IsNullable
        {
            get;
            set;
        }

        public override string ParameterName
        {
            get;
            set;
        }

        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }

        public override int Size
        {
            get;
            set;
        }

        public override string SourceColumn
        {
            get;
            set;
        }

        public override bool SourceColumnNullMapping
        {
            get;
            set;
        }

        public override DataRowVersion SourceVersion
        {
            get;
            set;
        }

        public override object Value
        {
            get;
            set;
        }
    }
}
