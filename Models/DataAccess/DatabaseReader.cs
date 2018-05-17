using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace Models
{
    public static class DatabaseReader
    {
        private static string url = "EALSQL1.eal.local";
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
                                PriceNoTax = (decimal)reader["Price"],
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

        public static Dictionary<Customer, string> GetCustomer(int customerId)
        {
            Dictionary<Customer, string> ret = new Dictionary<Customer, string>();
            Customer customer = new Customer();
            string errorMessage = string.Empty;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetCustomer", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customer.CustomerID = (int)reader["CustomerId"];
                            customer.CustomerName = (string)reader["CustomerName"];
                            customer.CustomerPhone = (string)reader["CustomerPhone"];
                            customer.Email = (string)reader["Email"];
                            customer.Subscribed = (bool)reader["Subscribed"];
                            customer.ItemInService = (bool)reader["ItemInService"];
                        }
                    }
                }
                catch (SqlException e)
                {
                    errorMessage = "\n\n" + e.Message;
                }
            }
            ret.Add(customer, errorMessage);
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

        public static Dictionary<List<Service>, string> GetServices()
        {
            Dictionary<List<Service>, string> ret = new Dictionary<List<Service>, string>();
            List<Service> Services = new List<Service>();
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
                                DayCreated = DateTime.Parse(reader["DayCreated"].ToString())
                            };
                            if (reader["DayServiced"] == DBNull.Value)
                            {
                                temp.DayServiced = null;
                            }
                            else
                            {
                                temp.DayServiced = (DateTime?)reader["DayServiced"];
                            }

                            temp.Repaired = (bool)reader["Repaired"];
                            temp.CustomerId = (int)reader["CustomerID"];

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

        public static Dictionary<int, string> GetSaleId()
        {
            Sale sale = Sale.Instance;
            Dictionary<int, string> ret = new Dictionary<int, string>();
            string errorMessage = string.Empty;
            int saleId = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetSaleIdWithTime", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    con.Open();

                    cmd.Parameters.Add("@TimeOfSale", SqlDbType.DateTime).Value = sale.TimeOfSale;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            saleId = (int)reader["SaleId"];
                        }
                    }
                }
                catch (SqlException e)
                {
                    errorMessage = "\n\n" + e.Message;
                }
            }
            ret.Add(saleId, errorMessage);
            return ret;
        }
    }
}
