using QuizConfig.Commands;
using QuizConfig.Models;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class ConfigVM : Base
    {
        #region Fields
        private readonly MainVM _mainVM;
        private string _questionTextBox;
		private string _correctAnswerTextBox;
		private string _wrongAnswer1TextBox;
        private string _wrongAnswer2TextBox;
        private string _wrongAnswer3TextBox;
        private Visibility _QuestionFormVisibility = Visibility.Hidden;
        private Visibility _saveAndCloseButtonVisibility = Visibility.Visible;
        private Visibility _saveAndAddButtonVisibility = Visibility.Visible;
        private Visibility _updateButtonVisibility = Visibility.Collapsed;
        private int? _selectedQuestionIndex = null;
        private int _selectedMenuItem;
        private QuestionModel _selectedQuestion = null;
        #endregion

        #region Properties
        public RelayCommand AddPackCMD { get; }
        public RelayCommand DeletePackCMD { get; }
        public RelayCommand QuestionFormVisibilityCMD { get; }
        public RelayCommand SaveAndCloseButtonCMD { get; }
        public RelayCommand SaveAndAddButtonCMD { get; }
        public RelayCommand UpdateQuestionCMD { get; }
        public RelayCommand CancelCMD { get; }
        public RelayCommand DeleteQuestionCMD { get; }
        public string QuestionTextBox
		{
			get { return _questionTextBox; }
			set { _questionTextBox = value; OnPropertyChanged(); }
		}
		public string CorrectAnswerTextBox
		{
			get { return _correctAnswerTextBox; }
			set { _correctAnswerTextBox = value; OnPropertyChanged(); }
		}
		public string WrongAnswer1TextBox
		{
			get { return _wrongAnswer1TextBox; }
			set { _wrongAnswer1TextBox = value; OnPropertyChanged(); }
		}
        public string WrongAnswer2TextBox
        {
            get { return _wrongAnswer2TextBox; }
            set { _wrongAnswer2TextBox = value; OnPropertyChanged(); }
        }
        public string WrongAnswer3TextBox
        {
            get { return _wrongAnswer3TextBox; }
            set { _wrongAnswer3TextBox = value; OnPropertyChanged(); }
        }
		public Visibility QuestionFormVisibility
        {
			get { return _QuestionFormVisibility; }
			set { _QuestionFormVisibility = value; OnPropertyChanged(); }
		}
        public Visibility SaveAndCloseButtonVisibility
        {
            get { return _saveAndCloseButtonVisibility; }
            set { _saveAndCloseButtonVisibility = value; OnPropertyChanged(); }
        }
        public Visibility SaveAndAddButtonVisibility
        {
            get { return _saveAndAddButtonVisibility; }
            set { _saveAndAddButtonVisibility = value; OnPropertyChanged(); }
        }
        public Visibility UpdateButtonVisibility
        {
            get { return _updateButtonVisibility; }
            set { _updateButtonVisibility = value; OnPropertyChanged(); }
        }
        public int? SelectedQestionIndex
        {
            get { return _selectedQuestionIndex; }
            set { _selectedQuestionIndex = value; }
        }
        public QuestionModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set 
            { 
                _selectedQuestion = value;
                QuestionFormVisibility = _selectedQuestion != null ? Visibility.Visible : Visibility.Hidden;
                SaveAndCloseButtonVisibility = _selectedQuestion != null ? Visibility.Collapsed : Visibility.Visible;
                SaveAndAddButtonVisibility = _selectedQuestion != null ? Visibility.Collapsed : Visibility.Visible;
                UpdateButtonVisibility = _selectedQuestion != null ? Visibility.Visible : Visibility.Collapsed;

                if(_selectedQuestion is not null)
                {
                    QuestionTextBox = _selectedQuestion.Question;
                    CorrectAnswerTextBox = _selectedQuestion.CorrectAnswer;
                    WrongAnswer1TextBox = _selectedQuestion.IncorrectAnswers[0];
                    WrongAnswer2TextBox = _selectedQuestion.IncorrectAnswers[1];
                    WrongAnswer3TextBox = _selectedQuestion.IncorrectAnswers[2];
                }
                OnPropertyChanged();
            }
        }
        public int SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { _selectedMenuItem = value; OnPropertyChanged(); }
        }
        #endregion


        #region Contructor
        public ConfigVM(MainVM mainVM)
        {
            this._mainVM = mainVM;

            this.AddPackCMD = new RelayCommand(AddPack);
            this.DeletePackCMD = new RelayCommand(DeletePack);
            this.QuestionFormVisibilityCMD = new RelayCommand(ChangeQuestionFormVisibility);
            this.SaveAndCloseButtonCMD = new RelayCommand(AddQuestionAndClose);
            this.SaveAndAddButtonCMD = new RelayCommand(AddQuestionAndClear);
            this.UpdateQuestionCMD = new RelayCommand(UpdateQuestion);
            this.CancelCMD = new RelayCommand(CancelInput);
            this.DeleteQuestionCMD = new RelayCommand(DeleteQuestion, CanDeleteQuestion);
        }
        #endregion


        #region Methods
        private void DeleteQuestion(object? obj)
        {
            _mainVM.ActivePack.Questions.Remove(SelectedQuestion);
            _mainVM.FileHandler.SaveToFile();
        }

        private bool CanDeleteQuestion(object? arg)
        {
            if (_selectedQuestion is not null)
                return true;
            else
                return false;
        }

        private void CancelInput(object? obj)
        {
            ClearTextBoxes();
            QuestionFormVisibility = Visibility.Hidden;
            SelectedQuestion = null;
            SelectedQestionIndex = null;
        }

        private void UpdateQuestion(object? obj)
        {
            SelectedQuestion.Question = QuestionTextBox;
            SelectedQuestion.CorrectAnswer = CorrectAnswerTextBox;
            SelectedQuestion.IncorrectAnswers[0] = WrongAnswer1TextBox;
            SelectedQuestion.IncorrectAnswers[1] = WrongAnswer1TextBox;
            SelectedQuestion.IncorrectAnswers[2] = WrongAnswer2TextBox;
            _mainVM.FileHandler.SaveToFile();
            CancelInput(obj);
        }

        private void AddQuestionAndClear(object? obj)
        {
            // TODO: Check if textboxes are empty
            QuestionModel newQuestion = new QuestionModel(QuestionTextBox, CorrectAnswerTextBox, WrongAnswer1TextBox, WrongAnswer2TextBox, WrongAnswer3TextBox);
            _mainVM.ActivePack.Questions.Add(newQuestion);
            _mainVM.FileHandler.SaveToFile();
            ClearTextBoxes();
        }

        private void AddQuestionAndClose(object? obj)
        {
            QuestionModel newQuestion = new QuestionModel(QuestionTextBox, CorrectAnswerTextBox, WrongAnswer1TextBox, WrongAnswer2TextBox, WrongAnswer3TextBox);
            _mainVM.ActivePack.Questions.Add(newQuestion);
            _mainVM.FileHandler.SaveToFile();
            CancelInput(obj);
        }

        private void ChangeQuestionFormVisibility(object? obj)
        {
            throw new NotImplementedException();
        }

        private void AddPack(object? obj)
        {
            QuestionPackModel newPack = new QuestionPackModel() { Name = $"Added with button" };
            _mainVM.QuestionPacks.Add(newPack);
            _mainVM.ActivePack = newPack;
            _mainVM.FileHandler.SaveToFile();
        }

        private void DeletePack(object? obj)
        {
            _mainVM.QuestionPacks.Remove(_mainVM.ActivePack);
            if (_mainVM.QuestionPacks.Count > 0)
                _mainVM.ActivePack = _mainVM.QuestionPacks.First();
            else
                _mainVM.ActivePack = null;
            _mainVM.FileHandler.SaveToFile();

        }

        private void ClearTextBoxes()
        {
            QuestionTextBox = string.Empty;
            CorrectAnswerTextBox = string.Empty;
            WrongAnswer1TextBox = string.Empty;
            WrongAnswer2TextBox = string.Empty;
            WrongAnswer3TextBox = string.Empty;
        }
        #endregion
    }
}
