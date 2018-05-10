using System;

namespace Models
{
    // Comments indicate the variable type and constraints in the service table in the database.
    public class Service
    {
        // Int
        public int ServiceNumber { get; set; }

        public string ServiceName { get; set; }

        // Nvarchar(150)
        public string ServiceDescription { get; set; }

        // Decimal(18, 2)
        public decimal PriceNoTax { get; set; }

        // Decimal(18, 2)
        public decimal PriceWithTax { get; set; }

        // DateTime
        public DateTime DayCreated { get; set; }

        // DateTime
        public DateTime? DayServiced { get; set; }

        // Bool
        public bool Repaired { get; set; }
        
        // Int - foregin key - connects the Service to the customer getting the service.
        public int CustomerId { get; set; }
        
    }
}
