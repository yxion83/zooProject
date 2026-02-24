using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a chimpanzee.
    /// </summary>
    [Serializable]
    public class Chimpanzee : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the chimpanzee class.
        /// </summary>
        /// <param name="name">Chimpanzee's name.</param>
        /// <param name="age">Chimpanzee's age.</param>
        /// <param name="weight">Chimpanzee's weight.</param>
        /// <param name="gender">Chimpanzee's gender.</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
        }
    }
}
