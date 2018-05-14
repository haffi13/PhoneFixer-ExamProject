using System;
using System.Collections.Generic;

namespace Models
{
    public sealed class Sale
    {
        // Add comments to explain the padlock -----------------------------------------------------------------------------------
        private static Sale instance = null;
        private static readonly object padlock = new object();

        public void NewInstance()
        {
            instance = new Sale();
        }
        public static Sale Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new Sale();
                    }
                    return instance;
                }
            }
        }
        
        // The constructor is private so an instance can only be created through the static Sale properties Instance
        private Sale()
        {
            Items = new List<Item>();
            Services = new List<Service>();
        }
        // Int - autoId
        public int SaleId { get; set; }
        // List of items
        public List<Item> Items { get; set; }
        // List of services
        public List<Service> Services { get; set; }
        // DateTime
        public DateTime TimeOfSale { get; set; }
        // Decimal (18,2)
        public decimal TaxOnSale { get; set; }
        // Bool - true if company. To know if to include moms in price.
        public bool Company { get; set; }
        // Decimal(18,2) 
        public decimal PriceWithTax { get; set; }
        // Bool - true if payment is with credit card and false if payment is with cash.
        public bool CreditCard { get; set; }
        // Percentage
        public double DiscountPercent { get; set; }
    }
}
