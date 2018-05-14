using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class SaleManager
    {
        public static void ClearSale()
        {
            Sale sale = Sale.Instance;
            sale.SaleId = 0;
            sale.Items = new List<Item>();
            sale.Services = new List<Service>();
            sale.TimeOfSale = DateTime.MinValue;
            sale.TaxOnSale = 0m;
            sale.PriceWithTax = 0m;
            sale.Company = false;
            sale.CreditCard = false;
            sale.DiscountPercent = 0;
        }

        public static void AddItem(Item item)
        {
            Sale sale = Sale.Instance;
            sale.Items.Add(item);
            sale.PriceWithTax += item.PriceWithTax;   
        }

        public static void RemoveItem(Item item)
        {
            Sale sale = Sale.Instance;
            if (sale.Items.Contains(item))
            {
                sale.Items.Remove(item);
                sale.PriceWithTax -= item.PriceWithTax;
            }
        }

        public static void AddService(Service service)
        {
            Sale sale = Sale.Instance;
            sale.Services.Add(service);
            sale.PriceWithTax += service.PriceWithTax;
        }

        public static void RemoveService(Service service)
        {
            Sale sale = Sale.Instance;
            if (sale.Services.Contains(service))
            {
                sale.Services.Remove(service);
                sale.PriceWithTax -= service.PriceWithTax;
            }
        }

        public static void FinalizeSale(bool company, bool creditCard, double discountPercent)
        {
            Sale sale = Sale.Instance;
            sale.TimeOfSale = DateTime.Now;
            sale.TaxOnSale = sale.PriceWithTax - ValueAddedTax.RemoveVAT(sale.PriceWithTax);
            sale.Company = company;
            sale.CreditCard = creditCard;
            sale.DiscountPercent = discountPercent;
        }
    }
}
