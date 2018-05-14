using ViewModels.DialogServices;
using Models;
using System.Collections.ObjectModel;

namespace ViewModels
{
    public class SaleViewModel : BaseViewModel, ITabItem
    {
        private readonly IDialogService dialogService;
        private ObservableCollection<Item> items = new ObservableCollection<Item>();
        private ObservableCollection<Service> services = new ObservableCollection<Service>();
        private Item selectedItem;
        private Service selectedService;
        private Sale sale;

        private string priceWithTax;
        private string priceWithoutTax;
        private string discountPercent;

        public string Header { get; set; }

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand RemoveServiceCommand { get; }
        public RelayCommand RemoveItemCommand { get; }
        

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = new ObservableCollection<Item>(sale.Items);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                services = new ObservableCollection<Service>(sale.Services);
                OnPropertyChanged();
            }
        }

        public Item SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public Service SelectedService
        {
            get { return selectedService; }
            set { selectedService = value; }
        }

        #region Public Properties

        public string PriceWithTax
        {
            get { return priceWithTax; }
            set
            {
                priceWithTax = sale.PriceWithTax.ToString();
                OnPropertyChanged();
            }            
        }

        public bool CreditCard
        {
            get { return sale.CreditCard; }
            set { sale.CreditCard = value; }
        }
        public bool Company
        {
            get { return sale.Company; }
            set { sale.Company = value; }
        }
        public string DiscountPercent
        {
            get { return discountPercent; }
            set
            {
                if (InputValidity.DoubleNotNull(value))
                {
                    discountPercent = value;
                    sale.DiscountPercent = double.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public SaleViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            RemoveItemCommand = new RelayCommand(RemoveItem);
            RemoveServiceCommand = new RelayCommand(RemoveService);

            sale = Sale.Instance;
        }

        private void Confirm()
        {
            // Validity check


        }

        private void Cancel()
        {
            // Create new sale instance.
        }

        private void RemoveItem()
        {
            if(selectedItem != null)
            {
                string errorMessage = SaleManager.RemoveItem(selectedItem);
                if(errorMessage != string.Empty)
                {
                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(errorMessage, Message.SaleErrorTitle));
                }
                else
                {
                    // Refresh ?
                }
            }
        }

        private void RemoveService()
        {
            SaleManager.RemoveService(selectedService);
        }
    }
}
