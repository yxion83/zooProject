using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Accounts;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;
using Timer = System.Timers.Timer;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// Randomize guests position.
        /// </summary>
        private Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The active status of the guest.
        /// </summary>
        private bool isActive;

        /// <summary>
        /// The gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// A list of all items being carried in guest's bag.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// Guest's checking account.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// The guest's adopted animal.
        /// </summary>
        private Animal adoptedAnimal;

        /// <summary>
        /// The feed timer.
        /// </summary>
        [NonSerialized]
        private Timer feedTimer;

        /// <summary>
        /// The text change of guest.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onTextChange;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="gender">The gender of this guest.</param>
        /// <param name="checkingAccount">The guest's checking account.</param>
        public Guest(string name, int age, decimal moneyBalance, WalletColor walletColor, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.name = name;
            this.gender = gender;
            this.bag = new List<Item>();
            this.wallet = new Wallet(walletColor);
            this.wallet.AddMoney(moneyBalance);
            this.Wallet.OnBalanceChange += this.HandleBalanceChange;
            this.checkingAccount = checkingAccount;
            this.checkingAccount.OnBalanceChange += this.HandleBalanceChange;
            this.XPosition = this.random.Next(1, 200);
            this.YPosition = 400;
            this.XDirection = (this.random.Next(0, 2) == 0) ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = (this.random.Next(0, 2) == 0) ? VerticalDirection.Up : VerticalDirection.Down;
            this.CreateTimers();
        }

        /// <summary>
        /// Gets a value of this guest's Xposition.
        /// </summary>
        public int XPosition { get; private set; }

        /// <summary>
        /// Gets a value of this guest's YPosition.
        /// </summary>
        public int YPosition { get; private set; }

        /// <summary>
        /// Gets a value of the guests horizontal direciton.
        /// </summary>
        public HorizontalDirection XDirection { get; private set; }

        /// <summary>
        /// Gets a value of the guests vertical direction.
        /// </summary>
        public VerticalDirection YDirection { get; private set; }

        /// <summary>
        /// Gets a value of the guest's hunger status.
        /// </summary>
        /// </summary>
        public HungerState HungerState { get; }

        /// <summary>
        /// Gets or sets a value of a vending machine.
        /// </summary>
        public Func<VendingMachine> GetVendingMachine { get; set; }

        /// <summary>
        /// Gets or sets an image update of the guest.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active status is true or false.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive || this.AdoptedAnimal != null;
            }

            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the text change of guest.
        /// </summary>
        public Action<Guest> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }

            set
            {
                this.onTextChange = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new FormatException("Names can only incldue letters and spaces.");
                }
                else
                {
                    this.name = value;
                    this.OnTextChange?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                return 0.0;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public double WeightGainPercentage
        {
            get { return 0.0; }
        }

        /// <summary>
        /// Gets or sets a value of the guest's age.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value >= 0 && value <= 120)
                {
                    this.age = value;
                    this.OnTextChange?.Invoke(this);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(paramName: "age", message: "Age must be between 120 and 0.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value of the guests gender.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets a value of the guest's checking acount.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets a value of this guest's wallet.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets or sets a value of this guest's adopted animal.
        /// </summary>
        public Animal AdoptedAnimal
        {
            get
            {
                return this.adoptedAnimal;
            }

            set
            {
                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger = null;
                    this.adoptedAnimal = value;
                }

                if (value != null)
                {
                    this.adoptedAnimal = value;
                    this.adoptedAnimal.OnHunger += this.HandleAnimalHungry;
                }

                this.OnTextChange?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets a value of this guest's display size.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets a value of this guest's resourcekey.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Method that allows guest to withdraw money.
        /// </summary>
        /// <param name="amount">Amount withdrawn.</param>
        public void WithdrawMoney(decimal amount)
        {
            decimal amountRemoved = this.CheckingAccount.RemoveMoney(amount);
            this.Wallet.AddMoney(amountRemoved);
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        public void FeedAnimal(IEater eater)
        {
            VendingMachine animalSnackMachine = this.GetVendingMachine();

            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            // Checks if guest needs to withdraw more money.
            if (this.Wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price * 5);
            }

            // Get money from wallet.
            decimal payment = this.Wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// Guest visits the ticket booth.
        /// </summary>
        /// <param name="ticketBooth">The ticketbooth the guest go to.</param>
        /// <returns>A ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            // Assigns ticket and water bottle price.
            decimal ticketPrice = ticketBooth.TicketPrice;
            decimal waterBottlePrice = ticketBooth.WaterBottlePrice;

            if (this.Wallet.MoneyBalance < (ticketBooth.TicketPrice + ticketBooth.WaterBottlePrice))
            {
                this.WithdrawMoney(ticketPrice * 2);
            }

            // Guest buys ticket and pays.
            decimal payment = this.Wallet.RemoveMoney(ticketPrice);
            Ticket ticket = ticketBooth.SellTicket(payment);

            // Guest buys waterbottles.
            decimal payForWater = this.Wallet.RemoveMoney(waterBottlePrice);
            WaterBottle waterBottle = ticketBooth.SellWaterBottle(payForWater);
            this.bag.Add(waterBottle);

            return ticket;
        }

        /// <summary>
        /// Guest visits the information booth.
        /// </summary>
        /// <param name="informationBooth">Booth that gives information to zoo.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            // Guest get free maps and couponBooks from the zoo and add them to their bag.
            Map map = informationBooth.GiveFreeMap();
            this.bag.Add(map);
            CouponBook couponBook = informationBooth.GiveFreeCouponBook();
            this.bag.Add(couponBook);
        }

        /// <summary>
        /// Method to handle animal hunger.
        /// </summary>
        public void HandleAnimalHungry()
        {
            this.feedTimer.Start();
        }

        /// <summary>
        /// Method to handle guest being ready to feed animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        public void HandleReadyToFeed(object sender, ElapsedEventArgs e)
        {
            this.FeedAnimal(this.AdoptedAnimal);
            this.feedTimer.Stop();
        }

        /// <summary>
        /// Generates a string representation of the guest.
        /// </summary>
        /// <returns>A string representation of the guest.</returns>
        public override string ToString()
        {
            if (this.AdoptedAnimal != null)
            {
                return this.Name + ": " + this.Age + " [" + string.Format("${0:0.00}", this.Wallet.MoneyBalance) + " /" + string.Format("${0:0.00}", this.CheckingAccount.MoneyBalance) + "]," + this.AdoptedAnimal.Name;
            }
            else
            {
                return this.Name + ": " + this.Age + " [" + string.Format("${0:0.00}", this.Wallet.MoneyBalance) + " /" + string.Format("${0:0.00}", this.CheckingAccount.MoneyBalance) + "]";
            }
        }

        /// <summary>
        /// Timer for guest.
        /// </summary>
        private void CreateTimers()
        {
            this.feedTimer = new Timer((5) * 1000);
            this.feedTimer.Elapsed += this.HandleReadyToFeed;
        }

        /// <summary>
        /// Method to deserialize.
        /// </summary>
        /// <param name="context">The Context.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        /// <summary>
        /// Method to hancle the guest's balance change.
        /// </summary>
        private void HandleBalanceChange()
        {
            this.OnTextChange?.Invoke(this);
        }
    }
}