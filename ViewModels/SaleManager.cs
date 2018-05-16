using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Threading.Tasks;

namespace ViewModels
{
    // This should be in the ViewModel layer where it can access the database.
    // Then it's possible to update the inventory when items are added to a sale.

    public sealed class SaleManager//singlt
    {
        // Decided to do this instead of creating a new instance.
        // As all relevant classes have access to the same instance of Sale it might be better
        // to clear all the values instead of creating a new instance as it might be problematic
        // to let the other classes know there is a new instance if one creates it.

        private static SaleManager instance = null;
        private static readonly object padlock = new object();

        public static SaleManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new SaleManager();
                    }
                    return instance;
                }
            }
        }

        private SaleManager()
        {

        }

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

        public void CancelSale()
        {
            Sale sale = Sale.Instance;
            if(sale.Items.Count > 0)
            {
                for (int i = 0; i < sale.Items.Count; i++)
                {
                    RemoveItem(sale.Items[i]);
                }
            }
            if(sale.Services.Count > 0)
            {
                for (int i = 0; i < sale.Services.Count; i++)
                {
                    RemoveService(sale.Services[i]);
                }
            }
            ClearSale();
        }

        public void AddItem(Item item) 
        {
            Sale sale = Sale.Instance;
            sale.Items.Add(item);

            sale.PriceWithTax += item.PriceWithTax;

            //item.NumberAvailable -= 1;
            //return DatabaseWriter.UpdateItem(item); // should not do this, not included on SD
        }

        public void RemoveItem(Item item)
        {
            Sale sale = Sale.Instance;
            if (sale.Items.Contains(item))
            {
                sale.Items.Remove(item);
                sale.PriceWithTax -= item.PriceWithTax;
                //item.NumberAvailable += 1;              // Do not do this here, same for service.
                //return DatabaseWriter.UpdateItem(item);
            }
        }

        public void AddService(Service service)
        {
            Sale sale = Sale.Instance;
            sale.Services.Add(service);
            sale.PriceWithTax += service.PriceWithTax;
        }

        public void RemoveService(Service service)
        {
            Sale sale = Sale.Instance;
            if (sale.Services.Contains(service))
            {
                sale.Services.Remove(service);
                sale.PriceWithTax -= service.PriceWithTax;
            }
        }

        public string FinalizeSale()
        {
            Sale sale = Sale.Instance;
            SaleValidity saleValidity = new SaleValidity();
            string errorMessage = string.Empty;
            if (saleValidity.SaleIsValid())
            {
                sale.TimeOfSale = DateTime.Now;
                sale.TaxOnSale = sale.PriceWithTax - ValueAddedTax.RemoveVAT(sale.PriceWithTax);
                foreach (var item in sale.Items)
                {
                    item.NumberAvailable--;
                    errorMessage = DatabaseWriter.UpdateItem(item);
                    if(errorMessage != string.Empty) 
                    {
                        break;
                    }
                }
            }

            // WRITE THE SALE TO THE DATABASE!
            // Write to the junction tables.
            ClearSale();
            return errorMessage;
        }
    }
}
