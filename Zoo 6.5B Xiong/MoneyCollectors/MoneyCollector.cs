using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a moneycollector.
    /// </summary>
    [Serializable]
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The moneycollector's money balance.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets a value of the money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }

            set
            {
                this.moneyBalance = value;
                this.OnBalanceChange?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets a value of the balance change in the Money Collector.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// The ability for the moneycollector to add money.
        /// </summary>
        /// <param name="amount">Amount of money being added.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// The ability for the moneycollector to remove money.
        /// </summary>
        /// <param name="amount">Amount being removed.</param>
        /// <returns>The current moneybalance.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the moneybalance.
            if (this.MoneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.MoneyBalance;
            }

            // Subtract the amount removed from the wallet's money balance.
            this.MoneyBalance -= amountRemoved;

            return amountRemoved;
        }
    }
}
