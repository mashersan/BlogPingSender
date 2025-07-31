using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BlogPingSender
{
    /// <summary>
    /// 各種設定を行うためのフォームクラス
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// このフォームで編集中の設定情報を保持するプロパティ
        /// </summary>
        public Settings CurrentSettings { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="settings">メインフォームから渡される現在の設定</param>
        public SettingsForm(Settings settings)
        {
            InitializeComponent();

            // 渡された設定を直接編集せず、ディープコピー（複製）を作成する。
            // これにより、キャンセルボタンが押されたときに変更が破棄される。
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

        /// <summary>
        /// フォームが読み込まれたときのイベント
        /// </summary>
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // CurrentSettingsの内容をUIコントロールに反映させる
            LoadBlogsToListBox();
            LoadPingsToListBox();
            numCheckInterval.Value = CurrentSettings.CheckIntervalMinutes;
            chkMinimizeToTray.Checked = CurrentSettings.MinimizeToTrayOnClose;
            chkStartWithWindows.Checked = CurrentSettings.StartWithWindows;
            chkStartMonitoringOnLaunch.Checked = CurrentSettings.StartMonitoringOnLaunch;
        }

        /// <summary>
        /// ブログリストをUIに表示する
        /// </summary>
        private void LoadBlogsToListBox()
        {
            lstBlogs.DataSource = null; // データソースを一旦クリア
            lstBlogs.DataSource = CurrentSettings.MonitoredBlogs;
            lstBlogs.DisplayMember = "DisplayName"; // 表示するプロパティ名を指定
        }

        /// <summary>
        /// Ping送信先リストをUIに表示する
        /// </summary>
        private void LoadPingsToListBox()
        {
            lstPingUrls.DataSource = null; // データソースを一旦クリア
            lstPingUrls.DataSource = CurrentSettings.PingUrls;
        }

        // --- イベントハンドラ ---

        private void btnAddBlog_Click(object sender, EventArgs e)
        {
            // 入力ボックスを表示してURLを受け取る
            string newUrl = Interaction.InputBox("追加するブログのRSSフィードURLを入力してください:", "ブログ追加", "http://");
            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                CurrentSettings.MonitoredBlogs.Add(new BlogInfo(newUrl));
                LoadBlogsToListBox(); // UIを更新
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
                    LoadBlogsToListBox(); // UIを更新
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
                    LoadBlogsToListBox(); // UIを更新
                }
            }
        }

        private void btnAddPing_Click(object sender, EventArgs e)
        {
            string newUrl = Interaction.InputBox("追加するPing送信先のURLを入力してください:", "Ping送信先追加", "http://");
            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                CurrentSettings.PingUrls.Add(newUrl);
                LoadPingsToListBox(); // UIを更新
            }
        }

        private void btnDeletePing_Click(object sender, EventArgs e)
        {
            if (lstPingUrls.SelectedItem is string selectedPing)
            {
                if (MessageBox.Show($"{selectedPing} を削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CurrentSettings.PingUrls.Remove(selectedPing);
                    LoadPingsToListBox(); // UIを更新
                }
            }
        }

        /// <summary>
        /// OKボタンがクリックされたときのイベント
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // UIコントロールの現在の値をCurrentSettingsに保存
            CurrentSettings.CheckIntervalMinutes = (int)numCheckInterval.Value;
            CurrentSettings.MinimizeToTrayOnClose = chkMinimizeToTray.Checked;
            CurrentSettings.StartWithWindows = chkStartWithWindows.Checked;
            CurrentSettings.StartMonitoringOnLaunch = chkStartMonitoringOnLaunch.Checked;

            // フォームの結果をOKとして設定し、フォームを閉じる
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// キャンセルボタンがクリックされたときのイベント
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // フォームの結果をCancelとして設定し、フォームを閉じる
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 設定のエクスポート
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            // 現在のUIの状態をメモリ上の設定に反映させてから保存する
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
                        // 現在の設定ファイルを指定の場所にコピー
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

        /// <summary>
        /// 設定のインポート
        /// </summary>
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
                        // 選択されたファイルで現在の設定ファイルを上書き
                        File.Copy(ofd.FileName, ofd.FileName, true);
                        MessageBox.Show("設定をインポートしました。\n画面に設定を再読み込みします。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 画面にインポートした設定を再読み込みして反映させる
                        CurrentSettings = Settings.Load();
                        SettingsForm_Load(sender, e); // Loadイベントを再度呼び出してUIを更新
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