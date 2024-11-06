using System.Collections.ObjectModel;
using QuizConfig.MiscClasses;
using QuizConfig.ViewModels;

namespace QuizConfig.Models
{
    internal class QuestionPackModel : BaseVM
    {
        #region Properties
        public string Name { get; set; }
        public int TimeLimit { get; set; }
        public Difficulty Difficulty { get; set; }
        public ObservableCollection<QuestionVM> Questions { get; set; }
        #endregion


        #region Constructors
        public QuestionPackModel()
        {
            this.Questions = new ObservableCollection<QuestionVM>();
        }

        public QuestionPackModel(string name, int timeLimit = 30, Difficulty difficulty = Difficulty.medium)
        {
            this.Questions = new ObservableCollection<QuestionVM>();

            this.Name = name;
            this.TimeLimit = timeLimit;
            this.Difficulty = difficulty;
        }
        #endregion
    }
}
