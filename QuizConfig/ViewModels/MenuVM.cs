using QuizConfig.Commands;
using QuizConfig.Models;
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
        public RelayCommand SetActivePackCMD { get; private set; }
        #endregion

        #region Constructor
        public MenuVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            this.SwitchViewCMD = new RelayCommand(SwitchView);
            this.SetActivePackCMD = new RelayCommand(SetActivePack);
            this.ExitProgramCMD = new RelayCommand(ExitProgram);
        }

        #endregion

        #region Methods
        private void SwitchView(object? obj)
        {
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
        private void SetActivePack(object? obj)
        {
            MainVM.ActivePack = obj as QuestionPackModel;
            Debug.WriteLine($"{obj}");
        }
        private void ExitProgram(object? obj)
        {
            ConfirmExitDialog exitDialog = new ConfirmExitDialog();
            exitDialog.Owner = Application.Current.MainWindow;
            exitDialog.ShowDialog();
        }
        #endregion
    }
}
