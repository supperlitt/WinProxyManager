using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.MyFileDb
{
    public class SqlAnalysis
    {
        private string sql = string.Empty;

        public SqlAnalysis(string sql)
        {
            this.sql = sql;
        }

        public void Analysis()
        {
            Regex createTableRegex = new Regex(@"create\s+table", RegexOptions.IgnoreCase);
            if (createTableRegex.IsMatch(this.sql))
            {
                // MS SQL
                Regex tableRegex = new Regex(@"create\s+table\s+(?<tableName>\w+)");
                Regex columnRegex = new Regex(@"\s{0,}(?<key>\w+)\s+(?<type>\w+)");
                Regex mainKeyRegex = new Regex(@"primary\s+key", RegexOptions.IgnoreCase);
                Regex commentRegex = new Regex(@"-{2,}\s{0,}(?<comment>[^\s]+)");
                string tableName = tableRegex.Match(this.sql).Groups["tableName"].Value;

                #region 加载列集合
                var columnList = new List<ColumnInfo>();
                string[] lines = this.sql.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    Match m = columnRegex.Match(line);
                    string key = m.Groups["key"].Value;
                    string value = m.Groups["type"].Value;
                    if (key == "" || value == "" || key.ToLower() == "create" || value.ToLower() == "table")
                    {
                        continue;
                    }

                    string comment = commentRegex.Match(line).Groups["comment"].Value;
                    if (string.IsNullOrEmpty(comment))
                    {
                        comment = key;
                    }

                    columnList.Add(new ColumnInfo()
                    {
                        ColumnName = key,
                        DBType = value,
                        IsAutoIncrement = line.ToLower().Contains("identity"),
                        IsMainKey = mainKeyRegex.IsMatch(line),
                        Comment = comment
                    });
                }
                #endregion
            }
            else
            {
                // 增删改查的情况
                Regex insertRegex = new Regex(@"^insert\s+into", RegexOptions.IgnoreCase);
                Regex selectRegex = new Regex(@"^select\s+", RegexOptions.IgnoreCase);
                Regex deleteRegex = new Regex(@"^delete\s+", RegexOptions.IgnoreCase);
                Regex updateRegex = new Regex(@"^update\s+", RegexOptions.IgnoreCase);

                if (insertRegex.IsMatch(this.sql.Trim()))
                {
                    // insert有几种类型
                    // insert into ttt('dt','dt1',dt2') values ('','','',);
                    // insert into ttt(dt,dt1,dt2) values ('','','',);
                    // insert into ttt values ('','','',);
                }
                else if (selectRegex.IsMatch(this.sql.Trim()))
                {

                }
                else if (deleteRegex.IsMatch(this.sql.Trim()))
                {

                }
                else if (updateRegex.IsMatch(this.sql.Trim()))
                {

                }
            }
        }
    }
}
