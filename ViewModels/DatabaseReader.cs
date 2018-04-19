using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-----------------------------------
// We are not sure where to put the Data Access in the layers.
// Should it be in the ViewModels or should it have its own layer?
//-----------------------------------

namespace ViewModels
{
    public class DatabaseReader
    {
        private static string url = "EALSQL1.eal.local";
        private static string database = "DB2017_B12";
        private static string username = "USER_B12";
        private static string password = "SesamLukOp_12";

        private static string connectionString = string.Format
            ("Server={0}; Database={1}; User Id={2}; Password={3};", url, database, username, password);


        // Should take in parameters of stored procedure and argument for the SP.
        public List<Item> ExecuteQuery()
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
                            float Number_Available = float.Parse(reader["Number_Available"].ToString());

                            // I don't think we need to check for nulls here.
                            // I think nothing happens if there isn't any value in field in the
                            // database.

                            temp.Barcode = Barcode;
                            temp.Name = ItemName;
                            temp.Description = Description;
                            temp.Price = Price;
                            temp.Category = Category;
                            temp.Model = Model;
                            temp.Number_Available = Number_Available;

                            Items.Add(temp);
                        }
                    }
                }

                catch (SqlException e)
                {
                    //----------------------------------
                    //      Deal with this!
                    //----------------------------------
                }
            }
            return Items;
        }
    }
}
