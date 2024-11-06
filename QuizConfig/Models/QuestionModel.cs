using QuizConfig.MiscClasses;

namespace QuizConfig.Models
{
    internal class QuestionModel
    {
        #region Properties
        public string Type { get; set; }
        public Difficulty Difficulty { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] IncorrectAnswers { get; set; }
        #endregion


        #region Constructors
        public QuestionModel() { }

        public QuestionModel(string question, string correctAnswer, string incorrectAnswer1,
            string incorrectAnswer2, string incorrectAnswer3, string type = "Multiple",
            Difficulty difficulty = Difficulty.medium, string category = "General")
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
