using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent non moving behaviors.
    /// </summary>
    [Serializable]
    public class NoMoveBehavior : IMoveBehavior
    {
        /// <summary>
        /// Move method of the animals that are non-moving.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        public void Move(Animal animal)
        {
            // Animal stands still.
        }
    }
}
