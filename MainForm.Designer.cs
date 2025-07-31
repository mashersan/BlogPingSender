using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BlogPingSender
{
    public partial class MainForm : Form
    {
        private Settings _settings;
        private System.Threading.Timer _checkTimer;
        private bool _isMonitoring = false;
        private bool _isExiting = false;
        private static readonly HttpClient client = new HttpClient();
        private readonly string _logFilePath = Path.Combine(Application.StartupPath, "activity_log.txt");

        private const string StartupRegistryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string ApplicationName = "BlogPingSender";

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            _settings = Settings.Load();
            Log("設定を読み込みました。");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetStartup();

            if (_settings.StartMonitoringOnLaunch && _settings.MonitoredBlogs.Any())
            {
                StartMonitoring();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (_isMonitoring)
            {
                MessageBox.Show("設定を変更するには、まず監視を停止してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var settingsForm = new SettingsForm(_settings))
            {
                if (settingsForm.ShowDialog() != DialogResult.OK)
                {
                    Log("設定の変更はキャンセルされました。");
                    return;
                }

                _settings = settingsForm.CurrentSettings;
                _settings.Save();
                Log("設定を保存しました。");

                SetStartup();
            }
        }

        private async void btnPingNow_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録されているすべてのサイトに対してPingを送信しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await PingAllBlogsAsync(true);
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_isMonitoring)
            {
                StopMonitoring();
            }
            else
            {
                StartMonitoring();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isExiting && _settings.MinimizeToTrayOnClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIconMain.Visible = true;
                Log("タスクトレイに常駐します。");
            }
            else
            {
                StopMonitoring();
                notifyIconMain.Visible = false;
            }
        }

        private void notifyIconMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        private void toolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            _isExiting = true;
            this.Close();
        }

        private void ShowForm()
        {
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
            notifyIconMain.Visible = false;
        }

        private void StartMonitoring()
        {
            if (_isMonitoring) return;
            _isMonitoring = true;
            btnStartStop.Text = "監視停止";
            btnSettings.Enabled = false;
            Log("ブログの監視を開始します...");

            _checkTimer = new System.Threading.Timer(
                async _ => await CheckAllBlogsForUpdates(),
                null,
                0,
                _settings.CheckIntervalMinutes * 60 * 1000);
        }

        private void StopMonitoring()
        {
            if (!_isMonitoring) return;

            _checkTimer?.Change(Timeout.Infinite, 0);
            _checkTimer?.Dispose();
            _checkTimer = null;

            _isMonitoring = false;
            btnStartStop.Text = "監視開始";
            btnSettings.Enabled = true;
            Log("ブログの監視を停止しました。");
        }

        private void SetStartup()
        {
            try
            {
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(StartupRegistryKey, true))
                {
                    if (_settings.StartWithWindows)
                    {
                        rk.SetValue(ApplicationName, Application.ExecutablePath);
                        Log("Windows起動時の自動実行を有効にしました。");
                    }
                    else
                    {
                        if (rk.GetValue(ApplicationName) != null)
                        {
                            rk.DeleteValue(ApplicationName, false);
                            Log("Windows起動時の自動実行を無効にしました。");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"スタートアップ設定の変更に失敗しました: {ex.Message}");
                MessageBox.Show($"スタートアップ設定の変更に失敗しました。\n管理者権限が必要な場合があります。\n\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CheckAllBlogsForUpdates()
        {
            Log($"全ブログの更新を確認中...");
            if (!_settings.MonitoredBlogs.Any())
            {
                Log("監視対象のブログが登録されていません。");
                return;
            }

            foreach (var blog in _settings.MonitoredBlogs.ToList())
            {
                try
                {
                    Log($"  - [{blog.BlogRssUrl}] を確認中...");
                    string rssContent = await client.GetStringAsync(blog.BlogRssUrl);
                    XDocument doc = XDocument.Parse(rssContent);
                    XNamespace ns = "http://www.w3.org/2005/Atom";

                    var latestPost = doc.Descendants(ns + "entry").FirstOrDefault();
                    if (latestPost == null)
                    {
                        Log($"    -> 記事を取得できませんでした。");
                        continue;
                    }

                    string latestPostId = latestPost.Element(ns + "id")?.Value;
                    string blogTitle = doc.Descendants(ns + "title").FirstOrDefault()?.Value ?? blog.BlogRssUrl;
                    blog.BlogTitle = blogTitle;

                    if (!string.IsNullOrEmpty(latestPostId) && blog.LastPostId != latestPostId)
                    {
                        Log($"    -> 新しい記事を発見！ Pingを送信します。 (記事ID: {latestPostId})");

                        notifyIconMain.ShowBalloonTip(
                            3000,
                            "ブログ更新を発見！",
                            $"{blog.BlogTitle} の更新を検知し、Pingを送信しました。",
                            ToolTipIcon.Info);

                        await SendPings(blog.BlogTitle, blog.BlogRssUrl);
                        blog.LastPostId = latestPostId;
                    }
                    else
                    {
                        Log("    -> 更新はありませんでした。");
                    }
                }
                catch (Exception ex)
                {
                    Log($"    -> エラーが発生しました: {ex.Message}");
                }
            }
            _settings.Save();
            Log("全ブログの確認が完了しました。");
        }

        private async Task PingAllBlogsAsync(bool isManual = false)
        {
            if (isManual)
            {
                Log("手動でのPing送信を開始します...");
            }

            if (!_settings.MonitoredBlogs.Any())
            {
                Log("対象のブログが登録されていません。");
                return;
            }

            foreach (var blog in _settings.MonitoredBlogs)
            {
                if (string.IsNullOrEmpty(blog.BlogTitle) || blog.BlogTitle == "（タイトル未設定）")
                {
                    Log($"ブログ「{blog.BlogRssUrl}」のタイトルが不明なため、Pingをスキップします。一度、更新チェックを実行してタイトルを取得してください。");
                    continue;
                }
                await SendPings(blog.BlogTitle, blog.BlogRssUrl);
            }

            if (isManual)
            {
                Log("手動でのPing送信が完了しました。");
            }
        }

        private async Task SendPings(string blogTitle, string blogUrl)
        {
            foreach (var pingUrl in _settings.PingUrls)
            {
                Log($"      -> {pingUrl} へ送信中...");
                try
                {
                    string xmlRpcBody = $@"
                        <?xml version=""1.0""?>
                        <methodCall>
                          <methodName>weblogUpdates.ping</methodName>
                          <params>
                            <param><value>{blogTitle}</value></param>
                            <param><value>{blogUrl}</value></param>
                          </params>
                        </methodCall>";

                    var content = new StringContent(xmlRpcBody, Encoding.UTF8, "text/xml");
                    var response = await client.PostAsync(pingUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Log($"      <- 送信成功！");
                    }
                    else
                    {
                        Log($"      <- 送信失敗 (ステータスコード: {response.StatusCode})");
                    }
                }
                catch (Exception ex)
                {
                    Log($"      <- 送信エラー: {ex.Message}");
                }
            }
        }

        private void Log(string message)
        {
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(_logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }

            if (IsDisposed || txtLog.IsDisposed) return;

            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(Log), message);
            }
            else
            {
                txtLog.AppendText($"{message}{Environment.NewLine}");
            }
        }
    }
}