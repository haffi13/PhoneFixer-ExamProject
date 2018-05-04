using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class CustomerViewModel : BaseViewModel, ITabItem
    {
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

        // Keeps an instance of the DialogService instanciated at startup.
        private readonly IDialogService dialogService;
        
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
    
        public CustomerViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            RefreshCustomers();

            AddCommand = new RelayCommand(AddCustomer);
            EditCommand = new RelayCommand(EditCustomer);
            DeleteCommand = new RelayCommand(DeleteCustomer);

        }
        public string Header { get; set; }

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }
        private void RefreshCustomers()
        {
            Customers = new ObservableCollection<Customer>(DatabaseReader.GetCustomers());
        }

        private void AddCustomer()
        {

        }

        private void EditCustomer()
        {

        }

        private void DeleteCustomer()
        {

        }


       
    }
        
        

}
