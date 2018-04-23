using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels 
{
    //Change name of TabItem to ITab item
    public class InventoryViewModel : BaseViewModel, ITabItem
    {
        private DatabaseReader databaseReader = new DatabaseReader();
        private ObservableCollection<Item> inventory = new ObservableCollection<Item>();
        private Item selectedItem;

        // The header is the name of the tab. It's set in the MainWindowViewModel ctor.
        public string Header { get; set; }

        public ObservableCollection<Item> Inventory
        {
            get { return inventory; }
            set
            {
                inventory = value;
                OnPropertyChanged();
            }
        }

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

        public InventoryViewModel()
        {
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
        private void AddItem()
        {
            
        }
        private void EditItem()
        {
            // Pass selected item into the dialog view model
        }
        private void DeleteItem()
        {

        }
    }
}
