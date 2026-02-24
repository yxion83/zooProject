using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foods
{
    /// <summary>
    /// Class that represents Food.
    /// </summary>
    [Serializable]
    public class Food
    {
        /// <summary>
        /// The weight of the food (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Food"/> class.
        /// </summary>
        /// <param name="weight">The weight of the food (in pounds).</param>
        public Food(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Gets the weight of the food (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
