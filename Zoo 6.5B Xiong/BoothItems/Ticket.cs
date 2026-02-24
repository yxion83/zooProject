using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a Ticket.
    /// </summary>
    [Serializable]
    public class Ticket : SoldItem
    {
        /// <summary>
        /// Status of if Ticket can be redeemed.
        /// </summary>
        private bool isRedeemed;

        /// <summary>
        /// Tickets serial Number.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the Ticket class.
        /// </summary>
        /// <param name="price">Ticket Price.</param>
        /// <param name="serialNumber">Ticket's serial Number.</param>
        /// <param name="weight">Ticket weight.</param>
        public Ticket(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets a value indicating whether this Ticket is redeemable.
        /// </summary>
        public bool IsRedeemed
        {
            get
            {
                return this.isRedeemed;
            }
        }

        /// <summary>
        /// Gets a value of the Ticket's serial number.
        /// </summary>
        public int SerialNumber
        {
            get { return this.serialNumber; }
        }

        /// <summary>
        /// Method to redeem Ticket.
        /// </summary>
        public void Redeem()
        {
            this.isRedeemed = true;
        }
    }
}
