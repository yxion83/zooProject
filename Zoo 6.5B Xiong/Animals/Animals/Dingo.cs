using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a dingo.
    /// </summary>
    [Serializable]
    public class Dingo : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Dingo class.
        /// </summary>
        /// <param name="name">The name of the Dingo.</param>
        /// <param name="age">The age of the Dingo.</param>
        /// <param name="weight">The weight of the Dingo (in pounds).</param>
        /// <param name="gender">The gender of the Dingo.</param>
        public Dingo(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
            this.EatBehavior = new BuryAndEatBoneBehavior();
        }
    }
}