using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class InputValidity
    {
        // Might want more generic names, like varchar20 check
        // Might fit better if we want to reuse these validity checks.
        public static bool Barcode(string barcode)
        {
            bool ret = true;

            if(barcode != null)
            {
                if (barcode.Length < 1 || barcode.Length > 15)
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        public static bool Name(string name)
        {
            bool ret = true;

            if(name != null)
            {
                if (name.Length < 1 || name.Length > 50)
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        public static bool Description(string description)
        {
            bool ret = true;

            if(description != null)
            {
                if (description.Length > 150)
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        public static bool Price(string price)
        {
            bool ret = true;
            if(price != null)
            {
                ret = IntCanParse(price);
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        public static bool Category(string category)
        {
            bool ret = true;

            if(category != null)
            {
                if(category.Length < 1 || category.Length > 20)
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        public static bool Model(string model)
        {
            bool ret = true;

            if(model != null)
            {
                if(model.Length < 1 || model.Length > 30)
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        public static bool NumberAvailable(string numberAvailable)
        {
            bool ret = true;

            if(numberAvailable != null)
            {
                ret = DecimalCanParse(numberAvailable);
            }
            else
            {
                ret = false;
            }

            return ret;
        }

        private static bool DecimalCanParse(string input)
        {
            bool ret = decimal.TryParse(input, out decimal result);

            return ret;
        }

        private static bool IntCanParse(string input)
        {
            bool ret = int.TryParse(input, out int result);

            return ret;
        }
    }
}
