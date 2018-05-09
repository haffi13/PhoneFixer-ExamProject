using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CustomerValidity
    {
        private bool nameIsValid;

        public bool CustomerIsValid()
        {
            if (NameIsValid)
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
    }
}
