using System;

namespace ViewModels.DialogServices
{
    // Called when the dialog box should close. Used by other dialog services
    // to see if the dialog result has a valid value.
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public bool? DialogResult { get; }
        public DialogCloseRequestedEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
