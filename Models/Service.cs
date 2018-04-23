using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Service
    {
        //int
        public int ServiceID { get; set; }

        //Nvarchar(150)
        public string Description { get; set; }

        //decimal(18, 2)
        public decimal Price { get; set; }

        public bool Done = false;
        public bool Pickup = false;
    }
}
