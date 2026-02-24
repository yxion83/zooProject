using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class for flying behavior.
    /// </summary>
    [Serializable]
    public class FlyBehavior : IMoveBehavior
    {
        /// <summary>
        /// Moves by flying.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            if (animal.YDirection == VerticalDirection.Down)
            {
                if (animal.YPosition + 10 > animal.YPositionMax)
                {
                    animal.YPosition = animal.YPositionMax;
                }
                else
                {
                    animal.YPosition += 10;
                }

                animal.YDirection = VerticalDirection.Up;
            }
            else
            {
                if (animal.YPosition - 10 < 0)
                {
                    animal.YPosition = 0;
                }
                else
                {
                    animal.YPosition -= 10;
                }

                animal.YDirection = VerticalDirection.Down;
            }
        }
    }
}
