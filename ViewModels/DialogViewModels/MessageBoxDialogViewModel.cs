using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class MessageBoxDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private string message;
        private string windowTitle;
        
        public RelayCommand ConfirmCommand { get; }

        // Bound to the TextBlock in the MessageBoxDialogView.
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; }
        }

        public MessageBoxDialogViewModel(string message, string windowTitle)
        {
            Message = message;
            WindowTitle = windowTitle;
            ConfirmCommand = new RelayCommand(Confirm);
        }

        private void Confirm()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
