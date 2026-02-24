using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// Interface that defines eating behaviors.
    /// </summary>
    public interface IEatBehavior
    {
        /// <summary>
        /// Method to eat.
        /// </summary>
        /// <param name="eater">Object that is eating.</param>
        /// <param name="food">Food being eaten.</param>
        void Eat(IEater eater, Food food);
    }
}
