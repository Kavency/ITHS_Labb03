using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using QuizConfig.MiscClasses;
using QuizConfig.ViewModels;

namespace QuizConfig.Models
{
    internal class QuestionPackModel : BaseVM
    {
        #region Properties
        [JsonPropertyName("Name")]
        public string Name { get; set; }


        [JsonPropertyName("TimeLimit")]
        public int TimeLimit { get; set; }


        [JsonPropertyName("Difficulty")]
        [JsonConverter(typeof(JsonDifficultyConverter))]
        public Difficulty Difficulty { get; set; }


        [JsonPropertyName("Questions")]
        public ObservableCollection<QuestionVM> Questions { get; set; }
        #endregion


        #region Constructors
        public QuestionPackModel()
        {
            this.Questions = new ObservableCollection<QuestionVM>();
        }

        public QuestionPackModel(string name, int timeLimit = 30, Difficulty difficulty = Difficulty.Medium)
        {
            this.Questions = new ObservableCollection<QuestionVM>();

            this.Name = name;
            this.TimeLimit = timeLimit;
            this.Difficulty = difficulty;
        }
        #endregion
    }
}
