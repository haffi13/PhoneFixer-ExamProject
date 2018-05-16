using Models;
using System;
using System.Data;
using System.Data.SqlClient;
            
namespace ViewModels
{
    public static class DatabaseWriter
    {
        private static string url      = "EALSQL1.eal.local";
        private static string database = "DB2017_B12";
        private static string username = "USER_B12";
        private static string password = "SesamLukOp_12";

        private static string connectionString = string.Format
            ("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);

        // Creates a new or updates an existing object in the Item table in the database.
        public static string UpdateItem(Item item)
        {
            // If you input a alredy existing barcode in when adding a new item
            // the old item becomes overridden. Make checks
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UpdateItem", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@Barcode", item.Barcode));
                    cmd.Parameters.Add(new SqlParameter("@Name", item.Name));
                    cmd.Parameters.Add(new SqlParameter("@Description", item.Description));
                    cmd.Parameters.Add(new SqlParameter("@Price", item.PriceNoTax));
                    cmd.Parameters.Add(new SqlParameter("@PriceWithTax", item.PriceWithTax));
                    cmd.Parameters.Add(new SqlParameter("@Category", item.Category));
                    cmd.Parameters.Add(new SqlParameter("@Model", item.Model));
                    cmd.Parameters.Add(new SqlParameter("@LastAddDay", item.LastTimeAdded));
                    cmd.Parameters.Add(new SqlParameter("@NumberAvailable", item.NumberAvailable));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.AddItemError + "\n\n" + e.Message;
                }
            }
            return ret;
        }


        public static string DeleteItem(Item item)
        {
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteItem", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@Barcode", item.Barcode));
                    
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.DeleteItemError + "\n\n" + e.Message;
                }
            }
            return ret;
        }


        public static string CreateCustomer(Customer customer)
        {
            string ret = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("CreateCustomer", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@CustomerName", customer.CustomerName));
                    cmd.Parameters.Add(new SqlParameter("@CustomerPhone", customer.CustomerPhone));
                    cmd.Parameters.Add(new SqlParameter("@Email", customer.Email));
                    cmd.Parameters.Add(new SqlParameter("@Subscribed", SqlDbType.Bit)).Value = customer.Subscribed;
                    cmd.Parameters.Add(new SqlParameter("@ItemInService", SqlDbType.Bit)).Value = customer.ItemInService;

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.AddCustomerError + "\n\n" + e.Message;
                }
            }
            return ret;
        }

        public static string EditCustomer(Customer customer)
        {
            string ret = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("EditCustomer", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@CustomerId", customer.CustomerID));
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", customer.CustomerName));
                    cmd.Parameters.Add(new SqlParameter("@CustomerPhone", customer.CustomerPhone));
                    cmd.Parameters.Add(new SqlParameter("@Email", customer.Email));
                    cmd.Parameters.Add(new SqlParameter("@Subscribed", customer.Subscribed));
                    cmd.Parameters.Add(new SqlParameter("@ItemInService", customer.ItemInService));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.EditCustomerError + "\n\n" + e.Message;
                }
            }
            return ret;
        }   

        public static string DeleteCustomer(Customer customer)
        {
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteCustomer", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@CustomerId", customer.CustomerID));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.DeleteCustomerError + "\n\n" + e.Message;
                }
            }
            return ret;
        }

        public static string CreateService(Service service)
        {
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("CreateService", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@ServiceName", service.ServiceName));
                    cmd.Parameters.Add(new SqlParameter("@ServiceDescription", service.ServiceDescription));
                    cmd.Parameters.Add(new SqlParameter("@PriceNoTax", service.PriceNoTax));
                    cmd.Parameters.Add(new SqlParameter("@PriceWithTax", service.PriceWithTax));
                    cmd.Parameters.Add(new SqlParameter("@DayCreated", service.DayCreated));
                    
                    if(service.DayServiced == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@DayServiced", DBNull.Value));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@DayServiced", service.DayServiced));
                    }

                    cmd.Parameters.Add(new SqlParameter("@Repaired", service.Repaired));
                    cmd.Parameters.Add(new SqlParameter("@CustomerId", service.CustomerId));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.AddServiceError + "\n\n" + e.Message;
                }
            }
            return ret;
        }

        public static string DeleteService(Service service)
        {
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteService", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@ServiceNumber", service.ServiceNumber));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.DeleteServiceError + "\n\n" + e.Message;
                }
            }
            return ret;
        }

        public static string EditService(Service service)
        {
            string ret = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("EditService", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@ServiceNumber", service.ServiceNumber));
                    cmd.Parameters.Add(new SqlParameter("@ServiceName", service.ServiceName));
                    cmd.Parameters.Add(new SqlParameter("@ServiceDescription", service.ServiceDescription));
                    cmd.Parameters.Add(new SqlParameter("@PriceNoTax", service.PriceNoTax));
                    cmd.Parameters.Add(new SqlParameter("@PriceWithTax", service.PriceWithTax));

                    if (service.DayServiced == null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@DayServiced", DBNull.Value));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@DayServiced", service.DayServiced));
                    }

                    cmd.Parameters.Add(new SqlParameter("@DayCreated", service.DayCreated));
                    cmd.Parameters.Add(new SqlParameter("@Repaired", service.Repaired));
                    cmd.Parameters.Add(new SqlParameter("@CustomerID", service.CustomerId));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.EditServiceError + "\n\n" + e.Message;
                }
            }
            return ret;
        }
        //Draft..stored procedures need to be made
        public static string CreateSale(Sale sale)
        {
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("CreateSale", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@TimeOfSale", sale.TimeOfSale));
                    cmd.Parameters.Add(new SqlParameter("@PriceWithTax", sale.PriceWithTax));
                    cmd.Parameters.Add(new SqlParameter("@Company", sale.TaxOnSale));
                    cmd.Parameters.Add(new SqlParameter("@CreditCard", sale.Company));
                    cmd.Parameters.Add(new SqlParameter("@DiscountPercentage", sale.DiscountPercentage));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.AddSaleError + "\n\n" + e.Message;
                }
            }
            return ret;
        }

        public static string AddToSaleItem(Item item)
        {
            Sale sale = Sale.Instance;
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("AddToSaleItem", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@SaleId", sale.SaleId));
                    cmd.Parameters.Add(new SqlParameter("@Barcode", item.Barcode));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.AddSaleError + "\n\n" + e.Message;
                }
            }
            return ret;
        }

        public static string AddToSaleService(Service service)
        {
            Sale sale = Sale.Instance;
            string ret = string.Empty;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("AddToSaleService", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@SaleId", sale.SaleId));
                    cmd.Parameters.Add(new SqlParameter("@ServiceNumber", service.ServiceNumber));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    ret = Message.AddSaleError + "\n\n" + e.Message;
                }
            }
            return ret;
        }
    }
}

