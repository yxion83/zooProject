using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Method for pacing behavior.
    /// </summary>
    [Serializable]
    public class PaceBehavior : IMoveBehavior
    {
        /// <summary>
        /// Method to Move by pacing.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
        }
    }
}
