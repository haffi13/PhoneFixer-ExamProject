using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    public class SelectCustomerDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        private Customer selectedCustomer;
        private readonly IDialogService dialogService;
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public RelayCommand ConfirmCommand { get; }

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public SelectCustomerDialogViewModel(IDialogService dialogService, string windowTitle)
        {
            this.dialogService = dialogService;

            ConfirmCommand = new RelayCommand(Confirm);

            Dictionary<List<Customer>, string> temp = DatabaseReader.GetCustomers();
            string errorMessage = temp.Values.FirstOrDefault();
            if (errorMessage == string.Empty)
            {
                Customers = new ObservableCollection<Customer>(temp.Keys.FirstOrDefault());
            }
            else
            {
                bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(Message.GetCustomersError + errorMessage, Message.CustomerErrorTitle));
            }
        }

        private void Confirm()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
