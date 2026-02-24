using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// Interface to reproducing behaviors.
    /// </summary>
    public interface IReproduceBehavior
    {
        /// <summary>
        /// Method to reproduce.
        /// </summary>
        /// <param name="mother">The mother.</param>
        /// <param name="baby">The baby.</param>
        /// <returns>Baby.</returns>
        IReproducer Reproduce(Animal mother, Animal baby);
    }
}
