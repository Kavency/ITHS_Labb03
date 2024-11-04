using QuizConfig.Commands;
using QuizConfig.Models;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class CreatePackVM : BaseVM
    {
        public MainVM MainVM { get; }

        public string Name { get; set; }
        public string Difficulty { get; set; } = "Medium";
        public int TimeLimit { get; set; } = 30;

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
