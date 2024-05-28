using Supermarket.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Supermarket.Converters
{
    public class StocConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var produs = values[0] as Produs;
            decimal.TryParse(values[1]?.ToString(), out decimal cantitate);
            string unitateMasura = values[2]?.ToString();
            DateTime.TryParse(values[3]?.ToString(), out DateTime dataAprovizionare);
            DateTime.TryParse(values[4]?.ToString(), out DateTime dataExpirare);
            decimal.TryParse(values[5]?.ToString(), out decimal pretAchizitie);

            return new Stoc
            {
                Produs = produs,
                ProdusId = produs?.Id ?? 0,
                Cantitate = cantitate,
                UnitateMasura = unitateMasura,
                DataAprovizionare = dataAprovizionare,
                DataExpirare = dataExpirare,
                PretAchizitie = pretAchizitie,
                PretVanzare = pretAchizitie * 1.19m
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
