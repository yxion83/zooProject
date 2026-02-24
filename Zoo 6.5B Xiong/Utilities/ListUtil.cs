using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    /// <summary>
    /// Class used to represent the ListUtil.
    /// </summary>
    public static class ListUtil
    {
        /// <summary>
        /// Method to turn many strings to single string.
        /// </summary>
        /// <param name="list">List being referred to.</param>
        /// <param name="separator">Separator between strings.</param>
        /// <returns>Single string value.</returns>
        public static string Flatten(this IEnumerable<string> list, string separator)
        {
            string result = null;

            list.ToList().ForEach(s => result += result == null ? s : separator + s);

            return result;
        }
    }
}
