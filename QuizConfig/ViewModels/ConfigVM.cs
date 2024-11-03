﻿using QuizConfig.Commands;
using QuizConfig.Models;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class ConfigVM : VMBase
    {
        #region Fields
        private readonly MainVM _mainVM;
        private string _questionTextBox;
		private string _correctAnswerTextBox;
		private string _wrongAnswer1TextBox;
        private string _wrongAnswer2TextBox;
        private string _wrongAnswer3TextBox;
        private Visibility _QuestionFormVisibility = Visibility.Visible;
        private Visibility _saveAndCloseButtonVisibility = Visibility.Visible;
        private Visibility _saveAndAddButtonVisibility = Visibility.Visible;
        private Visibility _updateButtonVisibility = Visibility.Collapsed;
        private int _selectedQuestionIndex;
        private int _selectedMenuItem;
        private QuestionModel _selectedQuestion;

        public RelayCommand AddPackCMD { get; }
        public RelayCommand DeletePackCMD { get; }
        #endregion

        #region Properties
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
        public int SelectedQestionIndex
        {
            get { return _selectedQuestionIndex; }
            set { _selectedQuestionIndex = value; }
        }
        public QuestionModel SelectedQuestion
        {
            get { return _selectedQuestion; }
            set { _selectedQuestion = value; }
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
        }
        #endregion


        #region Methods
        private void DeletePack(object? obj)
        {
            _mainVM.QuestionPacks.Remove(_mainVM.ActivePack);
            if (_mainVM.QuestionPacks.Count > 0)
                _mainVM.ActivePack = _mainVM.QuestionPacks.First();
            else
                _mainVM.ActivePack = null;
        }

        private void AddPack(object? obj)
        {
            QuestionPackModel newPack = new QuestionPackModel() { Name = $"Added with button" };
            _mainVM.QuestionPacks.Add(newPack);
            _mainVM.ActivePack = newPack;
        }
        #endregion
    }
}