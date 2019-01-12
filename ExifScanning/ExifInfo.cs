using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExifScanning
{
    /// <summary>
    /// 画像の EXIF (Exchangeable Image File Format) 情報を表します。
    /// </summary>
    public class ExifInfo
    {
        /// <summary>
        /// <see cref="PropertyItem.Type"/> プロパティの値を表します。
        /// </summary>
        private static class TypeValue
        {
            /// <summary>null で終わる ASCII 文字列</summary>
            public const int AsciiString = 2;
        }

        /// <summary>
        /// 元の画像が撮影された日時を取得します。
        /// </summary>
        public DateTime DateTimeOriginal { get; }

        /// <summary>
        /// 指定した画像ファイルから
        /// <see cref="ExifInfo"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fileName">画像ファイルのパス。</param>
        public ExifInfo(string fileName)
        {
            using (var image = new Bitmap(fileName))
            {
                IEnumerable<PropertyItem> items = image.PropertyItems;
                DateTimeOriginal = GetDateTimeOriginal(items);
            }
        }

        /// <summary>
        /// 指定したメタデータのコレクションから元の画像が撮影された日時を返します。
        /// </summary>
        /// <param name="items">画像ファイルに格納されたメタデータのコレクション</param>
        /// <returns>元の画像が撮影された日時。</returns>
        private DateTime GetDateTimeOriginal(IEnumerable<PropertyItem> items)
        {
            DateTime defaultValue = DateTime.MinValue;

            const int TagID = 0x9003;
            var item = items.FirstOrDefault(p => p.Id == TagID && p.Type == TypeValue.AsciiString);
            if (item == null) { return defaultValue; }

            string value = Encoding.ASCII.GetString(item.Value).Trim(new[] { '\0' });

            const string Format = "yyyy:MM:dd HH:mm:ss";
            return DateTime.TryParseExact(value, Format, null, DateTimeStyles.None, out var result) ? result : defaultValue;
        }
    }
}
