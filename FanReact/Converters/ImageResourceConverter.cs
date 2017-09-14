using System;
using Xamarin.Forms;

namespace FanReact
{
	public class ImageResourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return ImageSource.FromResource("FanReact.Data." + (value ?? ""), typeof(ImageResourceConverter));
        }

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}

