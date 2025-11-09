# ブログ更新 Ping 送信ツール 📢

指定したブログの更新を自動で検知し、複数のPingサーバーへ一括で更新通知を送信するWindowsデスクトップアプリケーションです。


---

## ✨ 主な機能

-   **複数サイト対応**: 複数のブログRSSフィードを登録し、まとめて監視できます。
-   **一括Ping送信**: 複数のPingサーバーを登録し、更新時に一括で通知を送信します。
-   **バックグラウンド動作**: タスクトレイに常駐し、PC作業の邪魔をせずにバックグラウンドで動作します。
-   **柔軟なカスタマイズ**:
    -   監視間隔を分単位で自由に設定可能。
    -   Windows起動時の自動実行。
    -   アプリ起動時の監視自動開始。
    -   閉じるボタンの挙動（タスクトレイ常駐 or 終了）の選択。
-   **便利なツール機能**:
    -   任意のタイミングでPingを送信できる「手動送信」機能。
    -   設定内容をファイルで管理できる「インポート/エクスポート」機能。
    -   動作履歴をファイルに記録するロギング機能。

---

## 🚀 使い方

1.  **起動**: `BlogPingSender.exe` を実行します。
2.  **初期設定**:
    -   `[設定...]`ボタンから設定画面を開きます。
    -   「監視ブログ一覧」に監視したいブログのRSS/AtomフィードURLを追加します。
    -   「Ping送信先一覧」に通知したいPingサーバーのURLを追加します。
    -   「動作設定」で好みのオプションにチェックを入れ、`[OK]`で保存します。
3.  **監視開始**: メイン画面で `[監視開始]` ボタンを押すと、設定した間隔でのチェックが始まります。
4.  **タスクトレイ常駐**:
    -   ウィンドウの閉じる（×）ボタンを押すと、アプリはタスクトレイに格納されます。（設定で無効化も可能）
    -   タスクトレイのアイコンをダブルクリック、または右クリックメニューの「表示」でウィンドウが再表示されます。
    -   右クリックメニューの「終了」でアプリケーションを完全に終了します。

---

## 動作環境

-   Windows 10 / 11
-   .NET Framework 4.7.2 (またはそれ以降)

---

## 開発者向け (ソースからのビルド)

このプロジェクトを自分でビルド・改造する場合、以下の環境が必要です。

-   **Visual Studio 2022** (またはそれ以降)
    -   「.NET デスクトップ開発」ワークロード
-   **.NET Framework 4.7.2 SDK** (またはそれ以降)
-   **依存パッケージ** (NuGetからインストール)
    -   `System.Text.Json`
    -   `Costura.Fody` (単一実行ファイル化のため)

---

## 更新履歴

-   **2025/07/31** - `Version 1.0.0`
    -   初期リリース

---

## 📄 ライセンス (License)

このプロジェクトは **MIT ライセンス** の下で公開されています。
これは、ソフトウェアで最も広く使われている、非常に寛容な（ゆるい）ライセンスです。

### ひと目でわかるライセンス要約

#### ✅ 可能なこと (ほぼ何でもできます)

* **商用利用**: このアプリ（またはコード）を使って利益を上げても構いません。
* **改変**: 自由にコードを改造できます。
* **再配布**: 改造したかどうかにかかわらず、コピーを他の人に配っても構いません。
* **プライベート利用**: このコードを公開せずに、個人的な目的や社内のみで利用しても構いません。

#### ⚠️ 守っていただきたい義務 (たった1つだけです)

* **著作権表示の保持**:
    このソフトウェア（のコードや実行ファイル）を再配布する場合、必ず以下の**ライセンス条文（全文）**と**著作権表示（Copyright行）**を、ソフトウェアのコピーまたは重要な部分（例: Readmeやライセンスファイル）に**そのまま含めてください**。

#### 🚫 免責事項 (重要な注意点)

* このソフトウェアは「現状のまま（AS IS）」提供されます。
* このソフトウェアを使用したことによって生じたいかなる損害（PCが壊れた、データが消えた等）についても、**作者（マッシャー (Masher)）は一切の責任を負いません**。ご利用は自己責任でお願いいたします。

---

### MIT ライセンス条文 (全文)

Copyright (c) 2025 マッシャー (Masher)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
