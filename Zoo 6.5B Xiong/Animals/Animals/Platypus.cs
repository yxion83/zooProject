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
    /// The class which is used to represent a platypus.
    /// </summary>
    [Serializable]
    public sealed class Platypus : Mammal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Platypus class.
        /// </summary>
        /// <param name="name">The name of the Platypus.</param>
        /// <param name="age">The age of the Platypus.</param>
        /// <param name="weight">The weight of the Platypus (in pounds).</param>
        /// <param name="gender">The gender of the Platypus.</param>
        public Platypus(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 12.0;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);
            this.EatBehavior = new ShowAffectionBehavior();
            this.ReproduceBehavior = new LayEggBehavior();
        }

        /// <summary>
        /// Gets a value of the platypus display size.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return (this.Age == 0) ? 0.5 : 1.1;
            }
        }

        /// <summary>
        /// Hatches the animal.
        /// </summary>
        public void Hatch()
        {
            // The animal hatches from an egg.
        }
    }
}