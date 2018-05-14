using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SaleManager
    {
        private static Sale sale;
        public SaleManager()
        {
            sale = Sale.Instance;
        }

        public static void ClearSale()
        {
            
        }


    }
}
