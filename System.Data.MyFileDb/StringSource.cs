using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.MyFileDb
{
    public class StringSource
    {
        public static string NotFoundDataBase
        {
            get { return "找不到数据库"; }
        }

        public static string DataBaseBreakDown
        {
            get { return "数据库损坏"; }
        }

        public static string NameOrPwdIsError
        {
            get { return "账号或密码错误"; }
        }
    }
}
