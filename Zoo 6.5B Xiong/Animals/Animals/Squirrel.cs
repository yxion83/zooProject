using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a squirrel.
    /// </summary>
    [Serializable]
    public class Squirrel : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the squirrel class.
        /// </summary>
        /// <param name="name">Squirrel's name.</param>
        /// <param name="age">Squirrel's age.</param>
        /// <param name="weight">Squirrel's weight.</param>
        /// <param name="gender">Squirrel's gender.</param>
        public Squirrel(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17.0;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Climb);
        }
    }
}
