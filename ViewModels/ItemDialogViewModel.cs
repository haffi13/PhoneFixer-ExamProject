using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels
{
    // This should maybe inherit from a specific dialog base which would
    // in turn inherit the base view model...inception!
    public class ItemDialogViewModel : BaseViewModel
    {
        DatabaseWriter databaseWriter = new DatabaseWriter();
        private Item item;

        private string price;
        private string numberAvailable;

        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ItemDialogViewModel()
        {
            item = new Item();

            AddItemCommand = new RelayCommand(AddItem);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void AddItem()
        {
            if (ItemDataIsCorrectFormat())
            {
                databaseWriter.AddItemToInventory(item);
                ClearPublicProperties();
                item = new Item();
            }
        }

        private void Cancel()
        {

        }
        // All item properties except Description do not allow nulls.
        private bool ItemDataIsCorrectFormat()
        {
            bool ret = true;

            if(Barcode == string.Empty || Barcode.Length > 15)
            {
                ret = false;
            }
            else if(Name == string.Empty || Name.Length > 50)
            {
                ret = false;
            }
            else if(Description.Length > 150)
            {
                ret = false;
            }
            else if(!PriceCanParse)
            {
                ret = false;
            }
            else if(Category == string.Empty || Category.Length > 20)
            {
                ret = false;
            }
            else if(Model == string.Empty || Model.Length > 30)
            {
                ret = false;
            }
            else if(!NumberAvailableCanParse)
            {
                ret = false;
            }

            return ret;
        }


        private bool priceCanParse;
        private bool numberAvailableCanParse;
        public bool PriceCanParse
        {
            get { return priceCanParse; }
            set { priceCanParse = value; }
        }
        public bool NumberAvailableCanParse
        {
            get { return numberAvailableCanParse; }
            set { numberAvailableCanParse = value; }
        }



        private void ClearPublicProperties()
        {
            Barcode = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Price = string.Empty;
            Category = string.Empty;
            Model = string.Empty;
            NumberAvailable = string.Empty;
        }

        #region These properties and corresponding variables should be in a dialog box view model
        public string Barcode
        {
            get { return item.Barcode; }
            set
            {
                item.Barcode = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return item.Name; }
            set
            {
                item.Name = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return item.Description; }
            set
            {
                item.Description = value;
                OnPropertyChanged();
            }
        }
        public string Price
        {
            get { return price; }
            set
            {
                priceCanParse = false;
                if (decimal.TryParse(value, out decimal price))
                {
                    item.Price = price;
                    priceCanParse = true;
                }
                OnPropertyChanged();
            }
        }
        public string Category
        {
            get { return item.Category; }
            set
            {
                item.Category = value;
                OnPropertyChanged();
            }
        }
        public string Model
        {
            get { return item.Model; }
            set
            {
                item.Model = value;
                OnPropertyChanged();
            }
        }
        public string NumberAvailable
        {
            get { return numberAvailable; }
            set
            {
                numberAvailableCanParse = false;
                if (Int32.TryParse(value, out int numberAvailable))
                {
                    item.NumberAvailable = numberAvailable;
                    numberAvailableCanParse = true;
                }
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
