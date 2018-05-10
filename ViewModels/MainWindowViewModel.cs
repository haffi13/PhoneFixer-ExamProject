using Models;
using System.Collections.ObjectModel;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // Gets populated with tab items. The UI gets the Header and the Content
        // of the tabs from here. Header is the title, Content is the ViewModel.
        private ObservableCollection<ITabItem> tabViewModels = new ObservableCollection<ITabItem>();
        private ITabItem selectedTab;
        
        // Collection of view models inheriting the ITabItem inteface. 
        // Those view models are then displayed in corresponding tabs in the MainWindow.
        // The tab control in the MainWindow is bound to this property.
        public ObservableCollection<ITabItem> TabViewModels
        {
            get { return tabViewModels; }
            set
            {
                tabViewModels = value;
                OnPropertyChanged();
            }
        }

        public ITabItem SelectedTab
        {
            get { return selectedTab; }
            set
            {
                selectedTab = value;
                OnPropertyChanged();
            }
        }

        // Constructor adds view models to an ObservableCollection bound to a tab control 
        // in the MainWindow.
        public MainWindowViewModel(IDialogService dialogService)
        {
            TabViewModels.Add(new InventoryViewModel(dialogService) { Header = "Inventory" });
            TabViewModels.Add(new CustomerViewModel(dialogService)  { Header = "Customers" });
            TabViewModels.Add(new ServiceViewModel(dialogService)   { Header = "Service" });
            TabViewModels.Add(new SaleViewModel(dialogService)      { Header = "Sale" });

            SelectedTab = TabViewModels[0];

            
        }   
    }
}
