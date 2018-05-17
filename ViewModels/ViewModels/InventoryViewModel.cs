using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Models;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class InventoryViewModel : BaseViewModel, ITabItem
    {
        private ObservableCollection<Item> inventory = new ObservableCollection<Item>();
        private Item selectedItem;
        private SaleManager saleManager;

        // Keeps an instance of the DialogService instanciated at startup.
        private readonly IDialogService dialogService;

        // The header is the name of the tab. It's set in the MainWindowViewModel ctor.
        public string Header { get; set; }

        // Collection of items populated by the items table in the database and is
        // bound to the DataGrid in the InventoryView.
        public ObservableCollection<Item> Inventory
        {   
            // The reason for doing this in the properties is so the ItemSource for the Datagrid gets updated 
            // when the view is loaded. Had some 
            get
            {
                Dictionary<List<Item>, string> temp = DatabaseReader.GetInventory();
                string errorMessage = temp.Values.FirstOrDefault();
                if (errorMessage == string.Empty)
                {
                    inventory = new ObservableCollection<Item>(temp.Keys.FirstOrDefault());
                }
                else
                {
                    bool? result = dialogService.ShowDialog
                            (new MessageBoxDialogViewModel(Message.GetItemError + errorMessage, Message.InventoryErrorTitle));
                }
                return inventory;
            }
        }

        // The item the user selected in the DataGrid in the InventoryView.
        public Item SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand AddToSaleCommand { get; }
        
        
        public InventoryViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            AddCommand = new RelayCommand(AddItem);
            EditCommand = new RelayCommand(EditItem);
            DeleteCommand = new RelayCommand(DeleteItem);
            AddToSaleCommand = new RelayCommand(AddToSale);

            saleManager = SaleManager.Instance;
        }

        // Opens a dialog box for the user to add a Item to the database.
        private void AddItem()
        {
            bool? result = dialogService.ShowDialog(new ItemDialogViewModel(dialogService));
            OnPropertyChanged(nameof(Inventory));
        }

        // Opens a dialog box for the user to edit the selected item in the datagrid in the
        // InventoryView. Updates the corresponding Item object in the database.
        private void EditItem()
        {
            if(SelectedItem != null)
            {
                bool? result = dialogService.ShowDialog(new ItemDialogViewModel(dialogService, SelectedItem));
                OnPropertyChanged(nameof(Inventory));
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
                    OnPropertyChanged(nameof(Inventory));
                }
            }
        }

        // Adds the selected item to the sale.
        private void AddToSale()
        {
            if(SelectedItem != null && SelectedItem.NumberAvailable > 0)
            {
                string errorMessage = string.Empty;
                saleManager.AddItemToSale(SelectedItem);
                //string errorMessage = saleManager.AddItem(selectedItem);
                if(errorMessage != string.Empty)
                {
                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(errorMessage, Message.InventoryErrorTitle));
                }
                else
                {
                    OnPropertyChanged(nameof(Inventory));
                }
            }
        }
    }
}
