﻿using FontAwesome.Sharp;
using QuizConfig.Commands;
using QuizConfig.Models;
using QuizConfig.Views.Dialogs;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QuizConfig.ViewModels
{
    internal class MenuVM : BaseVM
    {
        #region Properties
        public MainVM MainVM { get; set; }
        public RelayCommand SwitchViewCMD { get; }
        public RelayCommand SetActivePackCMD { get; }
        public RelayCommand MinimizeWindowCMD { get; }
        public RelayCommand MaximizeWindowCMD { get; }
        public RelayCommand ExitProgramCMD { get; }
        #endregion

        #region Constructor
        public MenuVM(MainVM mainVM)
        {
            this.MainVM = mainVM;

            this.SwitchViewCMD = new RelayCommand(SwitchView);
            this.SetActivePackCMD = new RelayCommand(SetActivePack);
            this.MaximizeWindowCMD = new RelayCommand(MaximizeWindow);
            this.MinimizeWindowCMD = new RelayCommand(MinimizeWindow);
            this.ExitProgramCMD = new RelayCommand(ExitProgram);
        }

        #endregion

        #region Methods
        private void SwitchView(object? obj)
        {
            if (MainVM.EditVisibility == Visibility.Visible)
            {
                MainVM.EditVisibility = Visibility.Hidden;
                MainVM.PlayVisibility = Visibility.Visible;
                MainVM.PlayVM.QuizStartViewVisibility = Visibility.Visible;
                MainVM.PlayVM.QuizViewVisibility = Visibility.Hidden;
                MainVM.PlayVM.QuizEndViewVisibility = Visibility.Hidden;
            }
            else
            {
                MainVM.EditVisibility = Visibility.Visible;
                MainVM.PlayVisibility = Visibility.Hidden;
            }
        }
        private void SetActivePack(object? obj)
        {
            MainVM.ActivePack = obj as QuestionPackVM;
            Debug.WriteLine($"{obj}");
        }


        private void MinimizeWindow(object obj)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.WindowState = WindowState.Minimized;
        }


        private void MaximizeWindow(object obj)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            if (mainWindow.WindowState == WindowState.Maximized)
            {
                var newIcon = new IconBlock { Icon = IconChar.WindowMaximize };

                mainWindow.WindowState = WindowState.Normal;
                mainWindow.Height = mainWindow.StartupHeight;
                mainWindow.Width = mainWindow.StartupWidth;
                mainWindow.Top = mainWindow.StartupTop;
                mainWindow.Left = mainWindow.StartupLeft;
            }
            else
            {
                var newIcon = new IconBlock { Icon = IconChar.WindowRestore };
                mainWindow.WindowState = WindowState.Maximized;
            }
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
