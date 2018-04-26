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
                            Item temp = new Item();

                            string Barcode = reader["Barcode"].ToString();
                            string ItemName = reader["Name"].ToString();
                            string Description = reader["Description"].ToString();
                            decimal Price = Convert.ToDecimal(reader["Price"].ToString());
                            string Category = reader["Category"].ToString();
                            string Model = reader["Model"].ToString();
                            int NumberAvailable = Convert.ToInt32(reader["NumberAvailable"].ToString());

                            temp.Barcode = Barcode;
                            temp.Name = ItemName;
                            temp.Description = Description;
                            temp.Price = Price;
                            temp.Category = Category;
                            temp.Model = Model;
                            temp.NumberAvailable = NumberAvailable;

                            Items.Add(temp);
                        }
                    }
                }

                catch (SqlException e)
                {
                    //----------------------------------
                    //      Deal with this!
                    //----------------------------------
                    MessageBox.Show(e.Message);
                }
            }
            return Items;
        }
    }
}
