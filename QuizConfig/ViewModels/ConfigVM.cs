using QuizConfig.Commands;
using QuizConfig.Models;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class ConfigVM : BaseVM
    {
        #region Fields
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
        private QuestionVM? _selectedQuestion = null;
        #endregion

        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand AddPackCMD { get; }
        public RelayCommand DeletePackCMD { get; }
        public RelayCommand QuestionFormVisibilityCMD { get; }
        public RelayCommand SaveAndCloseButtonCMD { get; }
        public RelayCommand SaveAndAddButtonCMD { get; }
        public RelayCommand UpdateQuestionCMD { get; }
        public RelayCommand CancelCMD { get; }
        public RelayCommand DeleteQuestionCMD { get; }
        public RelayCommand AddNewQuestionCMD { get; }

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
            set { _selectedQuestionIndex = value; OnPropertyChanged(); }
        }
        public QuestionVM? SelectedQuestion
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


        #region Contsructor
        public ConfigVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            this.AddPackCMD = new RelayCommand(AddPack);
            this.DeletePackCMD = new RelayCommand(DeletePack);
            this.QuestionFormVisibilityCMD = new RelayCommand(ChangeQuestionFormVisibility);
            this.SaveAndCloseButtonCMD = new RelayCommand(AddQuestionAndClose);
            this.SaveAndAddButtonCMD = new RelayCommand(AddQuestionAndClear);
            this.UpdateQuestionCMD = new RelayCommand(UpdateQuestion);
            this.CancelCMD = new RelayCommand(CancelInput);
            this.DeleteQuestionCMD = new RelayCommand(DeleteQuestion, CanDeleteQuestion);
            this.AddNewQuestionCMD = new RelayCommand(AddQuestion);
        }
     

        private void AddQuestion(object? obj)
        {
            QuestionFormVisibility = Visibility.Visible;
            ClearTextBoxes();
        }
        #endregion


        #region Methods
        private async void DeleteQuestion(object? obj)
        {
            MainVM.ActivePack.Questions.Remove(SelectedQuestion);
            await MainVM.FileHandler.SaveToFileAsync();
        }

        private bool CanDeleteQuestion(object? arg)
        {
            return true;
            //if (_selectedQuestion is not null)
            //    return true;
            //else
            //    return false;
        }

        private void CancelInput(object? obj)
        {
            ClearTextBoxes();
            QuestionFormVisibility = Visibility.Hidden;
            SelectedQuestion = null;
            SelectedQestionIndex = null;
        }

        private async void UpdateQuestion(object? obj)
        {
            SelectedQuestion.Question = QuestionTextBox;
            SelectedQuestion.CorrectAnswer = CorrectAnswerTextBox;
            SelectedQuestion.IncorrectAnswers[0] = WrongAnswer1TextBox;
            SelectedQuestion.IncorrectAnswers[1] = WrongAnswer2TextBox;
            SelectedQuestion.IncorrectAnswers[2] = WrongAnswer3TextBox;
            await MainVM.FileHandler.SaveToFileAsync();
            CancelInput(obj);
        }

        private async void AddQuestionAndClear(object? obj)
        {
            // TODO: Check if textboxes are empty
            QuestionVM newQuestion = new QuestionVM(new QuestionModel(QuestionTextBox, CorrectAnswerTextBox, WrongAnswer1TextBox, WrongAnswer2TextBox, WrongAnswer3TextBox));
            MainVM.ActivePack.Questions.Add(newQuestion);
            await MainVM.FileHandler.SaveToFileAsync();
            ClearTextBoxes();
        }

        private async void AddQuestionAndClose(object? obj)
        {
            QuestionVM newQuestion = new QuestionVM(new QuestionModel(QuestionTextBox, CorrectAnswerTextBox, WrongAnswer1TextBox, WrongAnswer2TextBox, WrongAnswer3TextBox));
            MainVM.ActivePack.Questions.Add(newQuestion);
            await MainVM.FileHandler.SaveToFileAsync();
            CancelInput(obj);
        }

        private void ChangeQuestionFormVisibility(object? obj)
        {
            throw new NotImplementedException();
        }

        private async void AddPack(object? obj)
        {
            CreateNewPackDialog createPackDialog = new CreateNewPackDialog();
            createPackDialog.ShowDialog();
        }

        private async void DeletePack(object? obj)
        {
            MainVM.QuestionPacks.Remove(MainVM.ActivePack);
            if (MainVM.QuestionPacks.Count > 0)
                MainVM.ActivePack = MainVM.QuestionPacks.First();
            else
                MainVM.ActivePack = null;
            await MainVM.FileHandler.SaveToFileAsync();

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
