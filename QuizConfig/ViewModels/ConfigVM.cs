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
        private int _selectedMenuItem;
        private QuestionVM? _selectedQuestion = null;
        #endregion


        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand AddPackCMD { get; }
        public RelayCommand DeletePackCMD { get; }
        public RelayCommand AddQuestionCMD { get; }
        public RelayCommand DeleteQuestionCMD { get; }
        public RelayCommand SaveAndCloseButtonCMD { get; }
        public RelayCommand SaveAndAddButtonCMD { get; }
        public RelayCommand UpdateQuestionCMD { get; }
        public RelayCommand CancelCMD { get; }

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
        public QuestionVM? SelectedQuestion
        {
            get { return _selectedQuestion; }
            set 
            { 
                _selectedQuestion = value;
                QuestionFormVisibility = SelectedQuestion != null ? Visibility.Visible : Visibility.Hidden;
                SaveAndCloseButtonVisibility = SelectedQuestion != null ? Visibility.Collapsed : Visibility.Visible;
                SaveAndAddButtonVisibility = SelectedQuestion != null ? Visibility.Collapsed : Visibility.Visible;
                UpdateButtonVisibility = SelectedQuestion != null ? Visibility.Visible : Visibility.Collapsed;

                if(SelectedQuestion is not null)
                {
                    QuestionTextBox = SelectedQuestion.Question;
                    CorrectAnswerTextBox = SelectedQuestion.CorrectAnswer;
                    WrongAnswer1TextBox = SelectedQuestion.IncorrectAnswers[0];
                    WrongAnswer2TextBox = SelectedQuestion.IncorrectAnswers[1];
                    WrongAnswer3TextBox = SelectedQuestion.IncorrectAnswers[2];
                }
                OnPropertyChanged();
                DeleteQuestionCMD.RaiseCanExecuteChanged();
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
            this.DeletePackCMD = new RelayCommand(DeletePack, CanDeletePack);
            this.SaveAndCloseButtonCMD = new RelayCommand(AddQuestionAndClose);
            this.SaveAndAddButtonCMD = new RelayCommand(AddQuestionAndClear);
            this.UpdateQuestionCMD = new RelayCommand(UpdateQuestion);
            this.CancelCMD = new RelayCommand(CancelInput);
            this.DeleteQuestionCMD = new RelayCommand(DeleteQuestion, CanDeleteQuestion);
            this.AddQuestionCMD = new RelayCommand(AddQuestion);
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


        private bool CanDeleteQuestion(object? arg) => SelectedQuestion is not null ? true : false;


        private void CancelInput(object? obj)
        {
            ClearTextBoxes();
            QuestionFormVisibility = Visibility.Hidden;
            SelectedQuestion = null;
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
            {
                MainVM.ActivePack = null;
            }

            await MainVM.FileHandler.SaveToFileAsync();
        }


        private bool CanDeletePack(object? arg)
        {
            if (MainVM.QuestionPacks is not null || MainVM.QuestionPacks.Count > 1)
                return true;
            else
                return false;
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
