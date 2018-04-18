using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // Gets populated with tab items. The UI gets the Header and the Content
        // of the tabs from here. Header is the title, Content is the ViewModel.
        private ObservableCollection<TabItem> tabItems = new ObservableCollection<TabItem>();

        // Maybe change the name of TabItems to be more specific/descriptive.
        public ObservableCollection<TabItem> TabItems
        {
            get { return tabItems; }
            set
            {
                tabItems = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            TabItems.Add(new InventoryViewModel { Header = "Inventory" });
        }
        
    }
}
