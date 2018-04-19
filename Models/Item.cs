using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Item
    {
        // NVarchar(15)
        string Barcode { get; set; }
        
        // NVarchar(50)
        string Name { get; set; }
        
        // NVarchar(150)
        string Description { get; set; }
        
        // Decimal(18, 2)
        decimal Price { get; set; }
        
        // NVarchar(20)
        string Category { get; set; }
        
        // NVarchar(30)
        string Model { get; set; }
        
        // Float <- why is this float ?
        float Number_Available { get; set; }
    }
}
