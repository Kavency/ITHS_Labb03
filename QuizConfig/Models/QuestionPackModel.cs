using System.Collections.ObjectModel;

namespace QuizConfig.Models
{
    internal class QuestionPackModel
    {
        public string Name { get; set; }
        public ObservableCollection<QuestionModel> Questions { get; set; }

        public QuestionPackModel(QuestionModel questionModel)
        {
            this.Questions = new ObservableCollection<QuestionModel>();
        }
    }
}
