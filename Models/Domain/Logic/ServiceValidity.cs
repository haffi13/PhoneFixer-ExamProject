using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceValidity
    {
        private bool nameIsValid = false;
        private bool priceIsValid = false;

        public bool ServiceIsValid()
        {
            if(nameIsValid && priceIsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
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
    }
}
