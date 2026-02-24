using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The interface which is used to define the role of something that is hatchable.
    /// </summary>
    public interface IHatchable
    {
        /// <summary>
        /// Hatches from its egg.
        /// </summary>
        void Hatch();
    }
}