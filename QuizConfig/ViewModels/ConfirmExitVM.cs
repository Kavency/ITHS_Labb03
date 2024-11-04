using QuizConfig.Commands;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class ConfirmExitVM : BaseVM
    {
        private readonly MainVM _mainVM;
        public RelayCommand ExitProgramCMD { get; }
        public RelayCommand CloseDialogCMD { get; }
        
        
        public ConfirmExitVM(MainVM mainVM)
        {
            this._mainVM = mainVM;
            ExitProgramCMD = new RelayCommand(ExitProgram);
            CloseDialogCMD = new RelayCommand(CloseDialog);
        }


        private async void ExitProgram(object? obj)
        {
            await _mainVM.FileHandler.SaveToFileAsync();
            CloseDialog(obj);
        }
        private void CloseDialog(object? obj)
        {
            Application.Current.Windows.OfType<ConfirmExitDialog>().FirstOrDefault()?.Close();
        }
    }
}
