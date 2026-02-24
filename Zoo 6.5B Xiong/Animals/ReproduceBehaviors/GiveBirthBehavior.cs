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
    /// The class which is giving birth behaviors.
    /// </summary>
    [Serializable]
    public class GiveBirthBehavior : IReproduceBehavior
    {
        /// <summary>
        /// Method to reproduce.
        /// </summary>
        /// <param name="mother">The mother.</param>
        /// <param name="baby">The baby.</param>
        /// <returns>A baby.</returns>
        public IReproducer Reproduce(Animal mother, Animal baby)
        {
            mother.Weight -= baby.Weight;

            // Feed the baby.
            this.FeedNewborn(baby, mother);
            return baby;
        }

        /// <summary>
        /// Feeds a baby eater.
        /// </summary>
        /// <param name="newborn">The baby.</param>
        /// <param name="mother">The eater to feed.</param>
        private void FeedNewborn(IEater newborn, Animal mother)
        {
            // Determine milk weight.
            double milkWeight = mother.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            newborn.Eat(milk);

            // Reduce parent's weight.
            mother.Weight -= milkWeight;
        }
    }
}
