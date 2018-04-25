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
        ItemDialogViewModel itemDialogViewModel;
        ObservableCollection<ITabItem> tabItem = new ObservableCollection<ITabItem>();

        // Doesn't work after implementing the dialog service.
        [TestInitialize]
        public void InitializeTest()
        {
            //tabItem.Add(new InventoryViewModel { Header = "Inventory" });
            //tabItem.Add(new InventoryViewModel { Header = "Service" });
            //mainWindowViewModel = new MainWindowViewModel();
            //itemDialogViewModel = new ItemDialogViewModel();
        }


        [TestMethod]
        public void TabItemHeaderRegistersCorrectly()
        {
            Assert.AreEqual(tabItem[0].Header, mainWindowViewModel.TabViewModels[0].Header);
            Assert.AreEqual(tabItem[1].Header, mainWindowViewModel.TabViewModels[1].Header);
        }

        // Tests if the view models for the views that are displayed in the tab control
        // can be added to a collection of ITabItem interfaces.
        [TestMethod]
        public void ITabItemCanBeAssignedToRelevantViewModels()
        {
            Assert.IsTrue(typeof(ITabItem).IsAssignableFrom(typeof(InventoryViewModel)));
            Assert.IsTrue(typeof(ITabItem).IsAssignableFrom(typeof(ServiceViewModel)));
            Assert.IsTrue(typeof(ITabItem).IsAssignableFrom(typeof(CustomerViewModel)));
        }

        // Tests if the values of the public properties are in the correct format
        // if the values are at the maximum lenght/size limit of set in the database table.
        // Only tests the bool method in the ItemDialogViewModel, does not check if the 
        // values can be added to the database.
        [TestMethod]
        public void ItemDataIsCorrectFormatBoolReturnsTrueIfAllValuesAreAtUpperSizeLimits()
        {
            string varchar10 = "1234567891";
            string varchar15 = "123456789123456";
            string varchar20 = varchar10 + varchar10;
            string varchar30 = varchar20 + varchar10;
            string varchar50 = varchar30 + varchar20;
            string varchar150 = varchar50 + varchar50 + varchar50;
            decimal maxDecimal = decimal.MaxValue;
            string sMaxDecimal = maxDecimal.ToString();
            Int32 maxInt = Int32.MaxValue;
            string sMaxInt = maxInt.ToString();

            itemDialogViewModel.Barcode = varchar15;
            itemDialogViewModel.Name = varchar50;
            itemDialogViewModel.Description = varchar150;
            itemDialogViewModel.Price = sMaxDecimal;
            itemDialogViewModel.Category = varchar20;
            itemDialogViewModel.Model = varchar30;
            itemDialogViewModel.NumberAvailable = sMaxInt;

            Assert.IsTrue(itemDialogViewModel.ItemDataIsCorrectFormat());
        }

        // Tests if the values of the public properties are in the correct format
        // if the values are at the lower lenght / size limit set in the database table.
        // Only tests the bool method in the ItemDialogViewModel, does not check if the 
        // values can be added to the database.
        [TestMethod]
        public void ItemDataIsCorrectFormatBoolReturnsTrueIfAllValuesAreWhitespaceOrSingleNumber()
        {
            string singleSpace = " ";
            string numThree = "3";

            itemDialogViewModel.Barcode = singleSpace;
            itemDialogViewModel.Name = singleSpace;
            itemDialogViewModel.Description = singleSpace;
            itemDialogViewModel.Price = numThree;
            itemDialogViewModel.Category = singleSpace;
            itemDialogViewModel.Model = singleSpace;
            itemDialogViewModel.NumberAvailable = numThree;

            Assert.IsTrue(itemDialogViewModel.ItemDataIsCorrectFormat());
        }
    }
}
