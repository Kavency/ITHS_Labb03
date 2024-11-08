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
                string lowerCase = difficulty.ToString();

                return char.ToUpper(lowerCase[0]) + lowerCase.Substring(1);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(value is string difficultyString)
            {
                string lowerCase = difficultyString.ToLower();

                if(Enum.TryParse(typeof(Difficulty), lowerCase, out var result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}
