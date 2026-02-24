using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a hummingbird.
    /// </summary>
    [Serializable]
    public class Hummingbird : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Hummingbird class.
        /// </summary>
        /// <param name="name">The name of the hummingbird.</param>
        /// <param name="age">The age of the hummingbird.</param>
        /// <param name="weight">The weight of the hummingbird (in pounds).</param>
        /// <param name="gender">The gender of the hummingbird.</param>
        public Hummingbird(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17.5;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Hover);
        }

        /// <summary>
        /// Gets a value of the hummingbird display size.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return (this.Age == 0) ? 0.4 : 0.6;
            }
        }
    }
}