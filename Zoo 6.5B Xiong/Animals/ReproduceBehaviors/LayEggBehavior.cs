using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent laying egg behavior.
    /// </summary>
    [Serializable]
    public class LayEggBehavior : IReproduceBehavior
    {
        /// <summary>
        /// Method to reproduce.
        /// </summary>
        /// <param name="mother">The mother.</param>
        /// <param name="baby">The baby.</param>
        /// <returns>A baby.</returns>
        public IReproducer Reproduce(Animal mother, Animal baby)
        {
            IHatchable egg = null;

            if (baby is IHatchable)
            {
                egg = baby as IHatchable;
                this.LayEgg(mother, baby);
                this.HatchEgg(egg);
            }

            return egg as IReproducer;
        }

        /// <summary>
        /// Method to hatch eggs.
        /// </summary>
        /// <param name="egg">Eggs that .</param>
        private void HatchEgg(IHatchable egg)
        {
            egg.Hatch();
        }

        /// <summary>
        /// Method to lay egg.
        /// </summary>
        /// <param name="mother">The mother.</param>
        /// <param name="baby">The baby.</param>
        private void LayEgg(Animal mother, Animal baby)
        {
            mother.Weight -= (baby.Weight * 1.25);
        }
    }
}
