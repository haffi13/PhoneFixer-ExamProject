namespace ViewModels.DialogServices
{
    // Interface which give access to the Registration of views to view models
    // and the ShowDialog method which Shows the corresponding dialog box.
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose
                                           where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;

    }
}
