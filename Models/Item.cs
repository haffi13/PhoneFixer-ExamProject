namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
    public class Item
    {
        // NVarchar(15) not null
        public string Barcode { get; set; }

        // NVarchar(50) not null
        public string Name { get; set; }
        
        // NVarchar(150)
        public string Description { get; set; }

        // Decimal(18, 2) not null
        public decimal Price { get; set; }

        // Decimal(18,2) not null
        public decimal PriceWithTax { get; set; }

        // NVarchar(20) not null
        public string Category { get; set; }

        // NVarchar(30) not null
        public string Model { get; set; }

        // Datetime not null
        public string LastTimeAdded { get; set; }

        // Int not null
        public int NumberAvailable { get; set; }
    }
}
