﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Models;
namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // Gets populated with tab items. The UI gets the Header and the Content
        // of the tabs from here. Header is the title, Content is the ViewModel.
        private ObservableCollection<ITabItem> tabViewModels = new ObservableCollection<ITabItem>();

        // Maybe change the name of TabItems to be more specific/descriptive.
        public ObservableCollection<ITabItem> TabViewModels
        {
            get { return tabViewModels; }
            set
            {
                tabViewModels = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            TabViewModels.Add(new InventoryViewModel { Header = "Inventory" });
            TabViewModels.Add(new ServiceViewModel   { Header = "Service" });
            TabViewModels.Add(new CustomerViewModel  { Header = "Customers" });   
        }   
    }
}
