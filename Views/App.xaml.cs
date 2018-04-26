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
            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<ItemDialogViewModel, ItemDialogView>();

            var viewModel = new MainWindowViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();
        }
    }
}
