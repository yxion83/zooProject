using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;

namespace Accounts
{
    /// <summary>
    /// The class which is used to represent an Account.
    /// </summary>
    [Serializable]
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// The money balance of the account.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets or sets a value of this accounts moneybalance.
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
        /// Gets or sets a value of the balance change in the Account.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Method for accounts to add money.
        /// </summary>
        /// <param name="amount">Amount of money added.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// Method for accounts to remove money.
        /// </summary>
        /// <param name="amount">Amount of money removed.</param>
        /// <returns>Amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            this.MoneyBalance -= amount;
            return amount;
        }
    }
}
