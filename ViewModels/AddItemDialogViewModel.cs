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
    public class AddItemDialogViewModel : BaseViewModel
    {
        private Item item;

        public AddItemDialogViewModel()
        {
            item = new Item();
        }

        #region These properties and corresponding variables should be in a dialog box view model
        public string ItemBarcode
        {
            get { return item.Barcode; }
            set
            {
                item.Barcode = value;
                OnPropertyChanged();
            }
        }
        public string ItemName
        {
            get { return item.Name; }
            set
            {
                item.Name = value;
                OnPropertyChanged();
            }
        }
        public string ItemDescription
        {
            get { return item.Description; }
            set
            {
                item.Description = value;
                OnPropertyChanged();
            }
        }
        public decimal ItemPrice
        {
            get { return item.Price; }
            set
            {
                item.Price = value;
                OnPropertyChanged();
            }
        }
        public string ItemCategory
        {
            get { return item.Category; }
            set
            {
                item.Category = value;
                OnPropertyChanged();
            }
        }
        public string ItemModel
        {
            get { return item.Model; }
            set
            {
                item.Model = value;
                OnPropertyChanged();
            }
        }
        public int ItemNumberAvailable
        {
            get { return item.NumberAvailable; }
            set
            {
                item.NumberAvailable = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
