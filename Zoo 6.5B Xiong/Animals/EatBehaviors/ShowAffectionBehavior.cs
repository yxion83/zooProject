using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a affectionate behavior while eating.
    /// </summary>
    [Serializable]
    public class ShowAffectionBehavior : IEatBehavior
    {
        /// <summary>
        /// Method for animal to eat.
        /// </summary>
        /// <param name="eater">Object eating.</param>
        /// <param name="food">Food being eaten.</param>
        public void Eat(IEater eater, Food food)
        {
            this.ShowAffection();
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
        }

        /// <summary>
        /// Method for animal to show affection.
        /// </summary>
        private void ShowAffection()
        {
        }
    }
}
