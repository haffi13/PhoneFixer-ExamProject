using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<ItemDialogViewModel, ItemDialogView>();
            

            var viewModel = new MainWindowViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();
        }
    }
}
