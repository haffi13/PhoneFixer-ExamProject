namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
    public class Customer
    {
        // Int not null
        public int CustomerID { get; set; }

        // Nvarchar(50) not null
        public string CustomerName { get; set; }

        // Nvarchar(50) not null
        public string CustomerPhone { get; set; }

        // Nvarchar(50) not null
        public string Email { get; set; }
        
        // Bool not null
        public bool Subscribed { get; set; } 

        // Bool not null
        public bool ItemInService { get; set; }
    }
}
