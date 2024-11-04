using QuizConfig.Commands;
using QuizConfig.Models;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class OptionsDialogVM : BaseVM
    {
        public MainVM MainVM { get; set; }

        public string Name { get; set; }
        public string Difficulty { get; set; }
        public int TimeLimit { get; set; }
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
            TimeLimit = MainVM.ActivePack.TimeLimit;

            OptionsDialog optionsDialog = new OptionsDialog();
            optionsDialog.ShowDialog();
        }

        private async void Update(object? obj)
        {
            QuestionPackVM newPack = new QuestionPackVM(new QuestionPackModel(Name, TimeLimit, Difficulty));
            MainVM.ActivePack = newPack;
            await MainVM.FileHandler.SaveToFileAsync();
            Cancel(obj);
        }


        private void Cancel(object? obj)
        {
            Application.Current.Windows.OfType<OptionsDialog>().FirstOrDefault()?.Close();
        }
    }
}
