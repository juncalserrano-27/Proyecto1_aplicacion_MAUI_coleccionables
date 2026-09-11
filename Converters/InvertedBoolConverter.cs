using System.Globalization;

namespace RepasoMAUI.Converters
{
    // Invierte un bool (true -> false). Sirve para "muestra la lista solo cuando NO está cargando".
    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b ? !b : value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool b ? !b : value;
    }
}
