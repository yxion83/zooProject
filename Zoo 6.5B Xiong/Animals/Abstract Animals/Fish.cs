using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a fish.
    /// </summary>
    [Serializable]
    public abstract class Fish : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Fish class.
        /// </summary>
        /// <param name="name">Name of the fish.</param>
        /// <param name="age">Age of the fish.</param>
        /// <param name="weight">Weight of the fish.</param>
        /// <param name="gender">The gender of the fish.</param>
        public Fish(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);
            this.EatBehavior = new ConsumeBehavior();
            this.ReproduceBehavior = new LayEggBehavior();
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }
    }
}
