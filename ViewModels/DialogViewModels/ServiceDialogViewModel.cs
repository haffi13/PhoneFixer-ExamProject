using System;
using ViewModels.DialogServices;

namespace ViewModels
{
    internal class ServiceDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }

        private string windowTitle;

        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; }
        }
        public ServiceDialogViewModel(string windowTitle)
        {
            WindowTitle = windowTitle;
        }

    }
}