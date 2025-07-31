// Settings.cs の中身をこれで置き換えてください
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace BlogPingSender
{
    public class Settings
    {
        public List<BlogInfo> MonitoredBlogs { get; set; } = new List<BlogInfo>();
        public List<string> PingUrls { get; set; } = new List<string> { "http://rpc.pingomatic.com/" };
        public int CheckIntervalMinutes { get; set; } = 60;
        public bool MinimizeToTrayOnClose { get; set; } = true;

        // ★★ ここから追加 ★★
        public bool StartWithWindows { get; set; } = false;
        public bool StartMonitoringOnLaunch { get; set; } = true;
        // ★★ ここまで追加 ★★

        public static readonly string settingsFilePath = Path.Combine(Application.StartupPath, "settings.json");

        public void Save()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
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

        public static Settings Load()
        {
            if (!File.Exists(settingsFilePath)) return new Settings();
            try
            {
                string jsonString = File.ReadAllText(settingsFilePath);
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