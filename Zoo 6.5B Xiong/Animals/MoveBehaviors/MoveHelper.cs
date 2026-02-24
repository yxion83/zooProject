using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent help for moving.
    /// </summary>
    public static class MoveHelper
    {
        /// <summary>
        /// Method to move horizontally.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        /// <param name="moveDistance">The move distance.</param>
        public static void MoveHorizontally(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case CagedItems.HungerState.Satisfied:
                    break;
                case CagedItems.HungerState.Hungry:
                    moveDistance /= 4;
                    break;
                case CagedItems.HungerState.Starving:
                    moveDistance = 0;
                    break;
                case CagedItems.HungerState.Tired:
                    moveDistance = 0;
                    break;
                default:
                    throw new InvalidOperationException("Invalid Operation");
                    break;
            }

            // If animal animal is facing right...
            if (animal.XDirection == HorizontalDirection.Right)
            {
                // If animal animals position and distance moved is greater then the max horizontal position allowed....
                if (animal.XPosition + moveDistance > animal.XPositionMax)
                {
                    animal.XPosition = animal.XPositionMax; // animal animals coordinates will be set to the max horizontal possion allowed.
                    animal.XDirection = HorizontalDirection.Left; // animal animal will then face left.
                }
                else
                {
                    // Else animal animals horizontal positon will to be set to the move distance added.
                    animal.XPosition += moveDistance;
                }
            }
            else
            {
                // If animal animals horizontal coordinates minus the distance moved is less than 0...
                if (animal.XPosition - moveDistance < 0)
                {
                    animal.XPosition = 0; // The animals horizontal coordinates will be set to 0.
                    animal.XDirection = HorizontalDirection.Right; // animal animal will face right.
                }
                else
                {
                    // Else animal animals horizontal positon will to be set to the move distance sbtracted.
                    animal.XPosition -= moveDistance;
                }
            }
        }

        /// <summary>
        /// Metho to move vertically.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        /// <param name="moveDistance">Distance moved.</param>
        public static void MoveVertically(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case CagedItems.HungerState.Satisfied:
                    break;
                case CagedItems.HungerState.Hungry:
                    moveDistance /= 4;
                    break;
                case CagedItems.HungerState.Starving:
                    moveDistance = 0;
                    break;
                case CagedItems.HungerState.Tired:
                    moveDistance = 0;
                    break;
                default:
                    throw new InvalidOperationException("Invalid Operation");
                    break;
            }

            if (animal.YDirection == VerticalDirection.Down)
            {
                if (animal.YPosition + moveDistance > animal.YPositionMax)
                {
                    animal.YPosition = animal.YPositionMax;
                    animal.YDirection = VerticalDirection.Up;
                }
                else
                {
                    animal.YPosition += moveDistance;
                }
            }
            else if (animal.YDirection == VerticalDirection.Up)
            {
                if (animal.YPosition - moveDistance < 0)
                {
                    animal.YPosition = 0;
                    animal.YDirection = VerticalDirection.Down;
                }
                else
                {
                    animal.YPosition -= moveDistance;
                }
            }
        }
    }
}
