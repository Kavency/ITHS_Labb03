using System.Windows.Markup;

namespace QuizConfig.MiscClasses
{
    public class EnumBinder : MarkupExtension
    {
        public Type EnumType { get; private set; }

        public EnumBinder(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
                throw new Exception("Argument type is not an Enum");

            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
