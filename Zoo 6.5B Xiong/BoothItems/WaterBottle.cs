using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a waterbottle.
    /// </summary>
    [Serializable]
    public class WaterBottle : SoldItem
    {
        /// <summary>
        /// Waterbottle's serial number.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the Waterbottle class.
        /// </summary>
        /// <param name="price">Waterbottle's price.</param>
        /// <param name="serialNumber">Waterbottle's serial number.</param>
        /// <param name="weight">Waterbottle's weight.</param>
        public WaterBottle(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets a value of this Waterbottle's serial number.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }
    }
}
