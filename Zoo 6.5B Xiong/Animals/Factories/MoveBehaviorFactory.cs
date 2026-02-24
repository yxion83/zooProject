using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// Class used to represent move behaviors factory.
    /// </summary>
    public static class MoveBehaviorFactory
    {
        /// <summary>
        /// Method to create move behaviors.
        /// </summary>
        /// <param name="type">Type of move.</param>
        /// <returns>Move behavior.</returns>
        public static IMoveBehavior CreateMoveBehavior(MoveBehaviorType type)
        {
            IMoveBehavior moveBehavior = null;

            switch (type)
            {
                case MoveBehaviorType.Fly:
                    moveBehavior = new FlyBehavior();
                    break;
                case MoveBehaviorType.Pace:
                    moveBehavior = new PaceBehavior();
                    break;
                case MoveBehaviorType.Swim:
                    moveBehavior = new SwimBehavior();
                    break;
                case MoveBehaviorType.NoMove:
                    moveBehavior = new NoMoveBehavior();
                    break;
                case MoveBehaviorType.Climb:
                    moveBehavior = new ClimbBehavior();
                    break;
                case MoveBehaviorType.Hover:
                    moveBehavior = new HoverBehavior();
                    break;
                default:
                    break;
            }

            return moveBehavior;
        }
    }
}
