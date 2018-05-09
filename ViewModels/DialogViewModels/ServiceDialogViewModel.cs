using System;
using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    public class ServiceDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        private readonly IDialogService dialogService;
        private ServiceValidity serviceValidity;
        private Service service;
        //private Customer selectedCustomer;

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand SelectCustomerCommand { get; }

        private string windowTitle;

        private string price;
        private string priceWithTax;

        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; }
        }

        #region Public Properties

        public string ServiceName
        {
            get { return service.ServiceName; }
            set
            {
                if (InputValidity.Varchar50NotNull(value))
                {
                    service.ServiceName = value;
                    serviceValidity.nameIsValid = true;
                }
                OnPropertyChanged();
            }
        }

        public string ServiceDescription
        {
            get { return service.ServiceDescription; }
            set
            {
                if (InputValidity.Varchar150Null(value))
                {
                    service.ServiceDescription = value;
                }   
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                if (InputValidity.DecimalNotNull(value))
                {
                    price = value;
                    service.PriceNoTax = decimal.Parse(value);
                    serviceValidity.priceIsValid = true;
                }
                else
                {
                    serviceValidity.priceIsValid = false;
                    price = string.Empty;
                }
            }
        }

        public string PriceWithTax
        {
            get { return priceWithTax; }
            set
            {
                if (InputValidity.DecimalNotNull(value))
                {
                    priceWithTax = value;
                    service.PriceWithTax = decimal.Parse(value);
                    serviceValidity.priceIsValid = true;
                }
                else
                {
                    serviceValidity.priceIsValid = false;
                    priceWithTax = string.Empty;
                }
            }
        }

        // Bound to done
        public bool Repaired
        {
            get { return service.Repaired; }
            set
            {
                service.Repaired = value;
                OnPropertyChanged();
            }
        }

        public string CustomerName
        {
            get
            {
                if(SelectedCustomer != null)
                {
                    return SelectedCustomer.CustomerName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                CustomerName = value;
                OnPropertyChanged();
            }
        }

       public Customer SelectedCustomer
        {
            // Make bool customer selected for checks.. 
            get { return service.Customer; }
            set
            {
                if(value != null)
                {
                    service.Customer = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }
        #endregion


        // Make a ctor that takes service as parameter to be able to add
        public ServiceDialogViewModel(string windowTitle, IDialogService dialogService)
        {
            WindowTitle = windowTitle;
            this.dialogService = dialogService;

            SelectCustomerCommand = new RelayCommand(SelectCustomer);
            if(service == null)
            {
                service = new Service();
                serviceValidity = new ServiceValidity();
                SelectedCustomer = new Customer();

            }
        }

        public ServiceDialogViewModel(string windowTitle, IDialogService dialogService, Service service)
        {
            WindowTitle = windowTitle;
            this.dialogService = dialogService;
            this.service = service;
            SelectCustomerCommand = new RelayCommand(SelectCustomer);
            serviceValidity = new ServiceValidity();

            ServiceName = service.ServiceName;
            ServiceDescription = service.ServiceDescription;
            Price = service.PriceNoTax.ToString();
            PriceWithTax = service.PriceWithTax.ToString();
            Repaired = service.Repaired;
            //CustomerName = service.Customer.CustomerName;
            //SelectedCustomer = service.Customer;
            
            
        }

        private void Confirm()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
        private void Cancel()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }

        private void SelectCustomer()
        {
            SelectCustomerDialogViewModel selectCustomerDialogViewModel = new SelectCustomerDialogViewModel(dialogService, "windowTitle");
            if(dialogService.ShowDialog(selectCustomerDialogViewModel) == true)
            {
                SelectedCustomer = selectCustomerDialogViewModel.SelectedCustomer;
                service.Customer = SelectedCustomer;
            }    
        }
    }
}