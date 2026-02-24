using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent consuming behavior.
    /// </summary>
    [Serializable]
    public class ConsumeBehavior : IEatBehavior
    {
        /// <summary>
        /// Method to eat.
        /// </summary>
        /// <param name="eater">Object eating.</param>
        /// <param name="food">Food being eaten.</param>
        public void Eat(IEater eater, Food food)
        {
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
        }
    }
}
