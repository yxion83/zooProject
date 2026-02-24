using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a sold items.
    /// </summary>
    [Serializable]
    public abstract class SoldItem : Item
    {
        /// <summary>
        /// The price of the items sold.
        /// </summary>
        private decimal price;

        /// <summary>
        /// Initializes a new instance of the Solditems class.
        /// </summary>
        /// <param name="price">Price of items sold.</param>
        /// <param name="weight">Weight of items sold.</param>
        public SoldItem(decimal price, double weight)
            : base(weight)
        {
            this.price = price;
        }

        /// <summary>
        /// Gets a value of the price of the items sold.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.price;
            }
        }
    }
}
