using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Customer
    {
        // Int
        public int CustomerID { get; set; }

        // Nvarchar(50)
        public string Name { get; set; }

        // Nvarchar(50)
        public string Tlf { get; set; }

        // Nvarchar(50)
        public string Email { get; set; }
        
        // Bool
        public bool Subscribed { get; set; } = false;

        // Int
        int AmountOfSales { get; set; }

        // Int
        int AmountSpent { get; set; }
    }
}
