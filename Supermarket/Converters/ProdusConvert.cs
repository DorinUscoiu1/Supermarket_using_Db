using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using Supermarket.Model;

namespace Supermarket.Converters
{
    public class ProdusConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
                return null;

            return new Produs
            {
                Nume = values[0]?.ToString(),
                CodBare = values[1]?.ToString(),
                Categoria = values[2] as Categorie,
                Producator = values[3] as Producator
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
