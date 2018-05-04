﻿using System;
using System.Windows;
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

        private string price;
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

        public RelayCommand ConfirmCommand { get; } // just get, doesnt need set
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
                if (InputValidity.Barcode(value))
                {
                    item.Barcode = value;
                    BarcodeIsValid = true;
                }
                else
                {
                    BarcodeIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return item.Name; }
            set
            {
                if (InputValidity.Name(value))
                {
                    item.Name = value;
                    NameIsValid = true;
                }
                else
                {
                    NameIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return item.Description; }
            set
            {
                if (InputValidity.Description(value))
                {
                    item.Description = value;
                    DescriptionIsValid = true;
                }
                else
                {
                    DescriptionIsValid = false;
                }
                OnPropertyChanged();
            }
        }

        // There is a string variable for price so the textbox it is bound to 
        // shows nothing instead of a 0 when the view is displayed.
        // When Price is edited the PriceWithTax gets updated automatically.
        public string Price
        {
            get { return price; }
            set
            {
                if (InputValidity.Price(value))
                {
                    price = value;
                    item.Price = decimal.Parse(value);
                    PriceIsValid = true;
                }
                else
                {
                    PriceIsValid = false;
                    price = string.Empty;
                }
                OnPropertyChanged();

                if (ValueAddedTax.AddVAT(item.Price) != item.PriceWithTax)
                {
                    PriceWithTax = ValueAddedTax.AddVAT(item.Price).ToString();
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
                if (InputValidity.Price(value))
                {
                    priceWithTax = value;
                    item.PriceWithTax = decimal.Parse(value);
                    PriceIsValid = true;
                }
                else
                {
                    PriceIsValid = false;
                    priceWithTax = string.Empty;
                }
                OnPropertyChanged();

                if(ValueAddedTax.RemoveVAT(item.PriceWithTax) != item.Price)
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
                if (InputValidity.Category(value))
                {
                    item.Category = value;
                    CategoryIsValid = true;
                }
                else
                {
                    CategoryIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        public string Model
        {
            get { return item.Model; }
            set
            {
                if (InputValidity.Model(value))
                {
                    item.Model = value;
                    ModelIsValid = true;
                }
                else
                {
                    ModelIsValid = false;
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
                if (InputValidity.NumberAvailable(value))
                {
                    numberAvailable = value;
                    item.NumberAvailable = int.Parse(value);
                    NumberAvailableIsValid = true;
                }
                else
                {
                    NumberAvailableIsValid = false;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        // bool? maybe...might make us get away with working with un-instanciated properties.
        #region Input validity checks
        // These are here to bind to the boxes to show where the input was wrong.
        // It should be done from the else statement in the Confirm method.
        // If you are reading this comment that has yet to be done...
        public bool BarcodeIsValid
        {
            get { return itemValidity.BarcodeIsValid; }
            set
            {
                itemValidity.BarcodeIsValid = value;
                OnPropertyChanged();
            }
        }
        public bool NameIsValid
        {
            get { return itemValidity.NameIsValid; }
            set
            {
                itemValidity.NameIsValid = value;
                OnPropertyChanged();
            }
        }
        public bool DescriptionIsValid
        {
            get { return itemValidity.DescriptionIsValid; }
            set
            {
                itemValidity.DescriptionIsValid = value;
                OnPropertyChanged();
            }
        }
        public bool PriceIsValid
        {
            get { return itemValidity.PriceIsValid; }
            set
            {
                itemValidity.PriceIsValid = value;
                OnPropertyChanged();
            }
        }
        public bool CategoryIsValid
        {
            get { return itemValidity.CategoryIsValid; }
            set
            {
                itemValidity.CategoryIsValid = value;
                OnPropertyChanged();
            }
        }
        public bool ModelIsValid
        {
            get { return itemValidity.ModelIsValid; }
            set
            {
                itemValidity.ModelIsValid = value;
                OnPropertyChanged();
            }
        }
        public bool NumberAvailableIsValid
        {
            get { return itemValidity.NumberAvailableIsValid; }
            set
            {
                itemValidity.NumberAvailableIsValid = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        // Constructor used when the dialog is to be used to add a item.
        public ItemDialogViewModel(IDialogService dialogService)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            this.dialogService = dialogService;

            if (item == null)
            {
                item = new Item();
                itemValidity = new ItemValidity();

                isEdit = false;

                BarcodeIsReadOnly = false;
                BarcodeIsEditable = true;

                ConfirmButtonContent = "Add";
                CancelButtonContent = "Close";
            }
        }

        // Constructor used when the dialog is to be used to edit a item.
        public ItemDialogViewModel(Item item, IDialogService dialogService)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            this.dialogService = dialogService;
            this.item = item;
            itemValidity = new ItemValidity();

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
            if (ItemDataIsCorrectFormat())
            {
                if ((isEdit && NumberAvailable != numberAvailableBeforeEdit) || (!isEdit))
                {
                    LastTimeAdded = DateTime.Now;
                }
                
                string errorMessage = DatabaseWriter.UpdateItem(item);

                if (DatabaseWriter.UpdateItem(item) == string.Empty)
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
                    ItemDialogMessage = Message.AddItemSuccess;
                }
                else
                {
                    bool? result = dialogService.ShowDialog
                       (new MessageBoxDialogViewModel(errorMessage, Message.InventoryErrorTitle));
                }
            }
            else
            {
                // Make visible in UI where input is not valid.
            }
            

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
            if(BarcodeIsValid &&
               NameIsValid &&
               DescriptionIsValid &&
               PriceIsValid &&
               CategoryIsValid &&
               ModelIsValid &&
               NumberAvailableIsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
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
