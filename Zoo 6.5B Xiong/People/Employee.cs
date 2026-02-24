using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Animals;
using BoothItems;
using Foods;
using Reproducers;

namespace People
{
    /// <summary>
    /// The class which is used to represent an employee.
    /// </summary>
    [Serializable]
    public class Employee : IEater
    {
        /// <summary>
        /// The name of the employee.
        /// </summary>
        private string name;

        /// <summary>
        /// The employee's identification number.
        /// </summary>
        private int number;

        /// <summary>
        /// The number of rooms the employee has sterilized.
        /// </summary>
        private int numberOfRoomsSterilized;

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        /// <param name="name">The name of the employee.</param>
        /// <param name="number">The employee's identification number.</param>
        public Employee(string name, int number)
        {
            this.name = name;
            this.number = number;
        }

        /// <summary>
        /// Gets or sets the weight of the employee.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public double WeightGainPercentage
        {
            get { return 0.0; }
        }

        /// <summary>
        /// Aids the specified reproducer in delivering its baby.
        /// </summary>
        /// <param name="reproducer">The reproducer tsehat is to give birth.</param>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer DeliverAnimal(IReproducer reproducer)
        {
            // Sterilize birthing area.
            this.SterilizeBirthingArea();

            // Give birth.
            IReproducer baby = reproducer.Reproduce();

            if (baby is IMover)
            {
                // Make the baby move.
                (baby as IMover).Move();
            }

            if (baby is Animal)
            {
                // Name the baby.
                (baby as Animal).Name = "Baby";
            }

            return baby;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Method to find a specific item.
        /// </summary>
        /// <param name="items">List of items.</param>
        /// <param name="type">Type of item.</param>
        /// <returns>Item requested.</returns>
        public Item FindItem(List<Item> items, Type type)
        {
            Item item = null;

            if (items.Count > 0)
            {
                foreach (Item i in items)
                {
                    if (i.GetType() == type)
                    {
                        item = i;
                        items.Remove(i);
                        break;
                    }
                }

                if (item == null)
                {
                    throw new MissingItemException($"An item of type {type.Name} does not exist.");
                }
            }

            return item;
        }

        /// <summary>
        /// Sterilizes the birthing area in preparation for delivering a baby.
        /// </summary>
        private void SterilizeBirthingArea()
        {
            this.numberOfRoomsSterilized++;
        }
    }
}