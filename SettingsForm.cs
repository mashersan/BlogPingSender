namespace BlogPingSender
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteBlog = new System.Windows.Forms.Button();
            this.btnEditBlog = new System.Windows.Forms.Button();
            this.btnAddBlog = new System.Windows.Forms.Button();
            this.lstBlogs = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeletePing = new System.Windows.Forms.Button();
            this.btnAddPing = new System.Windows.Forms.Button();
            this.lstPingUrls = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkStartMonitoringOnLaunch = new System.Windows.Forms.CheckBox();
            this.numCheckInterval = new System.Windows.Forms.NumericUpDown();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCheckInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDeleteBlog);
            this.groupBox1.Controls.Add(this.btnEditBlog);
            this.groupBox1.Controls.Add(this.btnAddBlog);
            this.groupBox1.Controls.Add(this.lstBlogs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 143);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "監視ブログ一覧";
            // 
            // btnDeleteBlog
            // 
            this.btnDeleteBlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteBlog.Location = new System.Drawing.Point(469, 88);
            this.btnDeleteBlog.Name = "btnDeleteBlog";
            this.btnDeleteBlog.Size = new System.Drawing.Size(85, 28);
            this.btnDeleteBlog.TabIndex = 3;
            this.btnDeleteBlog.Text = "削除";
            this.btnDeleteBlog.UseVisualStyleBackColor = true;
            this.btnDeleteBlog.Click += new System.EventHandler(this.btnDeleteBlog_Click);
            // 
            // btnEditBlog
            // 
            this.btnEditBlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditBlog.Location = new System.Drawing.Point(469, 54);
            this.btnEditBlog.Name = "btnEditBlog";
            this.btnEditBlog.Size = new System.Drawing.Size(85, 28);
            this.btnEditBlog.TabIndex = 2;
            this.btnEditBlog.Text = "編集...";
            this.btnEditBlog.UseVisualStyleBackColor = true;
            this.btnEditBlog.Click += new System.EventHandler(this.btnEditBlog_Click);
            // 
            // btnAddBlog
            // 
            this.btnAddBlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBlog.Location = new System.Drawing.Point(469, 20);
            this.btnAddBlog.Name = "btnAddBlog";
            this.btnAddBlog.Size = new System.Drawing.Size(85, 28);
            this.btnAddBlog.TabIndex = 1;
            this.btnAddBlog.Text = "追加...";
            this.btnAddBlog.UseVisualStyleBackColor = true;
            this.btnAddBlog.Click += new System.EventHandler(this.btnAddBlog_Click);
            // 
            // lstBlogs
            // 
            this.lstBlogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBlogs.FormattingEnabled = true;
            this.lstBlogs.ItemHeight = 12;
            this.lstBlogs.Location = new System.Drawing.Point(15, 21);
            this.lstBlogs.Name = "lstBlogs";
            this.lstBlogs.Size = new System.Drawing.Size(448, 112);
            this.lstBlogs.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDeletePing);
            this.groupBox2.Controls.Add(this.btnAddPing);
            this.groupBox2.Controls.Add(this.lstPingUrls);
            this.groupBox2.Location = new System.Drawing.Point(12, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 142);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ping送信先一覧";
            // 
            // btnDeletePing
            // 
            this.btnDeletePing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeletePing.Location = new System.Drawing.Point(469, 54);
            this.btnDeletePing.Name = "btnDeletePing";
            this.btnDeletePing.Size = new System.Drawing.Size(85, 28);
            this.btnDeletePing.TabIndex = 2;
            this.btnDeletePing.Text = "削除";
            this.btnDeletePing.UseVisualStyleBackColor = true;
            this.btnDeletePing.Click += new System.EventHandler(this.btnDeletePing_Click);
            // 
            // btnAddPing
            // 
            this.btnAddPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPing.Location = new System.Drawing.Point(469, 20);
            this.btnAddPing.Name = "btnAddPing";
            this.btnAddPing.Size = new System.Drawing.Size(85, 28);
            this.btnAddPing.TabIndex = 1;
            this.btnAddPing.Text = "追加...";
            this.btnAddPing.UseVisualStyleBackColor = true;
            this.btnAddPing.Click += new System.EventHandler(this.btnAddPing_Click);
            // 
            // lstPingUrls
            // 
            this.lstPingUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPingUrls.FormattingEnabled = true;
            this.lstPingUrls.ItemHeight = 12;
            this.lstPingUrls.Location = new System.Drawing.Point(15, 21);
            this.lstPingUrls.Name = "lstPingUrls";
            this.lstPingUrls.Size = new System.Drawing.Size(448, 112);
            this.lstPingUrls.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(380, 431);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 37);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(479, 431);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 37);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.chkStartMonitoringOnLaunch);
            this.groupBox3.Controls.Add(this.numCheckInterval);
            this.groupBox3.Controls.Add(this.chkStartWithWindows);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.chkMinimizeToTray);
            this.groupBox3.Location = new System.Drawing.Point(12, 309);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(560, 116);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "動作設定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "監視間隔:";
            // 
            // chkStartMonitoringOnLaunch
            // 
            this.chkStartMonitoringOnLaunch.AutoSize = true;
            this.chkStartMonitoringOnLaunch.Location = new System.Drawing.Point(17, 97);
            this.chkStartMonitoringOnLaunch.Name = "chkStartMonitoringOnLaunch";
            this.chkStartMonitoringOnLaunch.Size = new System.Drawing.Size(185, 16);
            this.chkStartMonitoringOnLaunch.TabIndex = 8;
            this.chkStartMonitoringOnLaunch.Text = "アプリ起動時に自動で監視を開始";
            this.chkStartMonitoringOnLaunch.UseVisualStyleBackColor = true;
            // 
            // numCheckInterval
            // 
            this.numCheckInterval.Location = new System.Drawing.Point(76, 28);
            this.numCheckInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numCheckInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCheckInterval.Name = "numCheckInterval";
            this.numCheckInterval.Size = new System.Drawing.Size(73, 19);
            this.numCheckInterval.TabIndex = 2;
            this.numCheckInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.Location = new System.Drawing.Point(17, 75);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(165, 16);
            this.chkStartWithWindows.TabIndex = 7;
            this.chkStartWithWindows.Text = "Windows起動時に自動実行";
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "分";
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(17, 53);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(221, 16);
            this.chkMinimizeToTray.TabIndex = 3;
            this.chkMinimizeToTray.Text = "閉じるボタンで終了せずタスクトレイに常駐";
            this.chkMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(12, 441);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 27);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "設定をエクスポート...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Location = new System.Drawing.Point(128, 441);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(110, 27);
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "設定をインポート...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 480);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(450, 519);
            this.Name = "SettingsForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCheckInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteBlog;
        private System.Windows.Forms.Button btnEditBlog;
        private System.Windows.Forms.Button btnAddBlog;
        private System.Windows.Forms.ListBox lstBlogs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeletePing;
        private System.Windows.Forms.Button btnAddPing;
        private System.Windows.Forms.ListBox lstPingUrls;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkStartMonitoringOnLaunch;
        private System.Windows.Forms.NumericUpDown numCheckInterval;
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
    }
}