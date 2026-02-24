using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a moneybox.
    /// </summary>
    [Serializable]
    public class MoneyBox : MoneyCollector
    {
        /// <summary>
        /// Method to remove money in the moneybox.
        /// </summary>
        /// <param name="amount">Amount of money removed.</param>
        /// <returns>The amount removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Lock();
            decimal amountRemoved = base.RemoveMoney(amount);
            this.Unlock();
            return amountRemoved;
        }

        /// <summary>
        /// Method to lock moneybox.
        /// </summary>
        private void Lock()
        {
        }

        /// <summary>
        /// Method to unlock moneybox.
        /// </summary>
        private void Unlock()
        {
        }
    }
}
