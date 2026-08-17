using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BAIOS.Core;

namespace BAIOS.App.Converters;

public sealed class BoolToVisibilityConverter : IValueConverter
{
    public bool Invert { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var flag = value is true;
        if (Invert)
        {
            flag = !flag;
        }

        return flag ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public sealed class SeverityToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            Severity.Ok => new SolidColorBrush(Color.FromRgb(0x3D, 0x9B, 0x6A)),
            Severity.Warning => new SolidColorBrush(Color.FromRgb(0xE6, 0xA8, 0x17)),
            Severity.Fail => new SolidColorBrush(Color.FromRgb(0xD9, 0x4F, 0x4F)),
            _ => new SolidColorBrush(Color.FromRgb(0x9A, 0x9A, 0xA8))
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public sealed class SeverityToLabelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value switch
        {
            Severity.Ok => "OK",
            Severity.Warning => "AVISO",
            Severity.Fail => "FALLO",
            _ => "—"
        };

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
