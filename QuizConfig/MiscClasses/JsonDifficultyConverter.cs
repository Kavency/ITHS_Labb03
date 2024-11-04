using System.Text.Json.Serialization;
using System.Text.Json;

namespace QuizConfig.MiscClasses
{
    public class JsonDifficultyConverter : JsonConverter<Difficulty>
    {
        public override Difficulty Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Enum.Parse<Difficulty>(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Difficulty value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
