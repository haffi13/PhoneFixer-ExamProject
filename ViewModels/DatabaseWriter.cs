using Models;
using System.Data;
using System.Data.SqlClient;

namespace ViewModels
{
    public class DatabaseWriter
    {
        private static string url      = "EALSQL1.eal.local";
        private static string database = "DB2017_B12";
        private static string username = "USER_B12";
        private static string password = "SesamLukOp_12";

        private static string connectionString = string.Format
            ("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);

        // Creates a new or updates an existing object in the Item table in the database.
        public void UpdateItem(Item item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ItemUpdate", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@Barcode", item.Barcode));
                    cmd.Parameters.Add(new SqlParameter("@Name", item.Name));
                    cmd.Parameters.Add(new SqlParameter("@Description", item.Description));
                    cmd.Parameters.Add(new SqlParameter("@Price", item.Price));
                    cmd.Parameters.Add(new SqlParameter("@Category", item.Category));
                    cmd.Parameters.Add(new SqlParameter("@Model", item.Model));
                    cmd.Parameters.Add(new SqlParameter("@NumberAvailable", item.NumberAvailable));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    //MessageBox.Show(e.Message.ToString()); //should not do this                 fixit!!!!!!!!!!!
                }
            }

        }

        public void DeleteItem(Item item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteItem", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Is it not enough to only pass the Barcode to the database?
                    // Might be redundant to ask for all the other stuffs.
                    cmd.Parameters.Add(new SqlParameter("@Barcode", item.Barcode));
                    // ------------------------------------------------------------
                    cmd.Parameters.Add(new SqlParameter("@Name", item.Name));
                    cmd.Parameters.Add(new SqlParameter("@Description", item.Description));
                    cmd.Parameters.Add(new SqlParameter("@Price", item.Price));
                    cmd.Parameters.Add(new SqlParameter("@Category", item.Category));
                    cmd.Parameters.Add(new SqlParameter("@Model", item.Model));
                    cmd.Parameters.Add(new SqlParameter("@NumberAvailable", item.NumberAvailable));

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException e)
                {
                    //MessageBox.Show(e.Message.ToString()); //should not do this                 fixit!!!!!!!!!!!
                }
            }
        }
    }
}
