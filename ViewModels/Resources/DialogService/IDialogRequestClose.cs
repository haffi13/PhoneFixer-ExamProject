using System;

namespace ViewModels.DialogServices
{
    // Interface which should be inherited by all dialog view models.
    // Gives access to an event handler that handles close requests of the dialog window.
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
