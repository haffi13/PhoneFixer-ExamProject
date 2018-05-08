using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    public class CustomerDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        private readonly IDialogService dialogService;

        private Customer customer;

        private string confirmButtonContent;
        private string cancelButtonContent;
        private string itemDialogMessage;

        private bool isEdit;

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }

        #region Public Properties

        public string CustomerName
        {
            get { return customer.CustomerName; }
            set
            {
                customer.CustomerName = value;
                OnPropertyChanged();
            }
        }
        public string CustomerPhone
        {
            get { return customer.CustomerPhone; }
            set
            {
                customer.CustomerPhone = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return customer.Email; }
            set
            {
                customer.Email = value;
                OnPropertyChanged();
            }
        }
        public bool Subscribed
        {
            get { return customer.Subscribed; }
            set
            {
                customer.Subscribed = value;
                OnPropertyChanged();
            }
        }
        public bool ItemInService
        {
            get { return customer.ItemInService; }
            set
            {
                customer.ItemInService = value;
                OnPropertyChanged();
            }
        }

        // Bound to the Confirm button in the CustomerDialogView. Contains the button content
        // "Add" if adding an item, "OK" if editing an item.
        public string ConfirmButtonContent
        {
            get { return confirmButtonContent; }
            set
            {
                confirmButtonContent = value;
                OnPropertyChanged();
            }
        }
        // Bound to the Cancel button in the CustomerDialogView. Contains the button content
        // "Close" if adding an item, "Cancel" if editing an item.
        public string CancelButtonContent
        {
            get { return cancelButtonContent; }
            set
            {
                cancelButtonContent = value;
                OnPropertyChanged();
            }
        }

        public string ItemDialogMessage
        {
            get { return itemDialogMessage; }
            set
            {
                itemDialogMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public CustomerDialogViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            
            isEdit = false;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            if(customer == null)
            {
                customer = new Customer();
                isEdit = false;

                confirmButtonContent = "Add";
                CancelButtonContent = "Close";
            }
        }
        public CustomerDialogViewModel(Customer customer ,IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.customer = customer;

            isEdit = true;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            confirmButtonContent = "OK";
            CancelButtonContent = "Cancel";

            CustomerName = customer.CustomerName;
            CustomerPhone = customer.CustomerPhone;
            Email = customer.Email;
            Subscribed = customer.Subscribed;
            ItemInService = customer.ItemInService;
        }

        private void Confirm()
        {
            string errorMessage = string.Empty;
            if (InputValidity.Name(CustomerName))
            {
                if (isEdit)
                {
                    errorMessage = DatabaseWriter.EditCustomer(customer);
                }
                else
                {
                    errorMessage = DatabaseWriter.AddCustomer(customer);
                }

                if (errorMessage != string.Empty)
                {
                    bool? result = dialogService.ShowDialog
                           (new MessageBoxDialogViewModel(errorMessage, Message.CustomerErrorTitle));
                }
                else
                {
                    CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
                }
            }
            else
            {
                //check input

            }
        }

        private void Cancel()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }

    }
}
