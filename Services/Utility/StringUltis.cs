using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utility
{
    public static class StringUltis
    {
        public static bool AreEqualIgnoreCase(string str1, string str2)
        {
            return str1.ToLower().Equals(str2.ToLower());
        }
    }
}
