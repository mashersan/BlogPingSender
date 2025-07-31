using System;

namespace BlogPingSender
{
    /// <summary>
    /// 監視対象のブログ一つ分の情報を保持するデータクラス
    /// </summary>
    public class BlogInfo
    {
        // プロパティを見やすいように表示するためのオーバーライド
        /// <summary>
        /// UIのリストボックスに表示するための整形済み文字列
        /// </summary>
        public string DisplayName => $"{BlogTitle} ({BlogRssUrl})";

        /// <summary>
        /// ブログのタイトル（RSSから自動取得）
        /// </summary>
        public string BlogTitle { get; set; } = "（タイトル未設定）";

        /// <summary>
        /// ブログのRSS/AtomフィードのURL
        /// </summary>
        public string BlogRssUrl { get; set; }

        /// <summary>
        /// 最後に確認した最新記事のID
        /// </summary>
        public string LastPostId { get; set; } = "";

        // JSONシリアライザがパラメータなしのコンストラクタを要求するため
        /// <summary>
        /// デシリアライズ用の空のコンストラクタ
        /// </summary>
        public BlogInfo() { }

        /// <summary>
        /// URLを指定して新しいインスタンスを生成するコンストラクタ
        /// </summary>
        /// <param name="url">RSS/AtomフィードのURL</param>
        public BlogInfo(string url)
        {
            BlogRssUrl = url;
        }
    }
}