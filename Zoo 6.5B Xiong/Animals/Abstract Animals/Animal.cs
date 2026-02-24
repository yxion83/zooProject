using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;
using Timer = System.Timers.Timer;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// Randomize animals position.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// A list of the zoo's animal's children.
        /// </summary>
        private List<Animal> children;

        /// <summary>
        /// The time the animal moves.
        /// </summary>
        [NonSerialized]
        private Timer moveTimer;

        /// <summary>
        /// Time for each hunger status.
        /// </summary>
        [NonSerialized]
        private Timer hungerTimer;

        /// <summary>
        /// Text change of animals.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onTextChange;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;
            this.XPositionMax = 800;
            this.XPosition = random.Next(1, this.XPositionMax + 1);
            this.YPositionMax = 400;
            this.YPosition = random.Next(1, this.YPositionMax + 1);
            this.XDirection = (random.Next(0, 2) == 0) ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = (random.Next(0, 2) == 0) ? VerticalDirection.Up : VerticalDirection.Down;
            this.MoveDistance = random.Next(5, 16);
            this.children = new List<Animal>();
            this.HungerState = HungerState.Satisfied;
            this.CreateTimers();
        }

        /// <summary>
        /// Gets or sets a value of animals distance moved.
        /// </summary>
        public int MoveDistance { get; set; }

        /// <summary>
        /// Gets or sets a value of the animals horizontal position.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets a value of animals max horizontal position allowed.
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets a value of the animals vertical position.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets a value of the animals max vertical position allowed.
        /// </summary>
        public int YPositionMax { get; set; }

        /// <summary>
        /// Gets or sets a the reproducing behavior.
        /// </summary>
        public IReproduceBehavior ReproduceBehavior { get; set; }

        /// <summary>
        /// Gets or sets a value of the move behavior of the animal.
        /// </summary>
        public IMoveBehavior MoveBehavior { get; set; }

        /// <summary>
        /// Gets or sets the eating behavior of the animal.
        /// </summary>
        public IEatBehavior EatBehavior { get; set; }

        /// <summary>
        /// Gets or sets a value of the animals Vertical direction of top or bottom.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets a value of animals horizontal direction of left or right.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets a value of the animals hunger state.
        /// </summary>
        public HungerState HungerState { get; set; }

        /// <summary>
        /// Gets or sets action animal will take while hungry.
        /// </summary>
        public Action OnHunger { get; set; }

        /// <summary>
        /// Gets or sets an image update of the animal.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value to alert zoo animal is pregnant.
        /// </summary>
        public Action<IReproducer> OnPregnant { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the active status is true.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.moveTimer.Enabled;
            }

            set
            {
                this.moveTimer.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the text change of Animals.
        /// </summary>
        public Action<Animal> OnTextChange
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
        /// Gets a value of the list of animal children in the zoo.
        /// </summary>
        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
         }

        /// <summary>
        /// Gets or sets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }

            set
            {
                this.isPregnant = value;
                this.OnTextChange?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
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
                    throw new FormatException("Names can only include letters and spaces.");
                }
                else
                {
                    this.name = value;
                    this.OnTextChange?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value >= 0 && value <= 1000)
                {
                    this.weight = value;
                    this.OnTextChange?.Invoke(this);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(paramName: "weight", message: "Weight must be between 0 and 1000");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value of age for this animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value >= 0 && value <= 100)
                {
                    this.age = value;
                    this.OnTextChange?.Invoke(this);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(paramName: "age", message: "Age must be between 100 and 0.");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value of the animals gender.
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
        /// Gets a value of the animals display size.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                return (this.age == 0) ? 0.5 : 1.0;
            }
        }

        /// <summary>
        /// Gets a value of the animals resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                string type = this.GetType().Name;
                return type += (this.Age == 0) ? "Baby" : "Adult";
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Method to convert the animalType to a Type.
        /// </summary>
        /// <param name="animalType">Animal type.</param>
        /// <returns>A type.</returns>
        public static Type ConvertAnimalTypeToType(AnimalType animalType)
        {
            Type result = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    result = typeof(Chimpanzee);
                    break;

                case AnimalType.Dingo:
                    result = typeof(Dingo);
                    break;

                case AnimalType.Eagle:
                    result = typeof(Eagle);
                    break;

                case AnimalType.Hummingbird:
                    result = typeof(Hummingbird);
                    break;

                case AnimalType.Kangaroo:
                    result = typeof(Kangaroo);
                    break;

                case AnimalType.Ostrich:
                    result = typeof(Ostrich);
                    break;

                case AnimalType.Platypus:
                    result = typeof(Platypus);
                    break;

                case AnimalType.Shark:
                    result = typeof(Shark);
                    break;

                case AnimalType.Squirrel:
                    result = typeof(Squirrel);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            this.EatBehavior.Eat(this, food);
            this.hungerTimer.Stop();
            this.HungerState = HungerState.Satisfied;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.OnPregnant?.Invoke(this);
            this.isPregnant = true;
            this.MoveBehavior = new NoMoveBehavior();
        }

        /// <summary>
        /// Moves about.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);
            this.OnImageUpdate?.Invoke(this);
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer Reproduce()
        {
            // Create a baby reproducer.
            Animal baby = Activator.CreateInstance(this.GetType(), string.Empty, 0, this.Weight * (this.BabyWeightPercentage / 100), this.Gender = (random.Next(0, 2) == 0) ? Gender.Female : Gender.Male) as Animal;

            this.ReproduceBehavior.Reproduce(this, baby);
            this.children.Add(baby);

            // Make mother not pregnant after giving birth.
            this.isPregnant = false;

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            string pregnantStatus = (this.IsPregnant == true) ? " P" : string.Empty;

            return this.Name + ": " + this.GetType().Name + " (" + this.Age + ", " + this.Weight + ")" + pregnantStatus;
        }

        /// <summary>
        /// Method to add a child to the zoo.
        /// </summary>
        /// <param name="animal">Animal added to zoo.</param>
        public void AddChild(Animal animal)
        {
            if (animal != null)
            {
                this.children.Add(animal);
            }
        }

        /// <summary>
        /// Sets up the move timer for the animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
#if DEBUG
         /*   this.moveTimer.Stop();*/
#endif
            this.Move();
#if DEBUG
            this.moveTimer.Start();
#endif
        }

        /// <summary>
        /// Method to create timers.
        /// </summary>
        private void CreateTimers()
        {
            this.moveTimer = new Timer(100);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();
            this.hungerTimer = new Timer(random.Next(1, 5) * 1000);
            this.hungerTimer.Elapsed += this.HandleHungerStateChange;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Method to deserialized.
        /// </summary>
        /// <param name="context">Context.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        /// <summary>
        /// Method animal will take to handlge hunger state.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void HandleHungerStateChange(object sender, ElapsedEventArgs e)
        {
            switch (this.HungerState)
            {
                case HungerState.Satisfied:
                    this.HungerState = HungerState.Hungry;
                    break;
                case HungerState.Hungry:
                    this.HungerState = HungerState.Starving;
                    break;
                case HungerState.Starving:
                    this.HungerState = HungerState.Tired;
                    this.OnHunger?.Invoke();
                    break;
                case HungerState.Tired:
                    break;
                default:
                    throw new InvalidOperationException("Hunger state should only be Satisfied, Hungry, Starving and Tired.");
                    break;
            }
        }
    }
}