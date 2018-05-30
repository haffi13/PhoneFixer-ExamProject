namespace Models
{
    public class ItemValidity
    {
        private bool barcodeIsValid = false;
        private bool nameIsValid = false;
        private bool priceIsValid = false;
        private bool categoryIsValid = false;
        private bool modelIsValid = false;
        private bool numberAvailableIsValid = false;

        public bool ItemIsValid()
        {
            if(BarcodeIsValid &&
               NameIsValid &&
               PriceIsValid &&
               CategoryIsValid &&
               ModelIsValid &&
               NumberAvailableIsValid
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BarcodeIsValid
        {
            get { return barcodeIsValid; }
            set { barcodeIsValid = value; }
        }

        public bool NameIsValid
        {
            get { return nameIsValid; }
            set { nameIsValid = value; }
        }

        public bool PriceIsValid
        {
            get { return priceIsValid; }
            set { priceIsValid = value; }
        }

        public bool CategoryIsValid
        {
            get { return categoryIsValid; }
            set { categoryIsValid = value; }
        }

        public bool ModelIsValid
        {
            get { return modelIsValid; }
            set { modelIsValid = value; }
        }

        public bool NumberAvailableIsValid
        {
            get { return numberAvailableIsValid; }
            set { numberAvailableIsValid = value; }
        }
    }
}
