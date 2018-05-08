using System.Windows;
using ViewModels.DialogServices;
using ViewModels;

namespace Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Instead of running the MainWindow the OnStartup is overridden.
        // Here view models for dialogs and dialog views are paired together.
        // Then the dialog service is passed to the MainWindow so the other 
        // view models can get access to it. 
        // After all that is done the MainWindow is shown.
        protected override void OnStartup(StartupEventArgs e)
        {
            // For some reason the dialog box doesn't appear in the center of the 
            // MainWindow even if MainWindow is set as owner here, and the 
            // ItemDialogWindow startup location is set to center of owner.
            // Might be the startup uri...
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<ItemDialogViewModel, ItemDialogView>();
            dialogService.Register<CustomerDialogViewModel, CustomerDialogView>();
            dialogService.Register<ServiceDialogViewModel, ServiceDialogView>();
            dialogService.Register<MessageBoxDialogViewModel, MessageBoxDialogView>();
            
            dialogService.Register<SelectCustomerDialogViewModel, SelectCustomerDialogView>();

            var viewModel = new MainWindowViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };

            view.Show();
        }
    }
}
