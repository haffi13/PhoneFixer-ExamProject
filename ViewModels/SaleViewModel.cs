using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    public class SaleViewModel : BaseViewModel, ITabItem
    {
        private readonly IDialogService dialogService;
        private Sale sale;

        private string priceWithTax;
        private string priceWithoutTax;
        private string discountPercent;

        public string Header { get; set; }

        #region Public Properties

        public string PriceWithTax
        {
            get { return priceWithTax; }
            set
            {
                if (InputValidity.DecimalNotNull(value))
                {
                    priceWithTax = value;
                    sale.PriceWithTax = decimal.Parse(value);
                    OnPropertyChanged();
                }
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

            sale = Sale.Instance;
        }

        private void Confirm()
        {
            // Validity check


        }
    }
}
