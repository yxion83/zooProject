using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    [Serializable]
    public class Wallet : IMoneyCollector
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private WalletColor color;

        /// <summary>
        /// Guest's money pocket.
        /// </summary>
        private IMoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(WalletColor color)
        {
            this.moneyPocket = new MoneyPocket();
            this.color = color;

            this.moneyPocket.OnBalanceChange = () =>
            {
                this.OnBalanceChange?.Invoke();
            };
        }

        /// <summary>
        /// Gets a value of the wallet's money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyPocket.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets or sets a guest's wallet color.
        /// </summary>
        public WalletColor Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the balance change in the wallet.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// The wallets method to add money.
        /// </summary>
        /// <param name="amount">Amount of money added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);
        }

        /// <summary>
        /// The wallets method to remove money.
        /// </summary>
        /// <param name="amount">Amount of money being removed.</param>
        /// <returns>Amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved = this.moneyPocket.RemoveMoney(amount);
            return amountRemoved;
        }
    }
}