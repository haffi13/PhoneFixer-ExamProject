using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    class CustomerDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        private readonly IDialogService dialogService;

        private Customer customer;

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

        #endregion


        public CustomerDialogViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.customer = new Customer();
        }
        public CustomerDialogViewModel(Customer customer ,IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.customer = customer;

            CustomerName = customer.CustomerName;
            CustomerPhone = customer.CustomerPhone;
            Email = customer.Email;
            Subscribed = customer.Subscribed;
            ItemInService = customer.ItemInService;
        }
    }
}
