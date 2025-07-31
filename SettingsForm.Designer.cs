using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BlogPingSender
{
    public partial class SettingsForm : Form
    {
        public Settings CurrentSettings { get; private set; }

        public SettingsForm(Settings settings)
        {
            InitializeComponent();

            CurrentSettings = new Settings
            {
                MonitoredBlogs = settings.MonitoredBlogs.Select(b => new BlogInfo { BlogTitle = b.BlogTitle, BlogRssUrl = b.BlogRssUrl, LastPostId = b.LastPostId }).ToList(),
                PingUrls = settings.PingUrls.ToList(),
                CheckIntervalMinutes = settings.CheckIntervalMinutes,
                MinimizeToTrayOnClose = settings.MinimizeToTrayOnClose,
                StartWithWindows = settings.StartWithWindows,
                StartMonitoringOnLaunch = settings.StartMonitoringOnLaunch
            };
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadBlogsToListBox();
            LoadPingsToListBox();
            numCheckInterval.Value = CurrentSettings.CheckIntervalMinutes;
            chkMinimizeToTray.Checked = CurrentSettings.MinimizeToTrayOnClose;
            chkStartWithWindows.Checked = CurrentSettings.StartWithWindows;
            chkStartMonitoringOnLaunch.Checked = CurrentSettings.StartMonitoringOnLaunch;
        }

        private void LoadBlogsToListBox()
        {
            lstBlogs.DataSource = null;
            lstBlogs.DataSource = CurrentSettings.MonitoredBlogs;
            lstBlogs.DisplayMember = "DisplayName";
        }

        private void LoadPingsToListBox()
        {
            lstPingUrls.DataSource = null;
            lstPingUrls.DataSource = CurrentSettings.PingUrls;
        }

        private void btnAddBlog_Click(object sender, EventArgs e)
        {
            string newUrl = Interaction.InputBox("追加するブログのRSSフィードURLを入力してください:", "ブログ追加", "http://");
            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                CurrentSettings.MonitoredBlogs.Add(new BlogInfo(newUrl));
                LoadBlogsToListBox();
            }
        }

        private void btnEditBlog_Click(object sender, EventArgs e)
        {
            if (lstBlogs.SelectedItem is BlogInfo selectedBlog)
            {
                string newUrl = Interaction.InputBox("ブログのRSSフィードURLを編集してください:", "ブログ編集", selectedBlog.BlogRssUrl);
                if (!string.IsNullOrWhiteSpace(newUrl))
                {
                    selectedBlog.BlogRssUrl = newUrl;
                    LoadBlogsToListBox();
                }
            }
        }

        private void btnDeleteBlog_Click(object sender, EventArgs e)
        {
            if (lstBlogs.SelectedItem is BlogInfo selectedBlog)
            {
                if (MessageBox.Show($"{selectedBlog.DisplayName} を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CurrentSettings.MonitoredBlogs.Remove(selectedBlog);
                    LoadBlogsToListBox();
                }
            }
        }

        private void btnAddPing_Click(object sender, EventArgs e)
        {
            string newUrl = Interaction.InputBox("追加するPing送信先のURLを入力してください:", "Ping送信先追加", "http://");
            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                CurrentSettings.PingUrls.Add(newUrl);
                LoadPingsToListBox();
            }
        }

        private void btnDeletePing_Click(object sender, EventArgs e)
        {
            if (lstPingUrls.SelectedItem is string selectedPing)
            {
                if (MessageBox.Show($"{selectedPing} を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CurrentSettings.PingUrls.Remove(selectedPing);
                    LoadPingsToListBox();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CurrentSettings.CheckIntervalMinutes = (int)numCheckInterval.Value;
            CurrentSettings.MinimizeToTrayOnClose = chkMinimizeToTray.Checked;
            CurrentSettings.StartWithWindows = chkStartWithWindows.Checked;
            CurrentSettings.StartMonitoringOnLaunch = chkStartMonitoringOnLaunch.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // まず現在のUIの状態で設定を保存するため、OKボタンのロジックを一時的に呼び出す
            CurrentSettings.CheckIntervalMinutes = (int)numCheckInterval.Value;
            CurrentSettings.MinimizeToTrayOnClose = chkMinimizeToTray.Checked;
            CurrentSettings.StartWithWindows = chkStartWithWindows.Checked;
            CurrentSettings.StartMonitoringOnLaunch = chkStartMonitoringOnLaunch.Checked;

            // 現在のメモリ上の設定をファイルに書き出す
            CurrentSettings.Save();

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "設定ファイル (*.json)|*.json|すべてのファイル (*.*)|*.*";
                sfd.FileName = "settings.json";
                sfd.Title = "設定ファイルをエクスポート";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(Settings.settingsFilePath, sfd.FileName, true);
                        MessageBox.Show("設定をエクスポートしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"エクスポートに失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "設定ファイル (*.json)|*.json|すべてのファイル (*.*)|*.*";
                ofd.Title = "設定ファイルをインポート";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(ofd.FileName, Settings.settingsFilePath, true);
                        MessageBox.Show("設定をインポートしました。\n画面に設定を再読み込みします。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 画面に再読み込み
                        CurrentSettings = Settings.Load();
                        SettingsForm_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"インポートに失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}