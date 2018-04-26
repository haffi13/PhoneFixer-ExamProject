using System;
using System.Windows.Input;

namespace ViewModels
{
    // This class is used to relay commands from the Views to the ViewModels.
    // Is used instead of inheriting the ICommand interface for each view model.
    public class RelayCommand : ICommand
    {
        private Action action;
        public RelayCommand(Action action)
        {
            this.action = action;
        }

        // REVIEW!! !! ! !!  ! https://www.codeproject.com/Tips/813345/Basic-MVVM-and-ICommand-Usage-Example
        public event EventHandler CanExecuteChanged = (sender, e) => { };   //--Seems to work without the sender..is never used in current solution
        //only the CanExecute and Execute are used.
        
        // Could this method be used to make StartSelection available in MainWindow.
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