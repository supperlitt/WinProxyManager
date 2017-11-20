using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WinProxyManager
{
    public partial class MainFrm : Form
    {
        private int proxyValue = 0;
        private int hideValue = 0;
        private int typeValue = 0;
        private int areaValue = 0;
        private int port = 0;
        private static object lockObj = new object();

        public MainFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.InitSetting();
            this.btnStart.Enabled = false;
            Thread t = new Thread(new ThreadStart(Execute));
            t.IsBackground = true;
            t.Start();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            this.port = int.Parse(this.txtPort.Text);
            this.btnStartServer.Enabled = false;
            Thread serverThread = new Thread(new ThreadStart(ServerExecute));
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void btnClearProxy_Click(object sender, EventArgs e)
        {
            int hours = int.Parse(this.txtHours.Text);
            DataHelper.DeleteData(hours);
        }

        private void btnShowApi_Click(object sender, EventArgs e)
        {
            string msg = string.Format("http://{0}:{1}/getline?业务类型", GetLanIP(), port);
            APIFrm frm = new APIFrm(msg);
            frm.ShowDialog();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.lstView.Columns.Add("索引", 60, HorizontalAlignment.Left);
            this.lstView.Columns.Add("来源", 80, HorizontalAlignment.Left);
            this.lstView.Columns.Add("IP端口", 140, HorizontalAlignment.Left);
            this.lstView.Columns.Add("状态", 80, HorizontalAlignment.Left);
            this.lstView.Columns.Add("更新时间", 140, HorizontalAlignment.Left);

            DataHelper.Init();

            var list = DataHelper.GetAll();
            if (list.Count > 0)
            {
                InitListView(list);
            }
        }

        private void ServerExecute()
        {
            SocketHttpHelper helper = new SocketHttpHelper(IPAddress.Any, this.port);
            helper.GetHandler += helper_GetHandler;
            helper.StartListen(10);
        }

        private string helper_GetHandler(string action, string arg)
        {
            switch (action)
            {
                case "getline":
                    {
                        string line = string.Empty;
                        lock (lockObj)
                        {
                            var item = DataHelper.GetLine(arg);
                            if (item != null)
                            {
                                line = item.Line;
                            }
                        }

                        return line;
                    }
            }

            return string.Empty;
        }

        private void Execute()
        {
            if (this.proxyValue != 0)
            {
                if ((this.proxyValue & (int)ProxyEnum.XiCi) == (int)ProxyEnum.XiCi)
                {
                    // 启动线程，并读取数据到，本地的sqlite里面，后面都是一样。
                    Thread t = new Thread(new ThreadStart(XiCiMethod));
                    t.IsBackground = true;
                    t.Start();
                }

                if ((this.proxyValue & (int)ProxyEnum.Kuai) == (int)ProxyEnum.Kuai)
                {
                    // 启动线程，并读取数据到，本地的sqlite里面，后面都是一样。
                    Thread t = new Thread(new ThreadStart(KuaiMethod));
                    t.IsBackground = true;
                    t.Start();
                }

                if ((this.proxyValue & (int)ProxyEnum.KunPeng) == (int)ProxyEnum.KunPeng)
                {
                    // 启动线程，并读取数据到，本地的sqlite里面，后面都是一样。

                    Thread t = new Thread(new ThreadStart(KunPengMethod));
                    t.IsBackground = true;
                    t.Start();
                }
            }
        }

        #region 线程方法
        private void XiCiMethod()
        {
            XiChiHelper helper = new XiChiHelper(0);
            int pageCount = helper.Init();
            this.ShowMsg("得到线路页数：" + pageCount);
            for (int i = 1; i <= pageCount; i++)
            {
                try
                {
                    List<string> list = helper.GetPageData(i);
                    this.ShowMsg("得到一页数据：" + list.Count);

                    // 加入到检测，用于测试线路正常与否
                    ThreadPool.QueueUserWorkItem(new WaitCallback(CheckLine), list);
                }
                catch { }
                finally
                {
                    // 30秒
                    Thread.Sleep(60 * 1000);
                }
            }
        }

        private void KuaiMethod()
        {
        }

        private void KunPengMethod()
        {
        }

        private void CheckLine(object obj)
        {
            List<string> list = obj as List<string>;
            foreach (var line in list)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(CheckSingleLine), line);
            }
        }

        private void CheckSingleLine(object obj)
        {
            string line = obj as string;
            try
            {
                string[] ip = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                using (TMWebClient client = new TMWebClient(30))
                {
                    client.Encoding = Encoding.GetEncoding("gb2312");
                    WebProxy proxy = new WebProxy();
                    proxy.UseDefaultCredentials = false;
                    proxy.Address = new Uri("http://" + line);
                    client.Proxy = proxy;

                    string result = client.DownloadString("http://1212.ip138.com/ic.asp");
                    if (result.Contains(ip[0]))
                    {
                        LineInfo info = new LineInfo();
                        info.Line = line;
                        info.UpdateTime = DateTime.Now;

                        // 可用
                        DataHelper.AddData(info);
                        AddListView(info);

                        this.ShowMsg(line);
                    }
                }
            }
            catch { }
        }
        #endregion

        private void InitSetting()
        {
            this.proxyValue = (int)((this.chkxichi.Checked ? ProxyEnum.XiCi : ProxyEnum.None) | (this.chkkuaidaili.Checked ? ProxyEnum.Kuai : ProxyEnum.None) | (this.chkkunpeng.Checked ? ProxyEnum.KunPeng : ProxyEnum.None));
            this.hideValue = (int)((this.chkgaoni.Checked ? Visiable.Hide : Visiable.None) | (this.chktouming.Checked ? Visiable.Show : Visiable.None));
            this.typeValue = (int)((this.chkhttp.Checked ? TypeEnum.Http : TypeEnum.None) | (this.chkhttps.Checked ? TypeEnum.Https : TypeEnum.None) | (this.chksock5.Checked ? TypeEnum.Sock5 : TypeEnum.None));
            this.areaValue = (int)((this.chkguonei.Checked ? Area.GuoNei : Area.None) | (this.chkguowai.Checked ? Area.GuoWai : Area.None));
        }

        /// <summary>
        /// 获取局域网IP地址,如果计算机配置了公网IP，可能得到公网的IP
        /// </summary>
        /// <returns></returns>
        public static string GetLanIP()
        {
            string ip = string.Empty;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection nics = mc.GetInstances();
                foreach (ManagementObject nic in nics)
                {
                    if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                    {
                        ip = (nic["IPAddress"] as string[])[0]; // IP地址
                        // string ipsubnet = (nic["IPSubnet"] as String[])[0]; // 子网掩码
                        if (nic["DefaultIPGateway"] == null)
                        {
                            continue;
                        }

                        break;
                    }
                }

                return ip;
            }
            catch
            {
                return "未找到";
            }
        }

        private void ShowMsg(string msg)
        {
            this.Invoke(new Action<TextBox>(p =>
            {
                if (p.Text.Length > 20000)
                {
                    p.Clear();
                }

                p.AppendText(string.Format("{0}\t{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            }), this.txtResult);
        }

        private void InitListView(List<LineInfo> list)
        {
            try
            {
                this.Invoke(new Action<ListView>(p =>
                {
                    p.BeginUpdate();
                    int index = 0;
                    foreach (var line in list)
                    {
                        ListViewItem item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[] { "", line.Line, "", line.UpdateTimeStr });

                        p.Items.Add(item);
                    }

                    p.EndUpdate();
                }), this.lstView);
            }
            catch { }
        }

        private void AddListView(LineInfo line)
        {
            try
            {
                this.Invoke(new Action<ListView>(p =>
                {
                    p.BeginUpdate();
                    int index = p.Items.Count;
                    ListViewItem item = new ListViewItem(index.ToString());
                    item.SubItems.AddRange(new string[] { "", line.Line, "成功", line.UpdateTimeStr });
                    p.Items.Add(item);

                    p.EndUpdate();
                }), this.lstView);
            }
            catch { }
        }
    }

    public enum ProxyEnum
    {
        None = 0x00,
        XiCi = 0x01,
        Kuai = 0x02,
        KunPeng = 0x04
    }

    public enum Visiable
    {
        None = 0x00,
        Hide = 0x01,
        Show = 0x02
    }

    public enum TypeEnum
    {
        None = 0x00,
        Http = 0x01,
        Https = 0x02,
        Sock5 = 0x04
    }

    public enum Area
    {
        None = 0x00,
        GuoNei = 0x01,
        GuoWai = 0x02
    }
}
