using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Item
    {
        // NVarchar(15)
        public string Barcode { get; set; }
        
        // NVarchar(50)
        public string Name { get; set; }
        
        // NVarchar(150)
        public string Description { get; set; }
        
        // Decimal(18, 2)
        public decimal Price { get; set; }
        
        // NVarchar(20)
        public string Category { get; set; }
        
        // NVarchar(30)
        public string Model { get; set; }
        
        // Float <- why is this float ?                  - if int yet this needs to be changed.
        public int NumberAvailable { get; set; }
    }
}
