namespace Models
{
    public class ReceiptNode
    {
        private string name;
        private decimal price;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public string ReceiptLine
        {
            get { return Name + " - " + Price; }
        }
    }
}
