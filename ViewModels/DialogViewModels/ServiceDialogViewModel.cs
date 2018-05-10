using System;
using ViewModels.DialogServices;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace ViewModels
{
    public class ServiceDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        private readonly IDialogService dialogService;
        private ServiceValidity serviceValidity;
        private Service service;
        private Customer selectedCustomer;

        private bool isEdit;

        private string windowTitle;
        private string confirmButtonContent;
        private string cancelButtonContent;

        private string priceNoTax;
        private string priceWithTax;

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand SelectCustomerCommand { get; }

        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; }
        }

        // Bound to the Confirm button in the ServiceDialogView. Contains the button content
        // "Add" if adding a service, "OK" if editing a service.
        public string ConfirmButtonContent
        {
            get { return confirmButtonContent; }
            set
            {
                confirmButtonContent = value;
                OnPropertyChanged();
            }
        }
        // Bound to the Cancel button in the ServiceDialogView. Contains the button content
        // "Close" if adding a service, "Cancel" if editing a service.
        public string CancelButtonContent
        {
            get { return cancelButtonContent; }
            set
            {
                cancelButtonContent = value;
                OnPropertyChanged();
            }
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
                    serviceValidity.NameIsValid = true;
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
                OnPropertyChanged();
            }
        }

        public string Price
        {
            get { return priceNoTax; }
            set
            {
                if (InputValidity.DecimalNotNull(value))
                {
                    priceNoTax = value;
                    service.PriceNoTax = decimal.Parse(value);
                    serviceValidity.PriceIsValid = true;
                }
                else
                {
                    priceNoTax = string.Empty;
                    serviceValidity.PriceIsValid = false;
                }
                OnPropertyChanged();

                if(ValueAddedTax.AddVAT(service.PriceNoTax) != service.PriceWithTax)
                {
                    PriceWithTax = ValueAddedTax.AddVAT(service.PriceNoTax).ToString();
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
                    serviceValidity.PriceIsValid = true;
                }
                else
                {
                    priceWithTax = string.Empty;
                    serviceValidity.PriceIsValid = false;
                }
                OnPropertyChanged();

                if(ValueAddedTax.RemoveVAT(service.PriceWithTax) != service.PriceNoTax)
                {
                    Price = ValueAddedTax.RemoveVAT(service.PriceWithTax).ToString();
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
        }

       public Customer SelectedCustomer
        {
            // Make bool customer selected for checks.. 
            get { return selectedCustomer; }
            set
            {
                if(value != null)
                {
                    selectedCustomer = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }
        #endregion


        // Ctor used to construct the view model when adding a new service.
        public ServiceDialogViewModel(string windowTitle, IDialogService dialogService)
        {
            WindowTitle = windowTitle;
            ConfirmButtonContent = "Add";
            CancelButtonContent = "Close";
            this.dialogService = dialogService;
            isEdit = false;

            SelectCustomerCommand = new RelayCommand(SelectCustomer);
            if(service == null)
            {
                service = new Service();
                serviceValidity = new ServiceValidity();
                SelectedCustomer = new Customer();
            }
        }

        // Ctor used to construct the view model when editing an existing service.
        public ServiceDialogViewModel(string windowTitle, IDialogService dialogService, Service service)
        {
            WindowTitle = windowTitle;
            ConfirmButtonContent = "Ok";
            CancelButtonContent = "Close";
            this.dialogService = dialogService;
            this.service = service;
            isEdit = true;
            SelectCustomerCommand = new RelayCommand(SelectCustomer);
            serviceValidity = new ServiceValidity();

            ServiceName = service.ServiceName;
            ServiceDescription = service.ServiceDescription;
            Price = service.PriceNoTax.ToString();
            PriceWithTax = service.PriceWithTax.ToString();
            Repaired = service.Repaired;
            
            Dictionary<Customer, string> temp = DatabaseReader.GetCustomer(service.CustomerId);
            if(temp.Values.FirstOrDefault() == string.Empty)
            {
                SelectedCustomer = temp.Keys.FirstOrDefault();
            }
            else
            {
                // Error message could not find selected customer.
            }   
        }

        private void Confirm()
        {
            if (serviceValidity.ServiceIsValid())
            {
                if (!isEdit)
                {
                    service.DayCreated = DateTime.Now;
                }
                if (Repaired)
                {
                    service.DayServiced = DateTime.Now;
                }

                string errorMessage = DatabaseWriter.CreateService(service);
                if (errorMessage == string.Empty)
                {
                    CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
                }
                else
                {
                    string customMessage;
                    if (isEdit)
                    {
                        customMessage = Message.EditServiceError + errorMessage;
                    }
                    else
                    {
                        customMessage = Message.AddServiceError + errorMessage;
                    }
                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(customMessage, Message.ServiceErrorTitle));
                }
                
            }
            else
            {
                bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(Message.ServiceInputError, Message.ServiceErrorTitle));
            }
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
            }    
        }
    }
}