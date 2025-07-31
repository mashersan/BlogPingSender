using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace BlogPingSender
{
    /// <summary>
    /// アプリケーションの設定を管理するクラス
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// 監視対象のブログリスト
        /// </summary>
        public List<BlogInfo> MonitoredBlogs { get; set; } = new List<BlogInfo>();

        /// <summary>
        /// Ping送信先のURLリスト
        /// </summary>
        public List<string> PingUrls { get; set; } = new List<string> { "http://rpc.pingomatic.com/" };

        /// <summary>
        /// 更新をチェックする間隔（分）
        /// </summary>
        public int CheckIntervalMinutes { get; set; } = 60;

        /// <summary>
        /// 閉じるボタンでタスクトレイに常駐させるかどうか
        /// </summary>
        public bool MinimizeToTrayOnClose { get; set; } = true;

        /// <summary>
        /// Windows起動時に自動実行するかどうか
        /// </summary>
        public bool StartWithWindows { get; set; } = false;

        /// <summary>
        /// アプリ起動時に自動で監視を開始するかどうか
        /// </summary>
        public bool StartMonitoringOnLaunch { get; set; } = true;

        /// <summary>
        /// 設定ファイルのフルパス
        /// </summary>
        public static readonly string settingsFilePath = Path.Combine(Application.StartupPath, "settings.json");

        /// <summary>
        /// 現在の設定内容をJSONファイルに保存する
        /// </summary>
        public void Save()
        {
            try
            {
                // 日本語が文字化けしないようにエンコーダーを指定
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // 人が読みやすいようにインデントを付ける
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                };
                string jsonString = JsonSerializer.Serialize(this, options);
                File.WriteAllText(settingsFilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"設定の保存に失敗しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// JSONファイルから設定を読み込む
        /// </summary>
        /// <returns>読み込まれた設定オブジェクト</returns>
        public static Settings Load()
        {
            // ファイルが存在しない場合は、デフォルト値で新しい設定オブジェクトを返す
            if (!File.Exists(settingsFilePath)) return new Settings();

            try
            {
                string jsonString = File.ReadAllText(settingsFilePath);
                // JSONからデシリアライズして返す。失敗した場合はnullになるので、その際はnew Settings()を返す
                return JsonSerializer.Deserialize<Settings>(jsonString) ?? new Settings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"設定の読み込みに失敗しました。デフォルト設定を使用します。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new Settings();
            }
        }
    }
}