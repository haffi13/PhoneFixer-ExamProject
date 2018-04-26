namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
    class Customer
    {
        // Int not null
        public int CustomerID { get; set; }

        // Nvarchar(50) not null
        public string Name { get; set; }

        // Nvarchar(50)
        public string Tlf { get; set; }

        // Nvarchar(50) 
        public string Email { get; set; }
        
        // Bool not null
        public bool Subscribed { get; set; } = false;

        // Int not null
        int AmountOfSales { get; set; }

        // Int
        int AmountSpent { get; set; }
    }
}
