using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The interface which is used to define the role of a mover.
    /// </summary>
    public interface IMover
    {
        /// <summary>
        /// Moves itself.
        /// </summary>
        void Move();
    }
}