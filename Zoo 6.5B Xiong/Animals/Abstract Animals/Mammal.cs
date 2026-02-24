using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a mammal.
    /// </summary>
    [Serializable]
    public abstract class Mammal : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Mammal class.
        /// </summary>
        /// <param name="name">The name of the mammal.</param>
        /// <param name="age">The age of the mammal.</param>
        /// <param name="weight">The weight of the mammal (in pounds).</param>
        /// <param name="gender">The gender of the mammal.</param>
        public Mammal(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Pace);
            this.EatBehavior = new ConsumeBehavior();
            this.ReproduceBehavior = new GiveBirthBehavior();
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public override double WeightGainPercentage
        {
            get
            {
                return 15.0;
            }
        }
    }
}