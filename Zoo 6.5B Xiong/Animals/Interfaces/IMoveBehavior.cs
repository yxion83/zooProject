using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The interface which is used to define the role moving behaviors.
    /// </summary>
    public interface IMoveBehavior
    {
        /// <summary>
        /// Method for animals to move.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        void Move(Animal animal);
    }
}
