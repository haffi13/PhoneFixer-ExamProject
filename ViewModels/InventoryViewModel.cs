using System;
using System.Collections.Generic;
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
        
        // The header is the name of the tab. It's set in the MainWindowViewModel ctor.
        public string Header { get; set; }

        

        public RelayCommand AddItemCmd { get; set; }
        public RelayCommand EditItemCmd { get; set; }
        public RelayCommand DeleteItemCmd { get; set; }

        public InventoryViewModel()
        {
            this.AddItemCmd = new RelayCommand(AddItem);
            this.EditItemCmd = new RelayCommand(EditItem);
            this.DeleteItemCmd = new RelayCommand(DeleteItem);
        }

        private void AddItem()
        {
            Item item = new Item
            {
                Barcode = ItemBarcode,
                Name = ItemName,
                Description = ItemDescription,
                Price = ItemPrice,
                Category = ItemCategory,
                Model = ItemModel,
                NumberAvailable = ItemNumberAvailable
            };
        }
        private void EditItem()
        {

        }
        private void DeleteItem()
        {

        }
    }
}
