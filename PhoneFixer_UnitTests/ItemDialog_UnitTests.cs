//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Models;
//using ViewModels;

//namespace PhoneFixer_UnitTests
//{
//    [TestClass]
//    class ItemDialog_UnitTests
//    {
//        ItemDialogViewModel itemDialogViewModel;
//        Item item;

//        [TestInitialize]
//        public void InitializeTest()
//        {
//            //itemDialogViewModel = new ItemDialogViewModel();
//            item = new Item();
//        }
        
//        [TestMethod]
//        public void ItemDataIsCorrectFormatBoolIsWorkingCorrectly()
//        {
//            // Not the one that is run in the tab control test.
//            string singleSpace = " ";
//            string numThree = "3";

//            itemDialogViewModel.Barcode = singleSpace;
//            itemDialogViewModel.Name = singleSpace;
//            itemDialogViewModel.Description = singleSpace;
//            itemDialogViewModel.Price = numThree;
//            itemDialogViewModel.Category = singleSpace;
//            itemDialogViewModel.Model = singleSpace;
//            itemDialogViewModel.NumberAvailable = numThree;

//            Assert.IsTrue(itemDialogViewModel.ItemDataIsCorrectFormat());


//            //public bool ItemDataIsCorrectFormat()
//            //{
//            //    // This needs to be tested
//            //    bool ret = true;

//            //    if (Barcode == string.Empty || Barcode.Length > 15)
//            //    {
//            //        ret = false;
//            //    }
//            //    else if (Name == string.Empty || Name.Length > 50)
//            //    {
//            //        ret = false;
//            //    }
//            //    else if (Description.Length > 150)
//            //    {
//            //        ret = false;
//            //    }
//            //    else if (!PriceCanParse)
//            //    {
//            //        ret = false;
//            //    }
//            //    else if (Category == string.Empty || Category.Length > 20)
//            //    {
//            //        ret = false;
//            //    }
//            //    else if (Model == string.Empty || Model.Length > 30)
//            //    {
//            //        ret = false;
//            //    }
//            //    else if (!NumberAvailableCanParse)
//            //    {
//            //        ret = false;
//            //    }

//            //    return ret;
//            //}
//        }
//    }
//}
