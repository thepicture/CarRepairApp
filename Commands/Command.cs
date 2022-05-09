using System;
using System.Windows.Input;

namespace CarRepairApp.Commands
{
    public class Command : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public Command(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public Command(Action<object> execute) : this(execute, null) { }
        public Command(Action execute)
        {
            this.execute = (obj) =>
            {
                execute();
            };
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        public bool CanExecute<T>(T parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute<T>(T parameter)
        {
            execute(parameter);
        }
    }
}
