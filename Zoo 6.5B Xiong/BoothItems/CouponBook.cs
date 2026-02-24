using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a CouponBook.
    /// </summary>
    [Serializable]
    public class CouponBook : Item
    {
        /// <summary>
        /// The date the couponbooks were made.
        /// </summary>
        private DateTime dateMade;

        /// <summary>
        /// The date the couponbook expire.
        /// </summary>
        private DateTime dateExpired;

        /// <summary>
        /// Initializes a new instance of the CouponBook class.
        /// </summary>
        /// <param name="dateMade">Date couponbook was made.</param>
        /// <param name="dateExpired">Date couponbook is expired.</param>
        /// <param name="weight">Weight of couponbook.</param>
        public CouponBook(DateTime dateMade, DateTime dateExpired, double weight)
            : base(weight)
        {
            this.dateMade = dateMade;
            this.dateExpired = dateExpired;
        }

        /// <summary>
        /// Gets a value of date couponbook was made.
        /// </summary>
        public DateTime DateMade
        {
            get
            {
                return this.dateMade;
            }
        }

        /// <summary>
        /// Gets a value of date couponbook is expired.
        /// </summary>
        public DateTime DateExpired
        {
            get
            {
                return this.dateExpired;
            }
        }
    }
}
