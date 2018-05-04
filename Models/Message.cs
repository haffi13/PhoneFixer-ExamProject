using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Message
    {
        // Error Messages
        public static string AddItemError = 
            "The system could not add the item to the inventory.\nPlease check your input.";
        public static string DeleteItemError =
            "The system could not delete the selected item from the inventory.";


        // Success Messsages
        public static string AddItemSuccess =
            "Item added to inventory!";
        // Might not need this as if it's a success the dialog window should close.
        public static string EditItemSuccess =
            "Item edited successfully!";

        // Window Titles
        public static string InventoryErrorTitle =
            "Inventory Error";

        public static string LongStringForTest = "hnjehnjehnje " + DeleteItemError + AddItemError + AddItemSuccess + EditItemSuccess;
    }
}
