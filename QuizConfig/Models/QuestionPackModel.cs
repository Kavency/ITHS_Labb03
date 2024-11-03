using System.Collections.ObjectModel;

namespace QuizConfig.Models
{
    internal class QuestionPackModel
    {
        #region Properties
        public string Name { get; set; }
        public int TimeLimit { get; set; }
        public string Difficulty { get; set; }
        public ObservableCollection<QuestionModel> Questions { get; set; }
        #endregion


        #region Constructors
        public QuestionPackModel()
        {
            this.Questions = new ObservableCollection<QuestionModel>();
        }

        public QuestionPackModel(string name, int timeLimit = 30, string difficulty = "Medium")
        {
            this.Questions = new ObservableCollection<QuestionModel>();

            this.Name = name;
            this.TimeLimit = timeLimit;
            this.Difficulty = difficulty;
        }
        #endregion
    }
}
