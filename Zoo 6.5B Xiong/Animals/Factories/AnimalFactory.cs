using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal factory.
    /// </summary>
    public static class AnimalFactory
    {
        /// <summary>
        /// Method to created an animal.
        /// </summary>
        /// <param name="type">Animal type.</param>
        /// <param name="name">Animal name.</param>
        /// <param name="age">Animal age.</param>
        /// <param name="weight">Animal weight.</param>
        /// <param name="gender">Animal gender.</param>
        /// <returns>Animal created.</returns>
        public static Animal CreateAnimal(AnimalType type, string name, int age, double weight, Gender gender)
        {
            Animal result = null;

            switch (type)
            {
                case AnimalType.Chimpanzee:
                    result = new Chimpanzee(name, age, weight, gender);
                    break;
                case AnimalType.Dingo:
                    result = new Dingo(name, age, weight, gender);
                    break;
                case AnimalType.Eagle:
                    result = new Eagle(name, age, weight, gender);
                    break;
                case AnimalType.Hummingbird:
                    result = new Hummingbird(name, age, weight, gender);
                    break;
                case AnimalType.Kangaroo:
                    result = new Kangaroo(name, age, weight, gender);
                    break;
                case AnimalType.Ostrich:
                    result = new Ostrich(name, age, weight, gender);
                    break;
                case AnimalType.Platypus:
                    result = new Platypus(name, age, weight, gender);
                    break;
                case AnimalType.Shark:
                    result = new Shark(name, age, weight, gender);
                    break;
                case AnimalType.Squirrel:
                    result = new Squirrel(name, age, weight, gender);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
