using QuizConfig.MiscClasses;
using QuizConfig.Models;
using System.Text.Json.Serialization;

namespace QuizConfig.ViewModels
{
    internal class QuestionVM : BaseVM
    {
        [JsonIgnore]
        public QuestionModel QuestionModel { get; set; }
   
        public string Type
        {
            get => QuestionModel.Type;
            set { QuestionModel.Type = value; OnPropertyChanged(); }
        }
        public Difficulty Difficulty
        {
            get => QuestionModel.Difficulty;
            set { QuestionModel.Difficulty = value; OnPropertyChanged(); }
        }
        public string Category
        {
            get => QuestionModel.Category;
            set { QuestionModel.Category = value; OnPropertyChanged(); }
        }
        public string Question
        {
            get => QuestionModel.Question;
            set { QuestionModel.Question = value; OnPropertyChanged(); }
        }
        public string CorrectAnswer
        {
            get => QuestionModel.CorrectAnswer;
            set { QuestionModel.CorrectAnswer = value; OnPropertyChanged(); }
        }
        public string[] IncorrectAnswers
        {
            get => QuestionModel.IncorrectAnswers;
            set { QuestionModel.IncorrectAnswers = value; OnPropertyChanged(); }
        }
        public QuestionVM()
        {
            this.QuestionModel = new QuestionModel();
        }
        public QuestionVM(QuestionModel questionModel)
        {
            this.QuestionModel = questionModel;
        }
    }
}
