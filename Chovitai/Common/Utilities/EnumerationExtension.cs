using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Chovitai.Common.Utilities
{
    public class EnumerationExtension : MarkupExtension
    {
        private Type? _enumType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="enumType">Enum</param>
        /// <exception cref="ArgumentNullException">nullの場合Exception</exception>
        public EnumerationExtension(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");

            EnumType = enumType;
        }

        /// <summary>
        /// Enumタイプ
        /// </summary>
        public Type? EnumType
        {
            get { return _enumType; }
            private set
            {
                // nullチェック
                if (_enumType == value || value == null)
                    return;

                // nullチェックをして左辺がnullでなければ左辺をnullならばvalueを返却する
                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                // Enumかどうかをチェック
                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                // 値のセット
                _enumType = value;
            }
        }

        public override object? ProvideValue(IServiceProvider serviceProvider) // or IXamlServiceProvider for UWP and WinUI
        {
            if (EnumType == null)
                return null;

            var enumValues = Enum.GetValues(EnumType);

            return (
              from object enumValue in enumValues
              select new EnumerationMember
              {
                  Value = enumValue,
                  Description = GetDescription(enumValue)
              }).ToArray();
        }

        private string GetDescription(object enumValue)
        {
            if (EnumType == null || enumValue == null || enumValue.ToString() == null)
            {
                return string.Empty;
            }
            else
            {
                string name = enumValue.ToString()!;
                var descriptionAttribute = EnumType
                  .GetField(name)!
                  .GetCustomAttributes(typeof(DescriptionAttribute), false)
                  .FirstOrDefault() as DescriptionAttribute;


                return descriptionAttribute != null
                  ? descriptionAttribute.Description!
                  : enumValue.ToString()!;
            }
        }

        public class EnumerationMember
        {
            public string Description { get; set; } = String.Empty;
            public object? Value { get; set; }
        }
    }
}
