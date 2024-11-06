using QuizConfig.MiscClasses;
using QuizConfig.Models;
using System.Text.Json.Serialization;

namespace QuizConfig.ViewModels
{
    internal class QuestionVM : BaseVM
    {
        [JsonIgnore]
        private readonly QuestionModel _questionModel;


        [JsonPropertyName("type")]
        public string Type
        {
            get => _questionModel.Type;
            set { _questionModel.Type = value; OnPropertyChanged(); }
        }
        

        [JsonPropertyName("difficulty")]
        public Difficulty Difficulty
        {
            get => _questionModel.Difficulty;
            set { _questionModel.Difficulty = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("category")]
        public string Category
        {
            get => _questionModel.Category;
            set { _questionModel.Category = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("question")]
        public string Question
        {
            get => _questionModel.Question;
            set { _questionModel.Question = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer
        {
            get => _questionModel.CorrectAnswer;
            set { _questionModel.CorrectAnswer = value; OnPropertyChanged(); }
        }


        [JsonPropertyName("incorrect_answers")]
        public string[] IncorrectAnswers
        {
            get => _questionModel.IncorrectAnswers;
            set { _questionModel.IncorrectAnswers = value; OnPropertyChanged(); }
        }


        public QuestionVM()
        {
            this._questionModel = new QuestionModel();
        }


        public QuestionVM(QuestionModel questionModel)
        {
            this._questionModel = questionModel;
        }
    }
}
