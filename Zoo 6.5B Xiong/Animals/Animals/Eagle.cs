using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a Eagle.
    /// </summary>
    [Serializable]
    public class Eagle : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Eagle class.
        /// </summary>
        /// <param name="name">Eagle's name.</param>
        /// <param name="age">Eagle's age.</param>
        /// <param name="weight">Eagle's weight.</param>
        /// <param name="gender">Eagle's gender.</param>
        public Eagle(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 25.0;
        }
    }
}
