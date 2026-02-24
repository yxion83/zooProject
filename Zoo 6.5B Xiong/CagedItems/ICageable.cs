using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CagedItems
{
    /// <summary>
    /// The interface which is used to make changes to the cages.
    /// </summary>
    public interface ICageable
    {
        /// <summary>
        /// Gets a value of the cage display size.
        /// </summary>
        double DisplaySize { get; }

        /// <summary>
        /// Gets a value of the resource key.
        /// </summary>
        string ResourceKey { get; }

        /// <summary>
        /// Gets a value of the xposition.
        /// </summary>
        int XPosition { get; }

        /// <summary>
        /// Gets a  value of the yposiiton.
        /// </summary>
        int YPosition { get; }

        /// <summary>
        /// Gets a value indicating whether the status is active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets a value of the horizontal direction.
        /// </summary>
        HorizontalDirection XDirection { get; }

        /// <summary>
        /// Gets a vlue of the vertical direction.
        /// </summary>
        VerticalDirection YDirection { get; }

        /// <summary>
        /// Gets a value of the hunger status.
        /// </summary>
        HungerState HungerState { get; }

        /// <summary>
        /// Gets or sets a image update in the cage.
        /// </summary>
        Action<ICageable> OnImageUpdate { get; set; }
    }
}
