﻿using System;
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
            foreach (var item in sale.Items)
            {
                RemoveItem(item);
            }
            foreach (var service in sale.Services)
            {
                RemoveService(service);
            }
            ClearSale();
        }

        public string AddItem(Item item)
        {
            Sale sale = Sale.Instance;
            sale.Items.Add(item);

            sale.PriceWithTax += item.PriceWithTax;

            item.NumberAvailable -= 1;
            return DatabaseWriter.UpdateItem(item);
        }

        public string RemoveItem(Item item)
        {
            Sale sale = Sale.Instance;
            if (sale.Items.Contains(item))
            {
                sale.Items.Remove(item);
                sale.PriceWithTax -= item.PriceWithTax;
                item.NumberAvailable += 1;
                return DatabaseWriter.UpdateItem(item);
            }
            else
            {
                return string.Empty;
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

        public void FinalizeSale(bool company, bool creditCard, double discountPercent)
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
