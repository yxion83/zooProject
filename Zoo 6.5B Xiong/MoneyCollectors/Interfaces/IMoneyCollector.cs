using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The interface which is used to define the role of the moneycollector.
    /// </summary>
    public interface IMoneyCollector
    {
        /// <summary>
        /// Gets a value of money balance in IMoneyCollectors interface.
        /// </summary>
        decimal MoneyBalance { get; }

        /// <summary>
        /// Gets or sets a value of the balance change.
        /// </summary>
        Action OnBalanceChange { get; set; }

        /// <summary>
        /// Method to add money in IMoneyCollectors interface.
        /// </summary>
        /// <param name="amount">Amount of money added in IMoneyCollectors interface.</param>
        void AddMoney(decimal amount);

        /// <summary>
        /// Method to remove money in IMoneyCollectors interface.
        /// </summary>
        /// <param name="amount">Amount of money removed in IMoneyCollectors interface.</param>
        /// <returns>amount leftover.</returns>
        decimal RemoveMoney(decimal amount);
    }
}
