using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using Reproducers;

namespace BirthingRooms
{
    /// <summary>
    /// The class which is used to represent a birthing room.
    /// </summary>
    [Serializable]
    public class BirthingRoom
    {
        /// <summary>
        /// The maximum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MaxTemperature = 95.0;

        /// <summary>
        /// The minimum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MinTemperature = 35.0;

        /// <summary>
        /// The initial temperature of the birthing room.
        /// </summary>
        private readonly double initialTemperature = 77.0;

        /// <summary>
        /// The current temperature of the birthing room.
        /// </summary>
        private double temperature;

        /// <summary>
        /// The employee currently assigned to be the vet of the birthing room.
        /// </summary>
        private Employee vet;

        /// <summary>
        /// Initializes a new instance of the BirthingRoom class.
        /// </summary>
        /// <param name="vet">The employee to be the birthing room's vet.</param>
        public BirthingRoom(Employee vet)
        {
            this.Temperature = this.initialTemperature;
            this.vet = vet;
            this.PregnantAnimals = new Queue<IReproducer>();
        }

        /// <summary>
        /// Gets or sets a value of the temperature change.
        /// </summary>
        public Action<double, double> OnTemperatureChange { get; set; }

        /// <summary>
        /// Gets a value of a queue for pregnant animals.
        /// </summary>
        public Queue<IReproducer> PregnantAnimals { get; private set; }

        /// <summary>
        /// Gets or sets the birthing room's temperature.
        /// </summary>
        public double Temperature
        {
            get
            {
                return this.temperature;
            }

            set
            {
                double previousTemp = this.temperature;

                // If the value is in range...
                if (value < MinTemperature)
                {
                    throw new ArgumentOutOfRangeException(paramName: "temperature", message: "The temperature must be above 35 degrees.");
                }
                else if (value > MaxTemperature)
                {
                    throw new ArgumentOutOfRangeException(paramName: "temperature", message: "The temperature must be below 95 degrees.");
                }
                else if (value >= MinTemperature && value <= MaxTemperature)
                {
                    this.temperature = value;
                    this.OnTemperatureChange?.Invoke(previousTemp, this.temperature);
                }
            }
        }

        /// <summary>
        /// Births a reproducer.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer BirthAnimal(IReproducer reproducer)
        {
            IReproducer baby = null;

            // If the reproducer is present and is pregnant...
            if (reproducer != null && reproducer.IsPregnant)
            {
                baby = this.vet.DeliverAnimal(reproducer);

                // Increase the temperature due to the heat generated from birthing.
                this.Temperature += 0.5;
            }

            return baby;
        }
    }
}