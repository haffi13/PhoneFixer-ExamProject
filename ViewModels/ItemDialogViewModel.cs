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
        private DatabaseWriter databaseWriter = new DatabaseWriter();
        private Item item;

        // Price and number available have a special string variable to store a string
        // version from the UI of the corresponding item value.
        private string price;
        private string numberAvailable;

        private string confirmButtonContent;
        private string cancelButtonContent;

        private bool barcodeIsEditable;
        private bool barcodeIsReadOnly;
        private bool priceCanParse;
        private bool numberAvailableCanParse;

        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

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
        public bool BarcodeIsReadOnly
        {
            get { return barcodeIsReadOnly; }
            set
            {
                barcodeIsReadOnly = value;
                OnPropertyChanged();
            } 
        }
        public bool BarcodeIsEditable
        {
            get { return barcodeIsEditable; }
            set
            {
                barcodeIsEditable = value;
                OnPropertyChanged();
            }
        }
        public string ConfirmButtonContent
        {
            get { return confirmButtonContent; }
            set
            {
                confirmButtonContent = value;
                OnPropertyChanged();
            }
        }
        public string CancelButtonContent
        {
            get { return cancelButtonContent; }
            set
            {
                cancelButtonContent = value;
                OnPropertyChanged();
            }
        }

        #region Public item properties 
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


        // Constructor used when the dialog is to be used to add a item.
        public ItemDialogViewModel()
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            if(item == null)
            {
                item = new Item();

                BarcodeIsReadOnly = false;
                BarcodeIsEditable = true;

                ConfirmButtonContent = "Add";
                CancelButtonContent = "Close";
            }
        }
        // Constructor used when the dialog is to be used to edit a item.
        public ItemDialogViewModel(Item item)
        {
            this.item = item;
            BarcodeIsReadOnly = true;
            BarcodeIsEditable = false;

            ConfirmButtonContent = "Ok";
            CancelButtonContent = "Cancel";

            // These properties are bound to the text boxes in the view.
            // By passing the selected item as args and setting the properties to the 
            // selected values this view model can be reused for both adding and editing.

            Barcode = item.Barcode;
            Name = item.Name;
            Description = item.Description;
            Price = item.Price.ToString();
            Category = item.Category;
            Model = item.Model;
            NumberAvailable = item.NumberAvailable.ToString();
        }

        // Method called when the Confirm button is clicked.
        // The ItemUpdate stored procedure can handle adding and editing items
        private void Confirm()
        {
            //if (ItemDataIsCorrectFormat())
            //{
            //    databaseWriter.AddItemToInventory(item);
            //    ClearPublicProperties();
            //    item = new Item();
            //}
            databaseWriter.UpdateItem(item);
            ClearPublicProperties();
            item = new Item();
        }

        // Method called when the Cancel button is clicked.
        private void Cancel()
        {

        }
        // No item properties except Description allow nulls.
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

    }
}
