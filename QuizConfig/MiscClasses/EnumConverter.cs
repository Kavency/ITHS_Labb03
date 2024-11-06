using System.Globalization;
using System.Windows.Data;

namespace QuizConfig.MiscClasses
{
    internal class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Difficulty difficulty)
            {
                return difficulty.ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string difficultyString && Enum.TryParse(typeof(Difficulty), difficultyString,out var result))
            {
                return result;
            }
            return null;
        }
    }
}
