using QuizConfig.MiscClasses;
using QuizConfig.Models;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace QuizConfig.ViewModels
{
    internal class QuestionPackVM
    {
        [JsonPropertyName("QuestionPackModel")]
        public QuestionPackModel QuestionPackModel { get; set; }


        [JsonPropertyName("Name")]
        public string Name => QuestionPackModel.Name;


        [JsonPropertyName("TimeLimit")]
        public int TimeLimit => QuestionPackModel.TimeLimit;


        [JsonPropertyName("Difficulty")]
        public Difficulty Difficulty => QuestionPackModel.Difficulty;


        [JsonPropertyName("Questions")]
        public ObservableCollection<QuestionVM> Questions => QuestionPackModel.Questions;

        public QuestionPackVM()
        {
            this.QuestionPackModel = new QuestionPackModel();
        }
        public QuestionPackVM(QuestionPackModel questionPackModel)
        {
            this.QuestionPackModel = questionPackModel;
        }
    }
}
