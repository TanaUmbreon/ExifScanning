using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExifScanning
{
    /// <summary>
    /// コマンド ラインのオプションを表します。
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// スキャン対象のフォルダ パスのコレクションを取得します。
        /// </summary>
        public IEnumerable<string> TargetDirectoryPaths { get; }

        /// <summary>
        /// スキャン対象のファイルの拡張子のコレクションを取得します。
        /// </summary>
        public IEnumerable<string> TargetFileExtensions { get; } = (new[] { ".jpg", ".jpeg" })
            .Select(ext => ext.ToLower()).ToArray(); // 拡張子の照合は小文字で行うので確実に小文字化させておく

        /// <summary>
        /// 出力するファイルの名前を取得します。
        /// </summary>
        public string OutputFileName { get; } = "撮影日時リスト.txt";

        /// <summary>
        /// 出力するファイルの文字エンコーディングを取得します。
        /// </summary>
        public Encoding OutputFileEncoding { get; } = Encoding.Default;

        /// <summary>
        /// <see cref="CommandLineOptions"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="args">コマンド ライン引数。</param>
        public CommandLineOptions(string[] args)
        {
            TargetDirectoryPaths = args;
        }
    }
}
