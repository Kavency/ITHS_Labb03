using QuizConfig.Commands;
using System.Windows;

namespace QuizConfig.ViewModels
{
    internal class ConfirmExitVM : Base
    {
        private readonly MainVM _mainVM;
        public ConfirmExitVM(MainVM mainVM)
        {
            this._mainVM = mainVM;
            ExitProgramCMD = new RelayCommand(Answer);
        }

        public RelayCommand ExitProgramCMD { get; }

        private void Answer(object? obj)
        {
            if (obj is Window window)
                window.Close();
            else
            {
                // TODO: Save to File
                Application.Current.Shutdown();
            }
        }
    }
}
