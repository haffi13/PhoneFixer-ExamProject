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
            if (barcode != null)
            {
                barcode.Trim();
                if (barcode.Length < 1 || barcode.Length > 15)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool Name(string name)
        {
            if (name != null)
            {
                name.Trim();
                if (name.Length < 1 || name.Length > 50)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        // Test this, string.empty or null...for all of them...
        public static bool Description(string description)
        {
            if (description != null)
            {
                description.Trim();
                if (description.Length > 150)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool Price(string price)
        {
            if (price != null)
            {
                price.Trim();
                return DecimalCanParse(price);
            }
            else
            {
                return false;
            }
        }

        public static bool Category(string category)
        {
            if (category != null)
            {
                category.Trim();
                if (category.Length < 1 || category.Length > 20)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool Model(string model)
        {
            if (model != null)
            {
                model.Trim();
                if (model.Length < 1 || model.Length > 30)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool NumberAvailable(string numberAvailable)
        {
            if (numberAvailable != null)
            {
                numberAvailable.Trim();
                return IntCanParse(numberAvailable);
            }
            else
            {
                return false;
            }
        }

        private static bool DecimalCanParse(string input)
        {
            return decimal.TryParse(input, out decimal result);
        }

        private static bool IntCanParse(string input)
        {
            return int.TryParse(input, out int result);
        }
    }
}
