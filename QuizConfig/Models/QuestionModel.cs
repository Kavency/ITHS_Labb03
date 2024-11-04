using QuizConfig.MiscClasses;
using System.Text.Json.Serialization;

namespace QuizConfig.Models
{
    internal class QuestionModel
    {

        #region Properties
        [JsonPropertyName("type")]
        public string Type { get; set; }


        [JsonPropertyName("difficulty")]
        [JsonConverter(typeof(JsonDifficultyConverter))]
        public Difficulty Difficulty { get; set; }


        [JsonPropertyName("category")]
        public string Category { get; set; }


        [JsonPropertyName("question")]
        public string Question { get; set; }


        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; }


        [JsonPropertyName("incorrect_answers")]
        public string[] IncorrectAnswers { get; set; }
        #endregion


        #region Constructors
        public QuestionModel() { }

        public QuestionModel(string question, string correctAnswer, string incorrectAnswer1,
            string incorrectAnswer2, string incorrectAnswer3, string type = "Multiple",
            Difficulty difficulty = Difficulty.Medium, string category = "General")
        {
            this.Question = question;
            this.CorrectAnswer = correctAnswer;
            this.IncorrectAnswers = [incorrectAnswer1, incorrectAnswer2, incorrectAnswer3];
            this.Type = type;
            this.Difficulty = difficulty;
            this.Category = category;
        }
        #endregion
    }
}
