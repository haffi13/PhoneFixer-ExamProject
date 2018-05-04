using Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
                    cmd.Parameters.Add(new SqlParameter("@Price", item.Price));
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
                    ret = Message.AddItemError + "\n\n" +
                                     e.Message;
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
                    ret = Message.DeleteItemError + "\n\n" +
                                e.Message;
                }
            }
            return ret;
        }

        public static string UpdateCustomer(Customer customer)
        {
            string ret = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UpdateCustomer", connection)
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
                    ret = Message.EditItemError + "\n\n" +
                                     e.Message;
                }
            }
            return ret;
        }
    }
}
