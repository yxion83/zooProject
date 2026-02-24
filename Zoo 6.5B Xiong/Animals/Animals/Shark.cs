using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a Shark.
    /// </summary>
    [Serializable]
    public class Shark : Fish
    {
        /// <summary>
        /// Initializes a new instance of the Shark class.
        /// </summary>
        /// <param name="name">Shark's name.</param>
        /// <param name="age">Shark's age.</param>
        /// <param name="weight">Shark's weight.</param>
        /// <param name="gender">Shark's gender.</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 18.0;
        }

        /// <summary>
        /// Gets a value of the sharks display size.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return (this.Age == 0) ? 1 : 1.5;
            }
        }
    }
}
