using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DatabaseWriter
    {
        private static string url = "EALSQL1.eal.local";
        private static string database = "DB2017_B12";
        private static string username = "USER_B12";
        private static string password = "SesamLukOp_12";

        private static string connectionString = string.Format
            ("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);

        public void AddItemToInventory(Item item)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("AddItem", connection)
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
    }
}
