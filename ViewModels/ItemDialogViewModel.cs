using System;
using System.Windows;
using Models;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class ItemDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        private Item item;
        
        // Price and number available have a string variable to store the string value
        // from the corresponding textboxes in the ItemDialogView. 
        // They cannot be stored in a  item object as those objects are of type int and decimal.
        private string price;
        private string numberAvailable;

        private string confirmButtonContent;
        private string cancelButtonContent;
        private string itemDialogMessage;

        private bool isEdit; // no properties.
        
        private bool barcodeIsEditable;
        private bool barcodeIsReadOnly;
        private bool priceCanParse;
        private bool numberAvailableCanParse;

        // Private variables who point out what values are not valid in the 
        // textboxes in the ItemDialogView.
        private bool barcodeIsValid = true;
        private bool nameIsValid = true;
        private bool descriptionIsValid = true;
        private bool priceIsValid = true;
        private bool categoryIsValid = true;
        private bool modelIsValid = true;
        private bool numberAvailableIsValid = true;

        // Handles requests to close the dialog box
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public RelayCommand ConfirmCommand { get; } // just get, doesnt need set
        public RelayCommand CancelCommand { get; }

        // Bool which returns if the value in the Price textbox in the ItemDialogView
        // can be parced to a decimal value.
        public bool PriceCanParse
        {
            get { return priceCanParse; }
            set { priceCanParse = value; }
        }
        
        // Bool which returns if the value in the NumberAvailable textbox in the ItemDialogView
        // can be parced to a Int value.
        public bool NumberAvailableCanParse
        {
            get { return numberAvailableCanParse; }
            set { numberAvailableCanParse = value; }
        }
        // Bound to the Barcode textbox in the ItemDialogView. Makes it un-editable.
        public bool BarcodeIsReadOnly
        {
            get { return barcodeIsReadOnly; }
            set
            {
                barcodeIsReadOnly = value;
                OnPropertyChanged();
            } 
        }
        // Bound to the Barcode textbox in the ItemDialogView. Makes it un-selectable.
        public bool BarcodeIsEditable
        {
            get { return barcodeIsEditable; }
            set
            {
                barcodeIsEditable = value;
                OnPropertyChanged();
            }
        }
        // Bound to the Confirm button in the ItemDialogView. Contains the button content
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
        // Bound to the Cancel button in the ItemDialogView. Contains the button content
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
                this.price = value.Trim();
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
                this.numberAvailable = value.Trim();
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


        // bool? maybe...might make us get away with working with un-instanciated properties.
        #region Input validity checks

        public bool BarcodeIsValid 
        {
            get { return barcodeIsValid; }
            set
            {
                barcodeIsValid = value;
                OnPropertyChanged();
            }
        } 

        public bool NameIsValid
        {
            get { return nameIsValid; }
            set
            {
                nameIsValid = value;
                OnPropertyChanged();
            }
        }

        public bool DescriptionIsValid
        {
            get { return descriptionIsValid; }
            set
            {
                descriptionIsValid = value;
                OnPropertyChanged();
            }
        }

        public bool PriceIsValid
        {
            get { return priceIsValid; }
            set
            {
                priceIsValid = value;
                OnPropertyChanged();
            }
        }

        public bool CategoryIsValid
        {
            get { return categoryIsValid; }
            set
            {
                categoryIsValid = value;
                OnPropertyChanged();
            }
        }

        public bool ModelIsValid
        {
            get { return modelIsValid; }
            set
            {
                modelIsValid = value;
                OnPropertyChanged();
            }
        }
        
        public bool NumberAvailableIsValid
        {
            get { return numberAvailableIsValid; }
            set
            {
                numberAvailableIsValid = value;
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

                isEdit = false;

                BarcodeIsReadOnly = false;
                BarcodeIsEditable = true;

                ConfirmButtonContent = "Add";
                CancelButtonContent = "Close";
            }
        }
        // Constructor used when the dialog is to be used to edit a item.
        public ItemDialogViewModel(Item item)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            this.item = item;

            isEdit = true;

            BarcodeIsReadOnly = true;
            BarcodeIsEditable = false;

            ConfirmButtonContent = "Ok";
            CancelButtonContent = "Cancel";

            // These properties are bound to the text boxes in the view.
            // By passing the selected item as args and setting the properties to the 
            // selected values this view model can be reused for both adding and editing,
            // decided from which constructor the view model is instanciated with.

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
                string errorMessage = DatabaseWriter.UpdateItem(item);
                //
                //  Insert bool to check if database operation goes smooooooth.
                if(errorMessage == string.Empty)
                {
                   
                    ClearPublicProperties();
                    item = new Item();

                    if (isEdit)
                    {
                        CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
                    }
                    
                    // Only reaches here if adding a item.
                    // Timer...
                    ItemDialogMessage = Message.AddItemSuccess;
                }
                else
                {
                    // Change to our own message box
                    MessageBox.Show(errorMessage);
                } 
           // }
        }

        // Method called when the Cancel button is clicked.
        private void Cancel()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }
        

        // This method is currently public for testing purposes. Shall be private!
        // This method checks if the input in the ItemDialogView is of values and size the 
        // item table in the database accepts.
        public bool ItemDataIsCorrectFormat()
        {
            bool ret = true;

            // We need to handle potential null reference exception.

            if(Barcode == string.Empty || Barcode.Length > 15)
            {
                ret = false;
                BarcodeIsValid = false;
            }
            else if(Name == string.Empty || Name.Length > 50)
            {
                ret = false;
                NameIsValid = false;
            }
            else if(Description.Length > 150)
            {
                ret = false;
                DescriptionIsValid = false;
            }
            else if(!PriceCanParse)
            {
                ret = false;
                PriceIsValid = false;
            }
            else if(Category == string.Empty || Category.Length > 20)
            {
                ret = false;
                CategoryIsValid = false;
            }
            else if(Model == string.Empty || Model.Length > 30)
            {
                ret = false;
                ModelIsValid = false;
            }
            else if(!NumberAvailableCanParse)
            {
                ret = false;
                NumberAvailableIsValid = false;
            }
            return ret;
        }

        // Clears the string properties bound to the textboxes in the ItemDialogView.
        // This method is called before a new instance of item is instanciated.
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
