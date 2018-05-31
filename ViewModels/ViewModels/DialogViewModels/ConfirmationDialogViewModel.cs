using System;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class ConfirmationDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private string message;

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public ConfirmationDialogViewModel(string message)
        {
            Message = message;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Confirm()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        private void Cancel()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }


    }
}
