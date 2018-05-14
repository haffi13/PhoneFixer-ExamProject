using System;
using System.Threading;
using Models;
using ViewModels.DialogServices;

namespace ViewModels
{

    // --------------------------------------------------

    // There is a problem with the items and the database.

    // It might be better to keep the editing specific and adding
    // specific stuff in seperate classes...

    // --------------------------------------------------


    public class ItemDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        private Item item;
        private ItemValidity itemValidity;
        private readonly IDialogService dialogService;

        private string priceNoTax;
        private string priceWithTax;
        private string numberAvailable;
        private string numberAvailableBeforeEdit;

        private string confirmButtonContent;
        private string cancelButtonContent;
        private string itemDialogMessage;

        private bool isEdit; // no properties.

        private bool barcodeIsEditable;
        private bool barcodeIsReadOnly;

        // Handles requests to close the dialog box
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }

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

        // Message that appears at the top of the dialog box.
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
                if (InputValidity.Varchar15NotNull(value))
                {
                    item.Barcode = value;
                    itemValidity.BarcodeIsValid = true;
                }
                else
                {
                    itemValidity.BarcodeIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return item.Name; }
            set
            {
                if (InputValidity.Varchar50NotNull(value))
                {
                    item.Name = value;
                    itemValidity.NameIsValid = true;
                }
                else
                {
                    itemValidity.NameIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return item.Description; }
            set
            {
                if (InputValidity.Varchar150Null(value))
                {
                    item.Description = value;
                }
                OnPropertyChanged();
            }
        }

        // There is a string variable for price so the textbox it is bound to 
        // shows nothing instead of a 0 when the view is displayed.
        // When Price is edited the PriceWithTax gets updated automatically.
        public string Price
        {
            get { return priceNoTax; }
            set
            {
                if (InputValidity.DecimalNotNull(value))
                {
                    priceNoTax = value;
                    item.PriceNoTax = decimal.Parse(value);
                    itemValidity.PriceIsValid = true;
                }
                else
                {
                    priceNoTax = string.Empty;
                    itemValidity.PriceIsValid = false;
                }
                OnPropertyChanged();

                if (ValueAddedTax.AddVAT(item.PriceNoTax) != item.PriceWithTax)
                {
                    PriceWithTax = ValueAddedTax.AddVAT(item.PriceNoTax).ToString();
                }
            }
        }

        // There is a string variable for priceWithTax so the textbox it is bound to 
        // shows nothing instead of a 0 when the view is displayed.
        // When PriceWithTax is edited the Price gets updated automatically.
        public string PriceWithTax
        {
            get { return priceWithTax; }
            set
            {
                if (InputValidity.DecimalNotNull(value))
                {
                    priceWithTax = value;
                    item.PriceWithTax = decimal.Parse(value);
                    itemValidity.PriceIsValid = true;
                }
                else
                {
                    priceWithTax = string.Empty;
                    itemValidity.PriceIsValid = false;
                }
                OnPropertyChanged();

                if (ValueAddedTax.RemoveVAT(item.PriceWithTax) != item.PriceNoTax)
                {
                    Price = ValueAddedTax.RemoveVAT(item.PriceWithTax).ToString();
                }
            }
        }

        public string Category
        {
            get { return item.Category; }
            set
            {
                if (InputValidity.Varchar20NotNull(value))
                {
                    item.Category = value;
                    itemValidity.CategoryIsValid = true;
                }
                else
                {
                    itemValidity.CategoryIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        public string Model
        {
            get { return item.Model; }
            set
            {
                if (InputValidity.Varchar30NotNull(value))
                {
                    item.Model = value;
                    itemValidity.ModelIsValid = true;
                }
                else
                {
                    itemValidity.ModelIsValid = false;
                }
                OnPropertyChanged();
            }
        }

        // This gets set when the user confirms the item he's adding to the system.

        // Make sure to implement it correctly when editing a item. 

        public DateTime LastTimeAdded
        {
            get { return item.LastTimeAdded; }
            set
            {
                item.LastTimeAdded = value;
                OnPropertyChanged();
            }
        }

        // There is a string variable for numberAvailable so the textbox it is bound to 
        // shows nothing instead of a 0 when the view is displayed.
        public string NumberAvailable
        {
            get { return numberAvailable; }
            set
            {
                if (InputValidity.IntNotNull(value))
                {
                    numberAvailable = value;
                    item.NumberAvailable = int.Parse(value);
                    itemValidity.NumberAvailableIsValid = true;
                }
                else
                {
                    itemValidity.NumberAvailableIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        #endregion


        // Constructor used when the dialog is to be used to add a item.
        public ItemDialogViewModel(IDialogService dialogService)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            itemValidity = new ItemValidity();
            this.dialogService = dialogService;

            if (item == null)
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
        public ItemDialogViewModel(IDialogService dialogService, Item item) : this(dialogService)
        {
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
            Price = item.PriceNoTax.ToString();
            PriceWithTax = item.PriceWithTax.ToString();
            Category = item.Category;
            Model = item.Model;
            LastTimeAdded = item.LastTimeAdded;
            NumberAvailable = item.NumberAvailable.ToString();
            numberAvailableBeforeEdit = NumberAvailable;
        }

        // Method called when the Confirm button is clicked.
        // The ItemUpdate stored procedure can handle adding and editing items
        private void Confirm()
        {
            if (itemValidity.ItemIsValid())
            {
                if ((isEdit && NumberAvailable != numberAvailableBeforeEdit) || (!isEdit))
                {
                    LastTimeAdded = DateTime.Now;
                }

                string errorMessage = DatabaseWriter.UpdateItem(item);

                if (errorMessage == string.Empty)
                {
                    item = new Item();
                    ClearPublicProperties();
                    itemValidity = new ItemValidity();

                    if (isEdit)
                    {
                        CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
                    }

                    
                    // Only reaches here if adding a item.
                    // Timer...
                    //ItemDialogMessage = Message.AddItemSuccess;
                }
                else
                {
                    string customMessage;
                    if (isEdit)
                    {
                        customMessage = Message.EditItemError + errorMessage;
                    }
                    else
                    {
                        customMessage = Message.AddItemError + errorMessage;
                    }

                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(customMessage, Message.InventoryErrorTitle));
                }
            }
            else
            {
                // Make visible in UI where input is not valid. 
                bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(Message.ItemInputError, Message.InventoryErrorTitle));
            }
        }

        // Method called when the Cancel button is clicked.
        private void Cancel()
        {
            CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }


        // Clears the string properties bound to the textboxes in the ItemDialogView.
        // This method is called before a new instance of item is instanciated.
        private void ClearPublicProperties()
        {
            Barcode = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Price = string.Empty;
            PriceWithTax = string.Empty;
            Category = string.Empty;
            Model = string.Empty;
            NumberAvailable = string.Empty;
        }
    }
}
