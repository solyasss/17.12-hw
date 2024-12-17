using System;
using System.Windows.Input;

namespace hw.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _can_execute;

        public RelayCommand(Action execute, Func<bool> can_execute = null)
        {
            _execute = execute;
            _can_execute = can_execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_can_execute == null)
                return true;
            return _can_execute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}