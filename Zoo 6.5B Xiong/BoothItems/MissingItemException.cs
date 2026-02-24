using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent exceptions for items that are missing.
    /// </summary>
    public class MissingItemException : Exception
    {
        /// <summary>
        /// Constructor to show missing items exception.
        /// </summary>
        /// <param name="message">Description of the exception.</param>
        public MissingItemException(string message)
           : base(message)
        {
        }
    }
}
