using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public sealed class SaleManager
    {
        private static SaleManager instance = null;
        private static readonly object padlock = new object();

        private Receipt receipt = new Receipt();

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

        public Receipt Receipt
        {
            get { return receipt; }
            set { receipt = value; }
        }

        // The constructor is private so an instance can only be created through the static properties "Instance"
        private SaleManager()
        {

        }

        public void ClearSale()
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
            Receipt = new Receipt();
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
            AddProductToReceipt(item);
        }

        public void RemoveItemFromSale(Item item)
        {
            Sale sale = Sale.Instance;
            if (sale.Items.Contains(item))
            {
                sale.Items.Remove(item);
                sale.PriceWithTax -= item.PriceWithTax;
                RemoveProductFromReceipt(item);
            }
        }

        public void AddServiceToSale(Service service)
        {
            Sale sale = Sale.Instance;
            sale.Services.Add(service);
            sale.PriceWithTax += service.PriceWithTax;
            AddProductToReceipt(service);
        }

        public void RemoveServiceFromSale(Service service)
        {
            Sale sale = Sale.Instance;
            if (sale.Services.Contains(service))
            {
                sale.Services.Remove(service);
                sale.PriceWithTax -= service.PriceWithTax;
                RemoveProductFromReceipt(service);
            }
        }

        private void AddProductToReceipt(object product)
        {
            ReceiptNode node = new ReceiptNode();
            if (product is Item item)
            {
                node.Name = item.Name;
                node.Price = item.PriceWithTax;
            }
            else if (product is Service service)
            {
                node.Name = service.ServiceName;
                node.Price = service.PriceWithTax;
            }
            Receipt.AddNode(node);
        }

        private void RemoveProductFromReceipt(object product)
        {
            string name = string.Empty;
            decimal price = 0;
            if (product is Item item)
            {
                name = item.Name;
                price = item.PriceWithTax;
            }
            else if (product is Service service)
            {
                name = service.ServiceName;
                price = service.PriceWithTax;
            }
            if (name != string.Empty && price != 0)
            {
                foreach (var node in Receipt.Nodes)
                {
                    if (name == node.Name && price == node.Price)
                    {
                        Receipt.Nodes.Remove(node);
                        break;
                    }
                }
            }
        }

        public string FinalizeSale()
        {
            Sale sale = Sale.Instance;
            string errorMessage = string.Empty;
            if (SaleValidity.SaleIsValid())
            {
                sale.TimeOfSale = DateTime.Now;
                sale.TaxOnSale = sale.PriceWithTax - ValueAddedTax.RemoveVAT(sale.PriceWithTax);
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
                    item.NumberAvailable--;
                    errorMessage = DatabaseWriter.UpdateItem(item);
                    if(errorMessage != string.Empty)
                    {
                        return errorMessage;
                    }
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
