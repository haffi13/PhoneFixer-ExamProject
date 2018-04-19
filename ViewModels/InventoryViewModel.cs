using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Models;

namespace ViewModels 
{
    //Change name of TabItem to ITab item
    public class InventoryViewModel : BaseViewModel, ITabItem
    {
        DatabaseReader databaseReader = new DatabaseReader();
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

        }
        private void EditItem()
        {

        }
        private void DeleteItem()
        {

        }
    }
}
