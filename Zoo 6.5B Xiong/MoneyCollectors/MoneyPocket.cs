using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent moneypocket.
    /// </summary>
    [Serializable]
    public class MoneyPocket : MoneyCollector
    {
        /// <summary>
        /// Method for moneypocket class to remove money.
        /// </summary>
        /// <param name="amount">Amount of money removed in moneypocket.</param>
        /// <returns>Amount of money removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unfold();
            decimal moneyRemoved = base.RemoveMoney(amount);
            this.Fold();

            return moneyRemoved;
        }

        /// <summary>
        /// Method to fold in moneypocket.
        /// </summary>
        private void Fold()
        {
        }

        /// <summary>
        /// Method to unfold in moneypocket.
        /// </summary>
        private void Unfold()
        {
        }
    }
}
