using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ViewModels
{
    public static class DatabaseReader
    {
        private static string url      = "EALSQL1.eal.local";
        private static string database = "DB2017_B12";
        private static string username = "USER_B12";
        private static string password = "SesamLukOp_12";

        private static string connectionString = string.Format
            ("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);


        // Returns a list of all Item objects in the Item table in the database.
        public static List<Item> GetInventory()
        {
            List<Item> Items = new List<Item>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllItems", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Item temp = new Item
                            {
                                Barcode = reader["Barcode"].ToString(),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = decimal.Parse(reader["Price"].ToString()),
                                PriceWithTax = decimal.Parse(reader["PriceWithTax"].ToString()),
                                Category = reader["Category"].ToString(),
                                Model = reader["Model"].ToString(),
                                LastTimeAdded = DateTime.Parse(reader["LastAddDay"].ToString()),
                                NumberAvailable = int.Parse(reader["NumberAvailable"].ToString())
                            };

                            Items.Add(temp);
                        }
                    }
                }

                catch (SqlException e)
                {
                    
                }
            }
            return Items;
        }


        public static Dictionary<List<Customer>, string> GetCustomers()
        {
            Dictionary<List<Customer>, string> ret = new Dictionary<List<Customer>, string>();
            List<Customer> Customers = new List<Customer>();
            string errorMessage = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllCustomers", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Customer temp = new Customer
                            {
                                CustomerID = (int)reader["CustomerId"],
                                CustomerName = (string)reader["CustomerName"],
                                CustomerPhone = reader["CustomerPhone"].ToString(),
                                Email = reader["Email"].ToString(),
                                Subscribed = (bool)reader["Subscribed"],
                                ItemInService = (bool)reader["ItemInService"]
                            };

                            Customers.Add(temp);
                        }
                    }
                }
                catch (SqlException e)
                {
                    errorMessage = "\n\n" + e.Message;
                }
            }
            ret.Add(Customers, errorMessage);
            return ret;
        }

        //public static List<Customer> GetCustomers()
        //{
        //    List<Customer> Customers = new List<Customer>();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("GetAllCustomers", con)
        //            {
        //                CommandType = CommandType.StoredProcedure
        //            };
        //            con.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    Customer temp = new Customer
        //                    {
        //                        CustomerID = (int)reader["CustomerId"],
        //                        CustomerName = (string)reader["CustomerName"],
        //                        CustomerPhone = reader["CustomerPhone"].ToString(),
        //                        Email = reader["Email"].ToString(),
        //                        Subscribed = (bool)reader["Subscribed"],
        //                        ItemInService = (bool)reader["ItemInService"]
        //                    };

        //                    Customers.Add(temp);
        //                }
        //            }
        //        }

        //        catch (SqlException e)
        //        {
        //            //----------------------------------
        //            //      Deal with this!
        //            //----------------------------------
        //            MessageBox.Show(e.Message);
        //        }
        //    }

        //    return Customers;
        //}
    }
}
