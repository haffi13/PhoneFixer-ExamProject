﻿using System.Collections.ObjectModel;
using Models;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class InventoryViewModel : BaseViewModel, ITabItem
    {
        private ObservableCollection<Item> inventory = new ObservableCollection<Item>();
        private Item selectedItem;

        // Keeps an instance of the DialogService instanciated at startup.
        private readonly IDialogService dialogService;

        // The header is the name of the tab. It's set in the MainWindowViewModel ctor.
        public string Header { get; set; }

        // Collection of items populated by the items table in the database and is
        // bound to the DataGrid in the InventoryView.
        public ObservableCollection<Item> Inventory
        {
            get { return inventory; }
            set
            {
                inventory = value;
                OnPropertyChanged();
            }
        }

        // The item the user selected in the DataGrid in the InventoryView.
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        
        
        public InventoryViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            RefreshInventory();

            AddCommand = new RelayCommand(AddItem);
            EditCommand = new RelayCommand(EditItem);
            DeleteCommand = new RelayCommand(DeleteItem);
        }

        // Populates the datagrid with all the items in the Item table in the database.
        private void RefreshInventory()
        {
            Inventory = new ObservableCollection<Item>(DatabaseReader.GetInventory());
        }

        // Opens a dialog box for the user to add a Item to the database.
        private void AddItem()
        {
            bool? result = dialogService.ShowDialog(new ItemDialogViewModel(dialogService));
            RefreshInventory();
        }

        // Opens a dialog box for the user to edit the selected item in the datagrid in the
        // InventoryView. Updates the corresponding Item object in the database.
        private void EditItem()
        {
            if(SelectedItem != null)
            {
                bool? result = dialogService.ShowDialog(new ItemDialogViewModel(SelectedItem, dialogService));
                RefreshInventory();
            }
        }

        // Deletes the selected item in the datagrid in the InventoryView.
        private void DeleteItem()
        {
            if (SelectedItem != null)
            {
                string errorMessage = DatabaseWriter.DeleteItem(SelectedItem);
                if(errorMessage != string.Empty)
                {
                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(errorMessage, Message.InventoryErrorTitle));
                }
                else
                {
                    RefreshInventory();
                }
            }
        }
    }
}
