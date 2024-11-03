using QuizConfig.Commands;
using QuizConfig.Views.Dialogs;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QuizConfig.ViewModels
{
    internal class MenuVM : Base
    {
        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand SwitchViewCMD { get; }
        public RelayCommand ExitProgramCMD { get; }
        #endregion

        #region Constructor
        public MenuVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            SwitchViewCMD = new RelayCommand(SwitchView);
            ExitProgramCMD = new RelayCommand(ExitProgram);
        }

        #endregion

        #region Methods
        private void SwitchView(object? obj)
        {
            MenuItem menuItem = obj as MenuItem;

            if (MainVM.EditVisibility == Visibility.Visible)
            {
                MainVM.EditVisibility = Visibility.Collapsed;
                MainVM.PlayVisibility = Visibility.Visible;
            }
            else
            {
                MainVM.EditVisibility = Visibility.Visible;
                MainVM.PlayVisibility = Visibility.Collapsed;
            }
        }

        private void ExitProgram(object? obj)
        {
            //throw new NotImplementedException();
            ConfirmExitDialog exitDialog = new ConfirmExitDialog();
            exitDialog.Owner = Application.Current.MainWindow;
            exitDialog.ShowDialog();
        }
        #endregion
    }
}
