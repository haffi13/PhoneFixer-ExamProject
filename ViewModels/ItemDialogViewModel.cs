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

        #region OBSOLETE
        // OBSOLETE!

        // Bool which returns if the value in the Price textbox in the ItemDialogView
        // can be parced to a decimal value.
        //public bool PriceCanParse
        //{
        //    get { return priceCanParse; }
        //    set { priceCanParse = value; }
        //}

        // Bool which returns if the value in the NumberAvailable textbox in the ItemDialogView
        // can be parced to a Int value.
        //public bool NumberAvailableCanParse
        //{
        //    get { return numberAvailableCanParse; }
        //    set { numberAvailableCanParse = value; }
        //}
        // Bound to the Barcode textbox in the ItemDialogView. Makes it un-editable.
        #endregion
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
        public string Price
        {
            get { return item.Price.ToString(); }
            set
            {
                if (InputValidity.Price(value))
                {
                    item.Price = decimal.Parse(value);
                    PriceWithTax = ValueAddedTax.AddVAT(item.Price).ToString();
                    PriceIsValid = true;
                }
                else
                {
                    PriceIsValid = false;
                }
                OnPropertyChanged();
            }
        }

        public string PriceWithTax
        {
            get { return item.PriceWithTax.ToString(); }
            set
            {
                if (InputValidity.Price(value))
                {
                    item.PriceWithTax = decimal.Parse(value);
                    Price = ValueAddedTax.RemoveVAT(item.PriceWithTax).ToString();
                    PriceIsValid = true;
                }
                else
                {
                    PriceIsValid = false;
                }
                OnPropertyChanged();
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

        public string LastAddDate
        {
            get { return item.LastTimeAdded; }
            set
            {
                item.LastTimeAdded = DateTime.Now.ToString();
                OnPropertyChanged();
            }
        }

        public string NumberAvailable
        {
            get { return item.NumberAvailable.ToString(); }
            set
            {
                if (InputValidity.NumberAvailable(value))
                {
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
            Category = item.Category;
            Model = item.Model;
            NumberAvailable = item.NumberAvailable.ToString();
        }

        // Method called when the Confirm button is clicked.
        // The ItemUpdate stored procedure can handle adding and editing items
        private void Confirm()
        {
            if (ItemDataIsCorrectFormat())
            {
                string errorMessage = DatabaseWriter.UpdateItem(item);
                //
                //  Insert bool to check if database operation goes smooooooth.
                if (errorMessage == string.Empty)
                {

                    ClearPublicProperties();
                    item = new Item();
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
            Category = string.Empty;
            Model = string.Empty;
            NumberAvailable = string.Empty;
        }
    }
}
