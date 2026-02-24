using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;

namespace Zoos
{
    /// <summary>
    /// Represents the class to sort resutls.
    /// </summary>
    [Serializable]
    public class SortResult
    {
        /// <summary>
        /// Gets or sets a list of the animals.
        /// </summary>
        public List<object> Objects { get; set; }

        /// <summary>
        /// Gets or sets a value of the comparison count.
        /// </summary>
        public int CompareCount { get; set; }

        /// <summary>
        /// Gets or sets a value of the elasped milli seconds.
        /// </summary>
        public double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the swap count.
        /// </summary>
        public int SwapCount { get; set; }
    }
}
