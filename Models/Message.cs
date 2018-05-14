using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Message
    {
        #region Error Messages
        public static string AddItemError = 
            "The system could not add the item to the inventory.\nPlease check your input.";
        public static string EditItemError = 
            "The system could not update the item.\nPlease check your input.";
        public static string DeleteItemError =
            "The system could not delete the selected item from the inventory.";
        public static string ItemInputError = 
            "The system could not perform that action./nPlease check your input.";
        public static string GetItemError =
            "The system could not retrieve the inventory from the database.";
        public static string DeleteCustomerError =
            "The system could not delete the selected customer.";
        public static string AddCustomerError = 
            "The system could not add the customer.\nPlease check your input.";
        public static string EditCustomerError = 
            "The system could not edit the customer.\nPlease check your input.";
        public static string GetCustomersError =
            "The system could not retrieve customers from the database.";
        public static string GetServiceError =
            "The system could not retrieve services from the database";
        public static string AddServiceError =
            "The system could not add the service.\nPlease check your input.";
        public static string EditServiceError =
            "The system could not edit the service.\nPlease check your input.";
        public static string ServiceInputError = 
            "The system could not perform that action.\nPlease check your input.";
        public static string DeleteServiceError =
            "The system could not delete the delected service.";
        #endregion

        #region Success Messages
        public static string AddItemSuccess =
            "Item added to inventory!";
        // Might not need this as if it's a success the dialog window should close.
        public static string EditItemSuccess =
            "Item edited successfully!";
        public static string AddCustomerSuccess = 
            "Customer added successfully!";
        #endregion


        #region DialogBox Window Titles
        // Window Titles
        public static string InventoryErrorTitle =
            "Inventory Error";
        public static string CustomerErrorTitle =
            "Customer Error";
        public static string ServiceErrorTitle = 
            "Service Error";
        public static string AddServiceTitle = 
            "Add Service";
        public static string EditServiceTitle = 
            "Edit Service";
        public static string SaleErrorTitle =
            "Sale Error";
        #endregion


        // We need to fix how long strings are portraied in the MessageBox
        public static string LongStringForTest = "hnjehnjehnje " + DeleteItemError + AddItemError + AddItemSuccess + EditItemSuccess;
    }
}
