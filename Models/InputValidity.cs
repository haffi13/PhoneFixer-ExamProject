﻿using System;
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
        public static bool Varchar15NotNull(string input)
        {
            if (input != null)
            {
                input.Trim();
                if (input.Length < 1 || input.Length > 15)
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

        

        public static bool Varchar50NotNull(string input)
        {
            if (input != null)
            {
                input.Trim();
                if (input.Length < 1 || input.Length > 50)
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

        public static bool Varchar150Null(string input)
        {
            if (input != null)
            {
                input.Trim();
                if (input.Length > 150)
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

        public static bool DecimalNotNull(string input)
        {
            if (input != null)
            {
                input.Trim();
                return DecimalCanParse(input);
            }
            else
            {
                return false;
            }
        }

        public static bool Varchar20NotNull(string input)
        {
            if (input != null)
            {
                input.Trim();
                if (input.Length < 1 || input.Length > 20)
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

        public static bool Varchar30NotNull(string input)
        {
            if (input != null)
            {
                input.Trim();
                if (input.Length < 1 || input.Length > 30)
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

        public static bool IntNotNull(string input)
        {
            if (input != null)
            {
                input.Trim();
                return IntCanParse(input);
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
