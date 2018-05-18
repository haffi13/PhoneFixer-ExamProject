using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
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
                    if (instance == null)
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
            sale.DiscountPercentage = 0;
        }

        public void CancelSale()
        {
            Sale sale = Sale.Instance;
            if (sale.Items.Count > 0)
            {
                for (int i = 0; i < sale.Items.Count; i++)
                {
                    RemoveItemFromSale(sale.Items[i]);
                }
            }
            if (sale.Services.Count > 0)
            {
                for (int i = 0; i < sale.Services.Count; i++)
                {
                    RemoveServiceFromSale(sale.Services[i]);
                }
            }
            ClearSale();
        }

        public void AddItemToSale(Item item)
        {
            Sale sale = Sale.Instance;
            sale.Items.Add(item);

            sale.PriceWithTax += item.PriceWithTax;

            //item.NumberAvailable -= 1;
            //return DatabaseWriter.UpdateItem(item); // should not do this, not included on SD
        }

        public void RemoveItemFromSale(Item item)
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

        public void AddServiceToSale(Service service)
        {
            Sale sale = Sale.Instance;
            sale.Services.Add(service);
            sale.PriceWithTax += service.PriceWithTax;
        }

        public void RemoveServiceFromSale(Service service)
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
                foreach (var item in sale.Items) // Might make more sense to do this while adding items to 
                                                // the junciton table when documenting the sale.
                {
                    item.NumberAvailable--;
                    errorMessage = DatabaseWriter.UpdateItem(item);
                    if (errorMessage != string.Empty)
                    {
                        return errorMessage;
                    }
                }
                errorMessage = DocumentSale();
                ClearSale();
            }

            return errorMessage;
        }

        private string DocumentSale()
        {
            Sale sale = Sale.Instance;
            Dictionary<int, string> temp = DatabaseWriter.CreateSale();
            string errorMessage = temp.Values.FirstOrDefault();
            sale.SaleId = temp.Keys.FirstOrDefault();
            if (errorMessage == string.Empty && sale.SaleId != 0)
            {
                foreach (var item in sale.Items)
                {
                    errorMessage = DatabaseWriter.AddToSaleItem(item);
                    if (errorMessage != string.Empty)
                    {
                        return errorMessage;
                    }
                }

                foreach (var service in sale.Services)
                {
                    errorMessage = DatabaseWriter.AddToSaleService(service);
                    if (errorMessage != string.Empty)
                    {
                        return errorMessage;
                    }
                }
            }

            return errorMessage;
        }
    }
}
