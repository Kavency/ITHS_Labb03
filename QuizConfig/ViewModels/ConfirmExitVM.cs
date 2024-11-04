using QuizConfig.Commands;
using QuizConfig.Views.Dialogs;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class ConfirmExitVM : Base
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

        private void CloseDialog(object? obj)
        {
            Application.Current.Windows.OfType<ConfirmExitDialog>().FirstOrDefault()?.Close();
        }


        private void ExitProgram(object? obj)
        {
            _mainVM.FileHandler.SaveToFile();
            Application.Current.Shutdown();
        }
    }
}
