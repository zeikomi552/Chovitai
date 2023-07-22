using Chovitai.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chovitai.Common.Converters
{
    [System.Windows.Data.ValueConversion(typeof(ImageNsfwEnum), typeof(Visibility))]
    public class ImageNsfwEnumToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        /// <summary>
        /// イメージフィルター
        /// </summary>
        public static ImageNsfwEnum ImageFilter { get; set; } = ImageNsfwEnum.None;

        #region IValueConverter メンバ

        public static bool Convert(string value)
        {

            var target = (ImageNsfwEnum)Enum.Parse(typeof(ImageNsfwEnum), value.ToString()!);

            if (ImageFilter == ImageNsfwEnum.Empty)
            {
                return true;
            }
            else if (target > ImageFilter)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value.ToString()!) ? Visibility.Visible : Visibility.Collapsed;
        }

        // TwoWayの場合に使用する
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }


}
