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

        private static Sale instance = null;
        private static readonly object padlock = new object();

        private SaleInstance()
        {

        }
        public static Sale Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Sale();
                    }
                    return instance;
                }

            }
        }
    }
}
    

