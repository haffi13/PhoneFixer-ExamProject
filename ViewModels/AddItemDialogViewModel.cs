using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    // This should maybe inherit from a specific dialog base which would
    // in turn inherit the base view model...inception!
    public class AddItemDialogViewModel : BaseViewModel
    {
        private string itemBarcode;
        private string itemName;
        private string itemDescription;
        private decimal itemPrice;
        private string itemCategory;
        private string itemModel;
        private int itemNumberAvailable;

        #region These properties and corresponding variables should be in a dialog box view model
        public string ItemBarcode
        {
            get { return itemBarcode; }
            set
            {
                itemBarcode = value;
                OnPropertyChanged();
            }
        }
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                OnPropertyChanged();
            }
        }
        public string ItemDescription
        {
            get { return itemDescription; }
            set
            {
                itemDescription = value;
                OnPropertyChanged();
            }
        }
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set
            {
                itemPrice = value;
                OnPropertyChanged();
            }
        }
        public string ItemCategory
        {
            get { return itemCategory; }
            set
            {
                itemCategory = value;
                OnPropertyChanged();
            }
        }
        public string ItemModel
        {
            get { return itemModel; }
            set
            {
                itemModel = value;
                OnPropertyChanged();
            }
        }
        public int ItemNumberAvailable
        {
            get { return itemNumberAvailable; }
            set
            {
                itemNumberAvailable = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
