using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoothItems;

namespace People
{
    /// <summary>
    /// The class which is used to represent a giving booth.
    /// </summary>
    [Serializable]
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Initializes a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">The attedant of the giving booth.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new CouponBook(DateTime.Now, (DateTime.Now).AddYears(1), 0.8));
            }

            for (int i = 0; i < 10; i++)
            {
                this.Items.Add(new Map(0.5, DateTime.Now));
            }
        }

        /// <summary>
        /// Method for the booth give out free coupon books.
        /// </summary>
        /// <returns>Coupon Books.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            try
            {
                Item couponBook = (CouponBook)this.Attendant.FindItem(this.Items, typeof(CouponBook));
                return couponBook as CouponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Coupon book was not found.", ex);
            }
        }

        /// <summary>
        /// Method for the booth to give out free maps.
        /// </summary>
        /// <returns>Map.</returns>
        public Map GiveFreeMap()
        {
            try
            {
                Item map = this.Attendant.FindItem(this.Items, typeof(Map));
                return map as Map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }
        }
    }
}
