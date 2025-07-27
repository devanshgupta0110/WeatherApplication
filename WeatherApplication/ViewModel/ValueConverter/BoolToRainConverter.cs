using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApplication.ViewModel.ValueConverter
{
	// Converter to change a boolean value to a string indicating whether it is currently raining or not
	public class BoolToRainConverter : IValueConverter
	{

		// Convert method to convert a boolean value to a string
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool isRaining = (bool)value;

			if (isRaining)
				return "Currently raining";

			return "Currently not raining";
		}

		// ConvertBack method to convert a string back to a boolean value
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string isRaining = (string)value;

			if (isRaining == "Currently raining")
				return true;
			return false;
		}
	}
}
