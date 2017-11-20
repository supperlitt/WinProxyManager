namespace WinProxyManager
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.chkxichi = new System.Windows.Forms.CheckBox();
            this.chkkuaidaili = new System.Windows.Forms.CheckBox();
            this.chkkunpeng = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chksock5 = new System.Windows.Forms.CheckBox();
            this.chkhttps = new System.Windows.Forms.CheckBox();
            this.chkhttp = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkguowai = new System.Windows.Forms.CheckBox();
            this.chkguonei = new System.Windows.Forms.CheckBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chktouming = new System.Windows.Forms.CheckBox();
            this.chkgaoni = new System.Windows.Forms.CheckBox();
            this.btnShowApi = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnClearProxy = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lstView = new System.Windows.Forms.ListView();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 130);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(66, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "爬代理";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkxichi
            // 
            this.chkxichi.AutoSize = true;
            this.chkxichi.Checked = true;
            this.chkxichi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkxichi.Location = new System.Drawing.Point(19, 20);
            this.chkxichi.Name = "chkxichi";
            this.chkxichi.Size = new System.Drawing.Size(48, 16);
            this.chkxichi.TabIndex = 1;
            this.chkxichi.Text = "西刺";
            this.chkxichi.UseVisualStyleBackColor = true;
            // 
            // chkkuaidaili
            // 
            this.chkkuaidaili.AutoSize = true;
            this.chkkuaidaili.Enabled = false;
            this.chkkuaidaili.Location = new System.Drawing.Point(85, 20);
            this.chkkuaidaili.Name = "chkkuaidaili";
            this.chkkuaidaili.Size = new System.Drawing.Size(60, 16);
            this.chkkuaidaili.TabIndex = 1;
            this.chkkuaidaili.Text = "快代理";
            this.chkkuaidaili.UseVisualStyleBackColor = true;
            // 
            // chkkunpeng
            // 
            this.chkkunpeng.AutoSize = true;
            this.chkkunpeng.Enabled = false;
            this.chkkunpeng.Location = new System.Drawing.Point(162, 20);
            this.chkkunpeng.Name = "chkkunpeng";
            this.chkkunpeng.Size = new System.Drawing.Size(48, 16);
            this.chkkunpeng.TabIndex = 1;
            this.chkkunpeng.Text = "鲲鹏";
            this.chkkunpeng.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkxichi);
            this.groupBox1.Controls.Add(this.chkkunpeng);
            this.groupBox1.Controls.Add(this.chkkuaidaili);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发布站点";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chksock5);
            this.groupBox2.Controls.Add(this.chkhttps);
            this.groupBox2.Controls.Add(this.chkhttp);
            this.groupBox2.Location = new System.Drawing.Point(13, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 48);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "检索类型";
            // 
            // chksock5
            // 
            this.chksock5.AutoSize = true;
            this.chksock5.Enabled = false;
            this.chksock5.Location = new System.Drawing.Point(161, 20);
            this.chksock5.Name = "chksock5";
            this.chksock5.Size = new System.Drawing.Size(54, 16);
            this.chksock5.TabIndex = 0;
            this.chksock5.Text = "Sock5";
            this.chksock5.UseVisualStyleBackColor = true;
            // 
            // chkhttps
            // 
            this.chkhttps.AutoSize = true;
            this.chkhttps.Enabled = false;
            this.chkhttps.Location = new System.Drawing.Point(84, 20);
            this.chkhttps.Name = "chkhttps";
            this.chkhttps.Size = new System.Drawing.Size(54, 16);
            this.chkhttps.TabIndex = 0;
            this.chkhttps.Text = "Https";
            this.chkhttps.UseVisualStyleBackColor = true;
            // 
            // chkhttp
            // 
            this.chkhttp.AutoSize = true;
            this.chkhttp.Checked = true;
            this.chkhttp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkhttp.Location = new System.Drawing.Point(18, 21);
            this.chkhttp.Name = "chkhttp";
            this.chkhttp.Size = new System.Drawing.Size(48, 16);
            this.chkhttp.TabIndex = 0;
            this.chkhttp.Text = "Http";
            this.chkhttp.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkguowai);
            this.groupBox3.Controls.Add(this.chkguonei);
            this.groupBox3.Location = new System.Drawing.Point(288, 69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(246, 48);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "国际分类";
            // 
            // chkguowai
            // 
            this.chkguowai.AutoSize = true;
            this.chkguowai.Enabled = false;
            this.chkguowai.Location = new System.Drawing.Point(84, 20);
            this.chkguowai.Name = "chkguowai";
            this.chkguowai.Size = new System.Drawing.Size(48, 16);
            this.chkguowai.TabIndex = 0;
            this.chkguowai.Text = "国外";
            this.chkguowai.UseVisualStyleBackColor = true;
            // 
            // chkguonei
            // 
            this.chkguonei.AutoSize = true;
            this.chkguonei.Checked = true;
            this.chkguonei.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkguonei.Location = new System.Drawing.Point(18, 21);
            this.chkguonei.Name = "chkguonei";
            this.chkguonei.Size = new System.Drawing.Size(48, 16);
            this.chkguonei.TabIndex = 0;
            this.chkguonei.Text = "国内";
            this.chkguonei.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(6, 6);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(564, 211);
            this.txtResult.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chktouming);
            this.groupBox4.Controls.Add(this.chkgaoni);
            this.groupBox4.Location = new System.Drawing.Point(288, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(246, 50);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "隐藏程度";
            // 
            // chktouming
            // 
            this.chktouming.AutoSize = true;
            this.chktouming.Enabled = false;
            this.chktouming.Location = new System.Drawing.Point(84, 20);
            this.chktouming.Name = "chktouming";
            this.chktouming.Size = new System.Drawing.Size(48, 16);
            this.chktouming.TabIndex = 0;
            this.chktouming.Text = "透明";
            this.chktouming.UseVisualStyleBackColor = true;
            // 
            // chkgaoni
            // 
            this.chkgaoni.AutoSize = true;
            this.chkgaoni.Checked = true;
            this.chkgaoni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkgaoni.Location = new System.Drawing.Point(18, 20);
            this.chkgaoni.Name = "chkgaoni";
            this.chkgaoni.Size = new System.Drawing.Size(48, 16);
            this.chkgaoni.TabIndex = 0;
            this.chkgaoni.Text = "高匿";
            this.chkgaoni.UseVisualStyleBackColor = true;
            // 
            // btnShowApi
            // 
            this.btnShowApi.Location = new System.Drawing.Point(490, 130);
            this.btnShowApi.Name = "btnShowApi";
            this.btnShowApi.Size = new System.Drawing.Size(107, 23);
            this.btnShowApi.TabIndex = 5;
            this.btnShowApi.Text = "读取接口和API";
            this.btnShowApi.UseVisualStyleBackColor = true;
            this.btnShowApi.Click += new System.EventHandler(this.btnShowApi_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(192, 132);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(50, 21);
            this.txtPort.TabIndex = 6;
            this.txtPort.Text = "9999";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnClearProxy
            // 
            this.btnClearProxy.Location = new System.Drawing.Point(359, 130);
            this.btnClearProxy.Name = "btnClearProxy";
            this.btnClearProxy.Size = new System.Drawing.Size(98, 23);
            this.btnClearProxy.TabIndex = 0;
            this.btnClearProxy.Text = "清空代理数据";
            this.btnClearProxy.UseVisualStyleBackColor = true;
            this.btnClearProxy.Click += new System.EventHandler(this.btnClearProxy_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 169);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 249);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 223);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = " 消息 ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lstView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(576, 223);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = " 列表 ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lstView
            // 
            this.lstView.Location = new System.Drawing.Point(7, 7);
            this.lstView.Name = "lstView";
            this.lstView.Size = new System.Drawing.Size(563, 210);
            this.lstView.TabIndex = 0;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            // 
            // txtHours
            // 
            this.txtHours.Location = new System.Drawing.Point(268, 132);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(38, 21);
            this.txtHours.TabIndex = 8;
            this.txtHours.Text = "6";
            this.txtHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "小时前";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(90, 130);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(61, 23);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "开启服务";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 430);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHours);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnShowApi);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClearProxy);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.btnStart);
            this.Name = "MainFrm";
            this.Text = "代理工具";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkxichi;
        private System.Windows.Forms.CheckBox chkkuaidaili;
        private System.Windows.Forms.CheckBox chkkunpeng;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkhttp;
        private System.Windows.Forms.CheckBox chksock5;
        private System.Windows.Forms.CheckBox chkhttps;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkguowai;
        private System.Windows.Forms.CheckBox chkguonei;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chktouming;
        private System.Windows.Forms.CheckBox chkgaoni;
        private System.Windows.Forms.Button btnShowApi;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnClearProxy;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lstView;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartServer;
    }
}

