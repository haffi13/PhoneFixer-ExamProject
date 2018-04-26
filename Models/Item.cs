namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
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
        
        // Int
        public int NumberAvailable { get; set; }
    }
}
