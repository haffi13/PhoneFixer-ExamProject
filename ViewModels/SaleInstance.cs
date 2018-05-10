using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels
{
    // Singleton pattern. Accessed from the ServiceViewModel, InventoryViewModel, SaleViewModel
    public sealed class SaleInstance
    {
        Sale sale;
        public SaleInstance()
        {
            sale = new Sale();
        }

    }
}
