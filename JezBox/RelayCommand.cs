using System;
using System.Windows.Input;

namespace JezBox
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExceute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExceute)
        {
            this._execute = execute;
            this._canExceute = canExceute ?? (_ => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExceute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}