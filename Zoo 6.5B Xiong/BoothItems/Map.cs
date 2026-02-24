using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a Map.
    /// </summary>
    [Serializable]
    public class Map : Item
    {
        /// <summary>
        /// Date Map was issued.
        /// </summary>
        private DateTime dateIssued;

        /// <summary>
        /// Initializes a new instance of the Map class.
        /// </summary>
        /// <param name="weight">Weight of the map.</param>
        /// <param name="dateIssued">Date this map was issued.</param>
        public Map(double weight, DateTime dateIssued)
            : base(weight)
        {
            this.dateIssued = dateIssued;
        }

        /// <summary>
        /// Gets a value of date this map was issued.
        /// </summary>
        public DateTime DateIssued
        {
            get
            {
                return this.dateIssued;
            }
        }
    }
}
