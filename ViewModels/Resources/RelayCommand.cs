using System;
using System.Windows.Input;

namespace ViewModels
{
    // This class is used to delegate commands from the Views to the ViewModels.
    // Is used instead of implementing the ICommand interface for each view model.
    public class RelayCommand : ICommand
    {
        private Action action;

        // This event handler is implemented from the ICommand interface. 
        // In our case it is never used as CanExecute always returns true.
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action action)
        {
            this.action = action;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}