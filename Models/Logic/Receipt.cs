using System.Collections.Generic;

namespace Models
{
    public class Receipt
    {
        public List<ReceiptNode> Nodes;
        public Receipt()
        {
            Nodes = new List<ReceiptNode>();
        }
    }
}
//        public static List<string> GetReceiptPerson()
//        {
//            Sale sale = Sale.Instance;
//            List<string> ret = new List<string>();
//            string temp = string.Empty;

//            foreach (var item in sale.Items)
//            {
//                temp = "Vare: " + item.Name + "\n" +
//                       "Pris: "   + item.PriceWithTax;
//                ret.Add(temp);
//            }

//            foreach (var service in sale.Services)
//            {
//                temp = "Service: " + service.ServiceName + "\n" +
//                       "Pris: "    + service.PriceWithTax;
//                ret.Add(temp);
//            }

//            temp = "Total pris: " + sale.PriceWithTax + "\n" +
//                   "Heraf moms: " + (sale.PriceWithTax - ValueAddedTax.RemoveVAT(sale.PriceWithTax));

//            return ret;
//        }

//        //public static string GetReceiptCompany()
//        //{

//        //}
//    }
//}
