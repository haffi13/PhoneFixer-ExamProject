namespace Models
{
    public static class SaleValidity
    {
        static Sale sale;
        public static bool SaleIsValid()
        {
            sale = Sale.Instance;
            bool ret = true;

            // The rest should be done in the database with the default values.
            if(sale.Items.Count < 1 && sale.Services.Count < 1)
            {
                ret = false;
            }

            return ret;
        }
    }
}
