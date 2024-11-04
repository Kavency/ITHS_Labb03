using QuizConfig.Models;
using System.Collections.ObjectModel;

namespace QuizConfig.ViewModels
{
    internal class QuestionPackVM
    {
        private readonly QuestionPackModel _questionPackModel;

        public string Name => _questionPackModel.Name;
        public int TimeLimit => _questionPackModel.TimeLimit;
        public string Difficulty => _questionPackModel.Difficulty;
        public ObservableCollection<QuestionModel> Questions => _questionPackModel.Questions;


        public QuestionPackVM(QuestionPackModel questionPackModel)
        {
            this._questionPackModel = questionPackModel;
        }
    }
}
