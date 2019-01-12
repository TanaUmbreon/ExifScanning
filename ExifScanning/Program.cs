using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExifScanning
{
    public static class Program
    {
        /// <summary>
        /// エントリポイント。
        /// </summary>
        static void Main(string[] args)
        {
            var options = new CommandLineOptions(args);

            using (var writer = new StreamWriter(options.OutputFileName, false, options.OutputFileEncoding))
            {
                foreach (var dirPath in options.TargetDirectoryPaths)
                {
                    Console.WriteLine($"\"{dirPath}\" をスキャンしています...");
                    WriteDateTimeOriginal(writer, dirPath, options.TargetFileExtensions);
                }
            }
        }

        /// <summary>
        /// 指定したストリームに、指定したフォルダ内 (サブフォルダも含む) にある指定した拡張子のファイルから撮影日時を取得してテキスト出力します。
        /// </summary>
        /// <param name="writer">出力ストリーム。</param>
        /// <param name="directoryPath">スキャンするフォルダのパス。</param>
        /// <param name="fileExtensions">撮影日時を取得する対象のファイル拡張子のコレクション。</param>
        private static void WriteDateTimeOriginal(StreamWriter writer, string directoryPath, IEnumerable<string> fileExtensions)
        {
            const string AllSearchPattern = "*";
            var filePaths = Directory.EnumerateFiles(directoryPath, AllSearchPattern, SearchOption.AllDirectories).
                Where(path => fileExtensions.Any(ext => path.ToLower().EndsWith(ext)));

            foreach (var path in filePaths)
            {
                var info = new ExifInfo(path);
                writer.WriteLine(info.DateTimeOriginal);
            }
        }
    }
}
