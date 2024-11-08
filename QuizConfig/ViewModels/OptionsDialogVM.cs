using QuizConfig.Commands;
using QuizConfig.MiscClasses;
using QuizConfig.Models;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class OptionsDialogVM : BaseVM
    {
        private string _name;
        private Difficulty _difficulty;
        private Difficulty _selectedDifficulty;
        private int _timeLimit;

        public MainVM MainVM { get; set; }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public Difficulty SelectedDifficulty { get => _selectedDifficulty; set { _selectedDifficulty = value; OnPropertyChanged(); } }
        public Difficulty Difficulty { get => _difficulty; set { _difficulty = value; OnPropertyChanged(); } }
        public int TimeLimit { get => _timeLimit; set { _timeLimit = value; OnPropertyChanged(); } }
        public RelayCommand OpenOptionsCMD { get; }
        public RelayCommand UpdateCMD { get; }
        public RelayCommand CancelCMD { get; }


        public OptionsDialogVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            this.OpenOptionsCMD = new RelayCommand(OpenOptions);
            this.UpdateCMD = new RelayCommand(Update);
            this.CancelCMD = new RelayCommand(Cancel);
        }


        private void OpenOptions(object? obj)
        {
            Name = MainVM.ActivePack.Name;
            Difficulty = MainVM.ActivePack.Difficulty;
            SelectedDifficulty = MainVM.ActivePack.Difficulty;
            TimeLimit = MainVM.ActivePack.TimeLimit;

            OptionsDialog optionsDialog = new OptionsDialog();
            optionsDialog.ShowDialog();
        }


        private async void Update(object? obj)
        {
            this.MainVM.ActivePack.Name = Name;
            this.MainVM.ActivePack.Difficulty = SelectedDifficulty;
            this.MainVM.ActivePack.TimeLimit = TimeLimit;
            await MainVM.FileHandler.SaveToFileAsync();
            Cancel(obj);
        }


        private void Cancel(object? obj)
        {
            Application.Current.Windows.OfType<OptionsDialog>().FirstOrDefault()?.Close();
        }
    }
}
