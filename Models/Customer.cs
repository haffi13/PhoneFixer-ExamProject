namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
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
