using Supermarket.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Supermarket.Converters
{
    public class CategorieConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1 && values[0] != null)
            {
                return new Categorie
                {
                    Nume = values[0].ToString()
                };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
