using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class used to represent hovering behavior.
    /// </summary>
    [Serializable]
    public class HoverBehavior : IMoveBehavior
    {
        /// <summary>
        /// Randomizes.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The hovering process.
        /// </summary>
        private HoverProcess process;

        /// <summary>
        /// Counts steps.
        /// </summary>
        private int stepCount;

        /// <summary>
        /// Method to move for the hover behavior.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        public void Move(Animal animal)
        {
            if (this.stepCount == 0)
            {
                this.NextProcess(animal);
            }

            this.stepCount--;

            int moveDistance;

            if (this.process == HoverProcess.Hovering)
            {
                moveDistance = animal.MoveDistance;
                animal.XDirection = (random.Next(0, 2) == 0) ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = (random.Next(0, 2) == 0) ? VerticalDirection.Up : VerticalDirection.Down;
                MoveHelper.MoveHorizontally(animal, moveDistance);
                MoveHelper.MoveVertically(animal, moveDistance);
            }
            else
            {
                moveDistance = animal.MoveDistance * 4;
                MoveHelper.MoveHorizontally(animal, moveDistance);
                MoveHelper.MoveVertically(animal, moveDistance);
            }
        }

        /// <summary>
        /// Method to  process next move decision for animal.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        private void NextProcess(Animal animal)
        {
            if (this.process == HoverProcess.Hovering)
            {
                this.process = HoverProcess.Zooming;
                this.stepCount = random.Next(5, 9);
                animal.XDirection = (random.Next(0, 2) == 0) ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = (random.Next(0, 2) == 0) ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                this.process = HoverProcess.Hovering;
                this.stepCount = random.Next(7, 11);
            }
        }
    }
}
