using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Animals;
using BoothItems;
using Microsoft.Win32.SafeHandles;
using MoneyCollectors;
using Reproducers;

namespace People
{
    /// <summary>
    /// The class which is used to represent a money collecting booth.
    /// </summary>
    [Serializable]
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// Ticket stack.
        /// </summary>
        private Stack<Ticket> ticketStack;

        /// <summary>
        /// The ticket price of the money collecting booth.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The water bottle price in the money collecting booth.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// The MoneyCollectingBooth's money box.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// Initializes a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">The attendant of the MoneyCollectingBooth.</param>
        /// <param name="ticketPrice">The ticket price of the money collecting booth.</param>
        /// <param name="waterBottlePrice">The price of the water bottle.</param>
        /// <param name="moneyBox">The moneybox of the moneycollecting booth.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;
            this.ticketStack = new Stack<Ticket>();

            for (int i = 0; i < 5; i++)
            {
                this.ticketStack.Push(new Ticket(this.ticketPrice, i + 206793, 0.01));
            }

            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new WaterBottle(this.waterBottlePrice, i + 743922, 1));
            }
        }

        /// <summary>
        /// Gets a value of money collecting booths money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets a value of the ticket price.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets a value of the water bottle price.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// The money collecting booth's method to add money.
        /// </summary>
        /// <param name="amount">Amount added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// The money collecting booth's methods to remove money.
        /// </summary>
        /// <param name="amount">Amount removed.</param>
        /// <returns>The amount of money removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved = this.moneyBox.RemoveMoney(amount);
            return amountRemoved;
        }

        /// <summary>
        /// Method to sell tickets at the booth.
        /// </summary>
        /// <param name="payment">Payment for the ticket.</param>
        /// <returns>Ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Item ticket = null;
            try
            {
                if (payment >= this.ticketPrice)
                {
                    ticket = this.ticketStack.Pop();
                    if (ticket != null)
                    {
                        this.moneyBox.AddMoney(payment);
                    }
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }

            return ticket as Ticket;
        }

        /// <summary>
        /// Method to sell water bottles at the booth.
        /// </summary>
        /// <param name="payment">Payment for the waterbottle.</param>
        /// <returns>WaterBottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            Item waterBottle = null;

            if (payment >= this.waterBottlePrice)
            {
                try
                {
                    waterBottle = this.Attendant.FindItem(this.Items, typeof(WaterBottle));
                    if (waterBottle != null)
                    {
                        this.moneyBox.RemoveMoney(payment);
                    }
                }
                catch (MissingItemException ex)
                {
                    throw new NullReferenceException("Water Bottle not found.", ex);
                }
            }

            return waterBottle as WaterBottle;
        }
    }
}
