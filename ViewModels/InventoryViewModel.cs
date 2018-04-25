﻿using System.Collections.ObjectModel;
using Models;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class InventoryViewModel : BaseViewModel, ITabItem
    {
        private DatabaseReader databaseReader = new DatabaseReader();
        private ObservableCollection<Item> inventory = new ObservableCollection<Item>();
        private Item selectedItem;
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

        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand EditItemCommand { get; set; }
        public RelayCommand DeleteItemCommand { get; set; }

        public InventoryViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            RefreshInventory();

            AddItemCommand = new RelayCommand(AddItem);
            EditItemCommand = new RelayCommand(EditItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
        }

        // Populates the datagrid with all the items in the Item table in the database.
        private void RefreshInventory()
        {
            Inventory = new ObservableCollection<Item>(databaseReader.ExecuteQuery());
        }

        // Opens a dialog box for the user to add a Item to the database.
        private void AddItem()
        {
            //var viewModel = new ItemDialogViewModel();

            bool? result = dialogService.ShowDialog(new ItemDialogViewModel());



            RefreshInventory();
        }

        // Opens a dialog box for the user to edit the selected item in the datagrid in the
        // InventoryView. Updates the corresponding Item object in the database.
        private void EditItem()
        {
            if(SelectedItem != null)
            {
                bool? result = dialogService.ShowDialog(new ItemDialogViewModel(SelectedItem));
                RefreshInventory();
            }
            // Pass selected item into the dialog view model
        }
        private void DeleteItem()
        {

        }
    }
}
