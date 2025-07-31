using System;

namespace BlogPingSender
{
    public class BlogInfo
    {
        // プロパティを見やすいように表示するためのオーバーライド
        public string DisplayName => $"{BlogTitle} ({BlogRssUrl})";

        public string BlogTitle { get; set; } = "（タイトル未設定）";
        public string BlogRssUrl { get; set; }
        public string LastPostId { get; set; } = "";

        // JSONシリアライザがパラメータなしのコンストラクタを要求するため
        public BlogInfo() { }

        public BlogInfo(string url)
        {
            BlogRssUrl = url;
        }
    }
}