using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth.
    /// </summary>
    [Serializable]
    public abstract class Booth
    {
        /// <summary>
        /// The Booth's list of items.
        /// </summary>
        private readonly List<Item> items;

        /// <summary>
        /// The employee currently assigned to be the attendant of the booth.
        /// </summary>
        private Employee attendant;

        /// <summary>
        /// Initializes a new instance of the Booth class.
        /// </summary>
        /// <param name="attendant">The employee to be the booth's attendant.</param>
        public Booth(Employee attendant)
        {
            this.items = new List<Item>();
            this.attendant = attendant;
        }

        /// <summary>
        /// Gets a value of the booths attendant.
        /// </summary>
        protected Employee Attendant
        {
            get
            {
                return this.attendant;
            }
        }

        /// <summary>
        /// Gets a value of a listt of all the booths items.
        /// </summary>
        protected List<Item> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}