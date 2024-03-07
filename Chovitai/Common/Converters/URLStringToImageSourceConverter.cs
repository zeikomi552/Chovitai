using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chovitai.Common.Converters
{
    [System.Windows.Data.ValueConversion(typeof(string), typeof(BitmapImage))]
    public class URLStringToImageSourceConverter : System.Windows.Data.IValueConverter
    {

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return null;
            }
            return Convert(value.ToString()!);
        }
        #region Converterのコア部分
        /// <summary>
        /// Converterのコア部分
        /// URIからBitmapImageを生成する
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>BitmapImage</returns>
        public static BitmapImage? Convert(string url)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;      // プロセスを占有しないため
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
                bitmap.EndInit();
                return bitmap;
            }
            catch { return null; }
        }
        #endregion
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
