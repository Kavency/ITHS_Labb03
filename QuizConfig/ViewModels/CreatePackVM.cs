using QuizConfig.Commands;
using QuizConfig.MiscClasses;
using QuizConfig.Models;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class CreatePackVM : BaseVM
    {
        private string _name;
        private Difficulty _difficulty;
        private Difficulty _selectedDifficulty;
        private int _timeLimit;


        public MainVM MainVM { get; }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public Difficulty SelectedDifficulty { get => _selectedDifficulty; set { _selectedDifficulty = value; OnPropertyChanged(); } }
        public Difficulty Difficulty { get => _difficulty; set { _difficulty = value; OnPropertyChanged(); } }
        public int TimeLimit { get => _timeLimit; set { _timeLimit = value; OnPropertyChanged(); } }
        public RelayCommand CreateCMD { get; }
        public RelayCommand CancelCMD { get; }

        public CreatePackVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            this.CreateCMD = new RelayCommand(Create);
            this.CancelCMD = new RelayCommand(Cancel);
        }


        private async void Create(object? obj)
        {
            QuestionPackVM newPack = new QuestionPackVM(new QuestionPackModel(Name, TimeLimit, Difficulty));
            MainVM.QuestionPacks.Add(newPack);
            MainVM.ActivePack = newPack;
            await MainVM.FileHandler.SaveToFileAsync();
            Cancel(obj);
        }
        private void Cancel(object? obj)
        {
            Application.Current.Windows.OfType<CreateNewPackDialog>().FirstOrDefault()?.Close();
        }
    }
}
