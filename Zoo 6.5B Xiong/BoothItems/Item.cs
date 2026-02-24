using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a item.
    /// </summary>
    [Serializable]
    public abstract class Item
    {
        /// <summary>
        /// The weight of the item.
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Bird class.
        /// </summary>
        /// <param name="price">Item's price.</param>
        /// <param name="weight">Item's weight.</param>
        public Item(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Gets a value of this items weight.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
