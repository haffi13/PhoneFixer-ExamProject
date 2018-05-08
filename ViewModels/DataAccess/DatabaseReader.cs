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
        public static Dictionary<List<Item>, string> GetInventory()
        {
            Dictionary<List<Item>, string> ret = new Dictionary<List<Item>, string>();
            List<Item> Items = new List<Item>();
            string errorMessage = string.Empty;

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
                                Barcode = (string)reader["Barcode"],
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"],
                                Price = (decimal)reader["Price"],
                                PriceWithTax = (decimal)reader["PriceWithTax"],
                                Category = reader["Category"].ToString(),
                                Model = (string)reader["Model"],
                                LastTimeAdded = (DateTime)reader["LastAddDay"],
                                NumberAvailable = (int)reader["NumberAvailable"]
                            };

                            Items.Add(temp);
                        }
                    }
                }
                catch (SqlException e)
                {
                    errorMessage = "\n\n" + e.Message;
                }
            }
            ret.Add(Items, errorMessage);
            return ret;
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
                                CustomerPhone = (string)reader["CustomerPhone"],
                                Email = (string)reader["Email"],
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

        public static Dictionary<List<Service>, string> GetService()
        {
            Dictionary<List<Service>, string> ret = new Dictionary<List<Service>, string>();
            List<Service> Services= new List<Service>();
            string errorMessage = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllServices", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Service temp = new Service
                            {
                                ServiceNumber = (int)reader["ServiceNumber"],
                                ServiceName = (string)reader["ServiceName"],
                                ServiceDescription = (string)reader["ServiceDescription"],
                                PriceNoTax = (decimal)reader["PriceNoTax"],
                                PriceWithTax = (decimal)reader["PriceWithTax"],
                                DayServiced = (DateTime)reader["DayServiced"],
                                DayUpdated = (DateTime)reader["DayUpdated"],
                                Repaired = (bool)reader["Repaired"],
                                CustomerId = (int)reader["CustomerID"]
                            };

                            Services.Add(temp);
                        }
                    }
                }

                catch (SqlException e)
                {
                    errorMessage = "\n\n" + e.Message;
                }
            }
            ret.Add(Services, errorMessage);
            return ret;
        }
    }
}
