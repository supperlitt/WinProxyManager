using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinProxyManager
{
    public class LineInfo
    {
        public string Line { get; set; }

        public DateTime UpdateTime { get; set; }

        public string UpdateTimeStr { get { return UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
    }
}
