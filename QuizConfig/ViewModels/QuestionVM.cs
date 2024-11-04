using QuizConfig.Models;

namespace QuizConfig.ViewModels
{
    internal class QuestionVM : BaseVM
    {
        private readonly QuestionModel _questionModel;

        #region Properties
        public string Type => _questionModel.Type;
        public string Difficulty => _questionModel.Difficulty;
        public string Category => _questionModel.Category;
        public string Question => _questionModel.Question;
        public string CorrectAnswer => _questionModel.CorrectAnswer;
        public string[] IncorrectAnswers => _questionModel.IncorrectAnswers;
        #endregion

        public QuestionVM(QuestionModel questionModel)
        {
            this._questionModel = questionModel;
        }
    }
}
