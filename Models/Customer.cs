using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Customer
    {
        //int
        public int CustomerID { get; set; }

        //Nvarchar(50)?
        public string Name { get; set; }

        //Nvarchar(50)
        public string Tlf { get; set; }

        //Nvarchar(50)
        public string Email { get; set; }

        public bool Subscribed = false;

        int AmountOfSales = 0;
        int AmountSpent = 0;
    }
}
