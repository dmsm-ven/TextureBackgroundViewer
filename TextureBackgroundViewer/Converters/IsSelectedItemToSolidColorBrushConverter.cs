using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace TextureBackgroundViewer.Converters;

public class IsSelectedItemToSolidColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var colors = parameter.ToString()
            .Split(';')
            .Select(colorString => (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString))
            .ToArray();

        return new SolidColorBrush((bool)value ? colors[0] : colors[1]);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
