using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class used to represent climb behavior.
    /// </summary>
    [Serializable]
    public class ClimbBehavior : IMoveBehavior
    {
        /// <summary>
        /// Randomizes.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The max height.
        /// </summary>
        private int maxHeight;

        /// <summary>
        /// Climbing process.
        /// </summary>
        private ClimbProcess process;

        /// <summary>
        /// Method to move for climb behavior.
        /// </summary>
        /// <param name="animal">Animal being referred too.</param>
        public void Move(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    animal.YDirection = VerticalDirection.Up;
                    MoveHelper.MoveVertically(animal, animal.MoveDistance);
                    if (animal.YPosition - animal.MoveDistance <= this.maxHeight)
                    {
                        animal.YDirection = VerticalDirection.Down;

                        if (animal.XDirection == HorizontalDirection.Left)
                        {
                            animal.XDirection = HorizontalDirection.Right;
                        }
                        else
                        {
                            animal.XDirection = HorizontalDirection.Left;
                        }

                        this.NextProcess(animal);
                    }

                    break;
                case ClimbProcess.Falling:
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
                    MoveHelper.MoveVertically(animal, animal.MoveDistance * 2);
                    if (animal.YPosition + animal.MoveDistance >= animal.YPositionMax)
                    {
                        this.NextProcess(animal);
                    }

                    break;
                case ClimbProcess.Scurrying:
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
                    if (animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                    {
                        animal.XPosition = animal.XPositionMax;
                        this.NextProcess(animal);
                    }
                    else if (animal.XPosition - animal.MoveDistance <= 0)
                    {
                        animal.XPosition = 0;
                        this.NextProcess(animal);
                    }

                    break;
            }
        }

        /// <summary>
        /// Method to get to the next process.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        private void NextProcess(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    this.process = ClimbProcess.Falling;
                    break;
                case ClimbProcess.Falling:
                    this.process = ClimbProcess.Scurrying;
                    break;
                case ClimbProcess.Scurrying:
                    int higherMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.15));
                    int lowerMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.85));

                    this.maxHeight = ClimbBehavior.random.Next(higherMax, lowerMax + 1);
                    this.process = ClimbProcess.Climbing;
                    break;
            }
        }
    }
}
