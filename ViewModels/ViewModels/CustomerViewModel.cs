using Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class CustomerViewModel : BaseViewModel, ITabItem
    {
        // Keeps an instance of the DialogService instanciated at startup.
        private readonly IDialogService dialogService;

        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        private Customer selectedCustomer;

        public string Header { get; set; }
        
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

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
    
        public CustomerViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            RefreshCustomers();

            AddCommand = new RelayCommand(AddCustomer);
            EditCommand = new RelayCommand(EditCustomer);
            DeleteCommand = new RelayCommand(DeleteCustomer);
        }

        
        private void RefreshCustomers()
        {
            Dictionary<List<Customer>, string> temp = DatabaseReader.GetCustomers();
            string errorMessage = temp.Values.FirstOrDefault();
            if(errorMessage == string.Empty)
            {
                Customers = new ObservableCollection<Customer>(temp.Keys.FirstOrDefault());
            }
            else
            {
                bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(Message.GetCustomersError + errorMessage, Message.CustomerErrorTitle));
            }
        }

        private void AddCustomer()
        {
            bool? result = dialogService.ShowDialog(new CustomerDialogViewModel(dialogService));
            RefreshCustomers();
        }

        private void EditCustomer()
        {
            if(SelectedCustomer != null)
            {
                bool? result = dialogService.ShowDialog(new CustomerDialogViewModel(dialogService, SelectedCustomer));
                RefreshCustomers();
            }
        }

        private void DeleteCustomer()
        {
            bool? deleteCustomer = dialogService.ShowDialog
                (new ConfirmationDialogViewModel(Message.DeleteCustomerConfirmation));
            if (deleteCustomer == true && SelectedCustomer != null)
            {
                string errorMessage = DatabaseWriter.DeleteCustomer(SelectedCustomer);
                if (errorMessage != string.Empty)
                {
                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(errorMessage, Message.CustomerErrorTitle));
                }
                else
                {
                    RefreshCustomers();
                }
            }
        }  
    }
}
