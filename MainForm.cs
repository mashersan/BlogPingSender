namespace BlogPingSender
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true、破棄しない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPingNow = new System.Windows.Forms.Button();
            this.contextMenuTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStartStop.Location = new System.Drawing.Point(12, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(121, 35);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "監視開始";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(139, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(94, 35);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "設定...";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 74);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(560, 325);
            this.txtLog.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ログ：";
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.ContextMenuStrip = this.contextMenuTray;
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "ブログ更新Ping送信ツール";
            this.notifyIconMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMain_MouseDoubleClick);
            // 
            // contextMenuTray
            // 
            this.contextMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShow,
            this.toolStripMenuItemExit});
            this.contextMenuTray.Name = "contextMenuTray";
            this.contextMenuTray.Size = new System.Drawing.Size(101, 48);
            // 
            // toolStripMenuItemShow
            // 
            this.toolStripMenuItemShow.Name = "toolStripMenuItemShow";
            this.toolStripMenuItemShow.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItemShow.Text = "表示";
            this.toolStripMenuItemShow.Click += new System.EventHandler(this.toolStripMenuItemShow_Click);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItemExit.Text = "終了";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // btnPingNow
            // 
            this.btnPingNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPingNow.Location = new System.Drawing.Point(459, 12);
            this.btnPingNow.Name = "btnPingNow";
            this.btnPingNow.Size = new System.Drawing.Size(113, 35);
            this.btnPingNow.TabIndex = 4;
            this.btnPingNow.Text = "今すぐPing送信";
            this.btnPingNow.UseVisualStyleBackColor = true;
            this.btnPingNow.Click += new System.EventHandler(this.btnPingNow_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.btnPingNow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnStartStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.Text = "ブログ更新Ping送信ツール";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.Button btnPingNow;
    }
}