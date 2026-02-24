using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Method for swimming behavior.
    /// </summary>
    [Serializable]
    public class SwimBehavior : IMoveBehavior
    {
        /// <summary>
        /// Method to move by swimming.
        /// </summary>
        /// <param name="animal">Anial being referred to.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
            MoveHelper.MoveVertically(animal, animal.MoveDistance / 2);
        }
    }
}
