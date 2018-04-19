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
        DatabaseReader databaseReader = new DatabaseReader();
        private ObservableCollection<Item> items = new ObservableCollection<Item>();


        // The header is the name of the tab. It's set in the MainWindowViewModel ctor.
        public string Header { get; set; }

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddItemCmd { get; set; }
        public RelayCommand EditItemCmd { get; set; }
        public RelayCommand DeleteItemCmd { get; set; }

        public InventoryViewModel()
        {
            RefreshInventory();

            this.AddItemCmd = new RelayCommand(AddItem);
            this.EditItemCmd = new RelayCommand(EditItem);
            this.DeleteItemCmd = new RelayCommand(DeleteItem);
        }

        // Populates the datagrid with all items in the Item table in the database.
        private void RefreshInventory()
        {
            Items = new ObservableCollection<Item>(databaseReader.ExecuteQuery());
        }
        private void AddItem()
        {
            
        }
        private void EditItem()
        {

        }
        private void DeleteItem()
        {

        }
    }
}
