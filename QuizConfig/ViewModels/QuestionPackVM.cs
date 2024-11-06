using QuizConfig.MiscClasses;
using QuizConfig.Models;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace QuizConfig.ViewModels
{
    internal class QuestionPackVM : BaseVM
    {
        [JsonIgnore]
        private readonly QuestionPackModel _questionPackModel;


        [JsonPropertyName("name")]
        public string Name 
        { 
            get => _questionPackModel.Name; 
            set { _questionPackModel.Name = value; OnPropertyChanged(); } 
        }


        [JsonPropertyName("timeLimit")]
        public int TimeLimit 
        { 
            get => _questionPackModel.TimeLimit; 
            set { _questionPackModel.TimeLimit = value; OnPropertyChanged(); } 
        }


        [JsonPropertyName("difficulty")]
        [JsonConverter(typeof(JsonDifficultyConverter))]
        public Difficulty Difficulty 
        { 
            get => _questionPackModel.Difficulty; 
            set { _questionPackModel.Difficulty = value; OnPropertyChanged(); } 
        }


        [JsonPropertyName("questions")]
        public ObservableCollection<QuestionVM> Questions 
        { 
            get => _questionPackModel.Questions; 
            set { _questionPackModel.Questions = value; OnPropertyChanged();  } 
        }

        public QuestionPackVM()
        {
            this._questionPackModel = new QuestionPackModel();
        }
        public QuestionPackVM(QuestionPackModel questionPackModel)
        {
            this._questionPackModel = questionPackModel;
        }
    }
}
