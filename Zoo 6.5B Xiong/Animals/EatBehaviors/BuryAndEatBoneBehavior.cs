using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a burying beahvoir while eating.
    /// </summary>
    [Serializable]
    public class BuryAndEatBoneBehavior : IEatBehavior
    {
        /// <summary>
        /// Method to eat.
        /// </summary>
        /// <param name="eater">Object eating.</param>
        /// <param name="food">Food being eaten.</param>
        public void Eat(IEater eater, Food food)
        {
            this.BuryBone(food);
            this.DigUpAndEatBone();
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
            this.Bark();
        }

        /// <summary>
        /// Method to bark.
        /// </summary>
        private void Bark()
        {
        }

        /// <summary>
        /// Method to bury bones.
        /// </summary>
        /// <param name="bone">Food being buried.</param>
        private void BuryBone(Food bone)
        {
        }

        /// <summary>
        /// Method to dig up and eat bones.
        /// </summary>
        private void DigUpAndEatBone()
        {
        }
    }
}
