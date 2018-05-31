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
        private SaleManager saleManager;

        private string priceWithTax;
        private string discountPercentage;// here for further development
        private string amountPaid;//added
        private bool amountPaidEnabled = true;//added

        // Implementation of the ITabItem inteface
        public string Header { get; set; }

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand RemoveProductCommand{ get; }
        
        public ObservableCollection<Item> Items
        {
            get
            {
                OnPropertyChanged(nameof(Receipt));
                return new ObservableCollection<Item>(sale.Items);
            }
        }

        public ObservableCollection<Service> Services
        {
            get
            {
                OnPropertyChanged(nameof(Receipt));
                return new ObservableCollection<Service>(sale.Services);
            }
        }

        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if(value != null)
                {
                    selectedItem = value;
                    selectedService = null;
                }
                else
                {
                    selectedItem = null;
                }
            }
        }

        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                if(value != null)
                {
                    selectedService = value;
                    SelectedItem = null;
                }
                else
                {
                    selectedService = null;
                }
            }
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
            set
            {
                sale.CreditCard = value;
                AmountPaidEnabled = !value;
            }
        }
        public bool Company
        {
            get { return sale.Company; }
            set { sale.Company = value; }
        }
        public string DiscountPercentage
        {
            get { return discountPercentage; }
            set
            {
                if (InputValidity.DoubleNotNull(value))
                {
                    discountPercentage = value;
                    sale.DiscountPercentage = double.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public bool AmountPaidEnabled
        {
            get {return amountPaidEnabled; }
            set
            {
                if(value == false)
                {
                    AmountPaid = string.Empty;
                }
                amountPaidEnabled = value;
                OnPropertyChanged();
            }
        }

        public string AmountPaid
        {
            get { return amountPaid; }
            set
            {
                if (InputValidity.DecimalNotNull(value) || value == string.Empty)
                {
                    amountPaid = value;
                    OnPropertyChanged(nameof(Remainder));
                }
                OnPropertyChanged();
            }
        }

        public string Remainder
        {
            get
            {
                if(CreditCard != true && AmountPaid != null && AmountPaid != string.Empty)
                {
                    return (decimal.Parse(AmountPaid) - sale.PriceWithTax).ToString(); 
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        
        public ObservableCollection<ReceiptNode> Receipt
        {
            get { return new ObservableCollection<ReceiptNode>(saleManager.Receipt.Nodes); }
        }

        #endregion

        public SaleViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            

            sale = Sale.Instance;
            saleManager = SaleManager.Instance;
        }

        private void Confirm()
        {
            string errorMessage = saleManager.FinalizeSale();
            if(errorMessage != string.Empty)
            {
                bool? result = dialogService.ShowDialog
                    (new MessageBoxDialogViewModel(errorMessage, Message.SaleErrorTitle));
            }
            AmountPaid = string.Empty;
            CreditCard = false;
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Services));
        }

        private void Cancel()
        {
            saleManager.CancelSale();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(Services));
        }

        private void RemoveProduct()
        {
            if(SelectedItem != null)
            {
                saleManager.RemoveItemFromSale(SelectedItem);
                OnPropertyChanged(nameof(Items));               
            }
            else if(SelectedService != null)
            {
                saleManager.RemoveServiceFromSale(SelectedService);
                OnPropertyChanged(nameof(Services));
            }
            OnPropertyChanged(nameof(Receipt));
        }
    }
}
