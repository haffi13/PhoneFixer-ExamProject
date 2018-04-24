using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModels;
using Models;
using System.Collections.ObjectModel;

namespace PhoneFixer_UnitTests
{
    [TestClass]
    public class TabControl_UnitTests
    {
        MainWindowViewModel mainWindowViewModel;
        ObservableCollection<ITabItem> tabItem = new ObservableCollection<ITabItem>();


        [TestInitialize]
        public void InitializeTest()
        {
            tabItem.Add(new InventoryViewModel { Header = "Inventory" });
            tabItem.Add(new InventoryViewModel { Header = "Service" });
            mainWindowViewModel = new MainWindowViewModel();
        }


        [TestMethod]
        public void TabItemHeaderRegistersCorrectly()
        {
            Assert.AreEqual(tabItem[0].Header, mainWindowViewModel.TabViewModels[0].Header);
            Assert.AreEqual(tabItem[1].Header, mainWindowViewModel.TabViewModels[1].Header);
        }

        [TestMethod]
        public void ITabItemCanBeAssignedToRelevantViewModels()
        {
            Assert.IsTrue(typeof(ITabItem).IsAssignableFrom(typeof(InventoryViewModel)));
            Assert.IsTrue(typeof(ITabItem).IsAssignableFrom(typeof(ServiceViewModel)));
            Assert.IsTrue(typeof(ITabItem).IsAssignableFrom(typeof(CustomerViewModel)));
        }

        [TestMethod]
        public void Test()
        {
            
        }
    }
}
