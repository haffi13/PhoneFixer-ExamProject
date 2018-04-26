namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
    class Service
    {
        // Int
        public int ServiceID { get; set; }

        // Nvarchar(150)
        public string Description { get; set; }

        // Decimal(18, 2)
        public decimal Price { get; set; }
        
        // Bool
        public bool Done { get; set; } = false;
        
        // Bool
        public bool Pickup { get; set; } = false;
    }
}
