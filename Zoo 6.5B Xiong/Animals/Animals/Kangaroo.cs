using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a kangaroo.
    /// </summary>
    [Serializable]
    public class Kangaroo : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the kangaroo class.
        /// </summary>
        /// <param name="name">Kangaroo's name.</param>
        /// <param name="age">Kangaroo's age.</param>
        /// <param name="weight">Kangaroo's weight.</param>
        /// <param name="gender">Kangaroo's gender.</param>
        public Kangaroo(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 13.0;
        }
    }
}
