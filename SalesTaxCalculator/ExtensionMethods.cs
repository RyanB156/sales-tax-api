using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCalculator
{
    public static class ExtensionMethods
    {

        /// <summary>
        /// Extension method to check if a NameValueCollection contains the specified key
        /// </summary>
        /// <param name="collection">The NameValueCollection to search</param>
        /// <param name="key">The specified key to search for</param>
        /// <returns></returns>
        public static bool Contains(this NameValueCollection collection, string key)
        {
            foreach (var k in collection.Keys)
                if (k.Equals(key))
                    return true;
            return false;
        }
    }
}
