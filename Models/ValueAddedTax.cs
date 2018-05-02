using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ValueAddedTax
    {
        public static decimal AddVAT(decimal input)
        {
            return input * 1.25m;
        }

        public static decimal RemoveVAT(decimal input)
        {
            return input * 0.80M;
        }
    }
}
