using QuizConfig.Commands;
using QuizConfig.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class MainVM : Base
    {
        #region Fields
        private QuestionPackModel _activePack;
        private Visibility _editVisibility = Visibility.Visible;
        private Visibility _playVisibility = Visibility.Collapsed;
        #endregion

        #region Properties
        public MenuVM MenuVM { get; set; }
        public ConfigVM ConfigVM { get; }
        public Visibility EditVisibility
        {
            get => _editVisibility;
            set { _editVisibility = value; OnPropertyChanged(); }
        }
        public Visibility PlayVisibility
        {
            get { return _playVisibility; }
            set { _playVisibility = value; OnPropertyChanged(); }
        }
        public RelayCommand SetActivePackCMD { get; } // Flytta till menuVM? eller kanske inte?
        public ObservableCollection<QuestionPackModel> QuestionPacks { get; set; }
        public QuestionPackModel ActivePack
        {
            get => _activePack;
            set { _activePack = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor
        public MainVM()
        {
            this.MenuVM = new MenuVM(this);
            this.ConfigVM = new ConfigVM(this);
            this.QuestionPacks = new ObservableCollection<QuestionPackModel>();

            this.SetActivePackCMD = new RelayCommand(SetActivePack);

            
            for (int i = 0; i < 5; i++)
            {
                QuestionPacks.Add(new QuestionPackModel() { Name = $"Pack {i + 1}" });
                Debug.WriteLine($"{QuestionPacks[i].Name} was added.");
                for (int j = 0; j < 5; j++)
                {
                    QuestionPacks[i].Questions.Add(new QuestionModel("What year?", "1981", "1977", "1985","1976"));
                    Debug.WriteLine($"Question {j + 1} added");
                }
            }


            this.ActivePack = this.QuestionPacks.First();
            
            this.MenuVM = new MenuVM(this);
            this.ConfigVM = new ConfigVM(this);

            this.SetActivePackCMD = new RelayCommand(SetActivePack);




        }
        #endregion

        #region Methods
        private void SetActivePack(object? obj)
        {
            ActivePack = obj as QuestionPackModel;
            Debug.WriteLine($"{obj}");
        }
        #endregion
    }
}
