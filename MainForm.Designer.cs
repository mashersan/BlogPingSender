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
    /// <summary>
    /// アプリケーションのメインフォームクラス
    /// </summary>
    public partial class MainForm : Form
    {
        // --- クラスメンバー変数 ---
        // アプリケーション設定
        private Settings _settings;
        // 定期実行タイマー
        private System.Threading.Timer _checkTimer;
        // 監視中かどうかのフラグ
        private bool _isMonitoring = false;
        // アプリケーションを完全に終了するかどうかのフラグ
        private bool _isExiting = false;
        // HTTP通信に使用するクライアント（静的で使いまわす）
        private static readonly HttpClient client = new HttpClient();
        // ログファイルのパス
        private readonly string _logFilePath = Path.Combine(Application.StartupPath, "activity_log.txt");

        // Windowsスタートアップ登録用のレジストリキー情報
        private const string StartupRegistryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string ApplicationName = "BlogPingSender";

        // --- コンストラクタとフォームイベント ---
        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        /// <summary>
        /// アプリケーションの設定をファイルから読み込む
        /// </summary>
        private void LoadSettings()
        {
            _settings = Settings.Load();
            Log("設定を読み込みました。");
        }

        /// <summary>
        /// フォームが初めて表示される前に実行されるイベント
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Windowsスタートアップ設定を現在の設定に合わせて更新
            SetStartup();

            // 「起動時に監視開始」が有効、かつ、ブログが1件以上登録されている場合
            if (_settings.StartMonitoringOnLaunch && _settings.MonitoredBlogs.Any())
            {
                StartMonitoring();
            }
        }

        /// <summary>
        /// フォームが閉じられようとしているときのイベント
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 「完全に終了」フラグが立っておらず、「タスクトレイ常駐」が有効で、ユーザーが「×」ボタンで閉じた場合
            if (!_isExiting && _settings.MinimizeToTrayOnClose && e.CloseReason == CloseReason.UserClosing)
            {
                // フォームが閉じるのをキャンセル
                e.Cancel = true;
                // フォームを非表示にしてタスクトレイに常駐
                this.Hide();
                // タスクトレイアイコンを表示
                notifyIconMain.Visible = true;
                Log("タスクトレイに常駐します。");
            }
            else
            {
                // 上記以外の場合（メニューから終了を選んだ、Windowsがシャットダウンするなど）は、アプリを完全に終了する
                StopMonitoring();
                // アイコンを非表示にする
                notifyIconMain.Visible = false;
            }
        }

        // --- UIイベントハンドラ ---
        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (_isMonitoring)
            {
                MessageBox.Show("設定を変更するには、まず監視を停止してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 設定フォームをモーダルダイアログとして表示
            using (var settingsForm = new SettingsForm(_settings))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // OKが押されたら、変更された設定を取得して保存
                    _settings = settingsForm.CurrentSettings;
                    _settings.Save();
                    Log("設定を保存しました。");

                    // スタートアップ設定を即時反映
                    SetStartup();
                }
                else
                {
                    Log("設定の変更はキャンセルされました。");
                }
            }
        }
        /// <summary>
        /// 今すぐPing送信を実行するボタンのクリックイベント
        /// </summary>
        private async void btnPingNow_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("登録されている全てのサイトに対してpingを送信しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await PingAllBlogsAsync(true);
            }
        }

        /// <summary>
        /// 監視開始/停止ボタンのクリックイベント
        /// </summary>
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


        /// <summary>
        /// タスクトレイアイコンがダブルクリックされたときのイベント
        /// </summary>
        private void notifyIconMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        /// <summary>
        /// メニューの「表示」がクリックされたときのイベント
        /// </summary>
        private void toolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        /// <summary>
        /// 終了メニューがクリックされたときのイベント
        /// </summary>
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            // 「終了」メニューがクリックされたら、完全に終了するフラグを立ててから閉じる
            _isExiting = true;
            this.Close();
        }

        /// <summary>
        /// 非表示になっているフォームを再表示する
        /// </summary>
        private void ShowForm()
        {
            // フォームを表示
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
            {
                // 最小化されていたら元に戻す
                this.WindowState = FormWindowState.Normal;
            }
            // フォームをアクティブにする
            this.Activate();
            // タスクトレイアイコンは非表示に
            notifyIconMain.Visible = false;
        }

        // --- コアロジック ---

        /// <summary>
        /// ブログの監視を開始する
        /// </summary>
        private void StartMonitoring()
        {
            // 既に開始している場合は何もしない
            if (_isMonitoring) return;
            _isMonitoring = true;
            btnStartStop.Text = "監視停止";
            btnSettings.Enabled = false;
            Log("ブログの監視を開始します...");

            // タイマーをセットアップ。初回は即時実行し、その後は設定された間隔で実行。
            _checkTimer = new System.Threading.Timer(
                async _ => await CheckAllBlogsForUpdates(),
                null,
                0, // 最初の実行までの待機時間 (ms)
                _settings.CheckIntervalMinutes * 60 * 1000); // 2回目以降の実行間隔 (ms)
        }

        /// <summary>
        /// ブログの監視を停止する
        /// </summary>
        private void StopMonitoring()
        {
            // 既に停止している場合は何もしない
            if (!_isMonitoring) return;

            // タイマーの今後のスケジュールをすべてキャンセル
            _checkTimer?.Change(Timeout.Infinite, 0);
            // タイマーリソースを解放
            _checkTimer?.Dispose();
            // nullを代入して、GCの対象にする
            _checkTimer = null;

            _isMonitoring = false;
            btnStartStop.Text = "監視開始";
            btnSettings.Enabled = true;
            Log("ブログの監視を停止しました。");
        }

        /// <summary>
        /// Windowsのスタートアップ設定をレジストリに登録または削除する
        /// </summary>
        private void SetStartup()
        {
            try
            {
                // HKEY_CURRENT_USER のRunキーを開く
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(StartupRegistryKey, true))
                {
                    if (_settings.StartWithWindows)
                    {
                        // チェックが入っている場合、レジストリにアプリのパスを登録
                        rk.SetValue(ApplicationName, Application.ExecutablePath);
                        Log("Windows起動時の自動実行を有効にしました。");
                    }
                    else
                    {
                        // チェックが外れている場合、レジストリから削除
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

        /// <summary>
        /// 登録されているすべてのブログの更新を確認する
        /// </summary>
        private async Task CheckAllBlogsForUpdates()
        {
            Log($"全ブログの更新を確認中...");
            if (!_settings.MonitoredBlogs.Any())
            {
                Log("監視対象のブログが登録されていません。");
                return;
            }

            // ToList()でリストのコピーを作成し、ループ中にリストが変更されてもエラーにならないようにする
            foreach (var blog in _settings.MonitoredBlogs.ToList())
            {
                try
                {
                    Log($"  - [{blog.BlogRssUrl}] を確認中...");
                    string rssContent = await client.GetStringAsync(blog.BlogRssUrl);
                    XDocument doc = XDocument.Parse(rssContent);
                    // BloggerのフィードはAtom形式
                    XNamespace ns = "http://www.w3.org/2005/Atom"; 

                    var latestPost = doc.Descendants(ns + "entry").FirstOrDefault();
                    if (latestPost == null)
                    {
                        Log($"    -> 記事を取得できませんでした。");
                        continue;
                    }

                    string latestPostId = latestPost.Element(ns + "id")?.Value;
                    // RSSフィードからブログタイトルを取得。取得できなければURLを仮のタイトルとする
                    string blogTitle = doc.Descendants(ns + "title").FirstOrDefault()?.Value ?? blog.BlogRssUrl;
                    blog.BlogTitle = blogTitle; // 取得したタイトルを保存

                    // 最新記事IDが前回と異なっていれば、更新とみなす
                    if (!string.IsNullOrEmpty(latestPostId) && blog.LastPostId != latestPostId)
                    {
                        Log($"    -> 新しい記事を発見！ Pingを送信します。 (記事ID: {latestPostId})");

                        // デスクトップ通知を表示
                        notifyIconMain.ShowBalloonTip(
                            3000, // 表示時間 (ms)
                            "ブログ更新を発見！",
                            $"{blog.BlogTitle} の更新を検知し、Pingを送信しました。",
                            ToolTipIcon.Info);

                        await SendPings(blog.BlogTitle, blog.BlogRssUrl);
                        // 最新記事IDを更新
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
            // 記事タイトルやLastPostIdの変更をファイルに保存
            _settings.Save();
            Log("全ブログの確認が完了しました。");
        }

        /// <summary>
        /// 登録されている全てのブログにPingを送信する（手動実行用）
        /// </summary>
        /// <param name="isManual">手動実行かどうかのフラグ</param>
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
                // ブログタイトルが未取得の場合はPingをスキップ
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

        /// <summary>
        /// 指定されたブログ情報に基づき、登録されている全サーバーにPingを送信する
        /// </summary>
        private async Task SendPings(string blogTitle, string blogUrl)
        {
            foreach (var pingUrl in _settings.PingUrls)
            {
                Log($"      -> {pingUrl} へ送信中...");
                try
                {
                    // XML-RPC形式のリクエストボディを作成
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

        /// <summary>
        /// ログメッセージをUIとファイルに出力する
        /// </summary>
        /// <param name="message">ログに出力するメッセージ</param>
        private void Log(string message)
        {
            // ログファイルへの追記処理
            try
            {
                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(_logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                // ログファイルへの書き込み失敗はコンソールにのみ表示（アプリの動作を止めないため）
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }

            // フォームが破棄された後はUI操作をしない
            if (IsDisposed || txtLog.IsDisposed) return;

            // UIスレッド以外からの呼び出しでも安全にテキストボックスを更新するための処理
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