﻿using System.Windows.Input;

namespace QuizConfig.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool> canExecute;

        public event EventHandler? CanExecuteChanged;


        public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);

            this.execute = execute;
            this.canExecute = canExecute;
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


        public bool CanExecute(object? parameter)
        {
            return canExecute is null || canExecute(parameter);
        }


        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
