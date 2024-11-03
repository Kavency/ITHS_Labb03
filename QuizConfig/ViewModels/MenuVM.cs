using QuizConfig.Commands;
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
        #endregion

        #region Constructor
        public MenuVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            SwitchViewCMD = new RelayCommand(SwitchView);
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
        #endregion
    }
}
