using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foods
{
    /// <summary>
    /// The interface IEater.
    /// </summary>
    public interface IEater
    {
        /// <summary>
        /// Gets or sets the value of weight of the eater.
        /// </summary>
        double Weight { get; set; }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        double WeightGainPercentage { get; }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        void Eat(Food food);
    }
}
