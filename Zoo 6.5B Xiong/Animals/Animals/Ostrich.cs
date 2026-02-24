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
    /// The class which is used to represent a ostrich.
    /// </summary>
    [Serializable]
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// Initializes a new instance of the ostrich class.
        /// </summary>
        /// <param name="name">Ostrich's name.</param>
        /// <param name="age">Ostrich's age.</param>
        /// <param name="weight">Ostrich's weight.</param>
        /// <param name="gender">Ostrich's gender.</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 30.0;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Pace);
        }

        /// <summary>
        /// Gets a value of the Ostrich display size.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return (this.Age == 0) ? 0.4 : 0.8;
            }
        }
    }
}
