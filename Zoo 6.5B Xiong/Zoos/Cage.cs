using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using BoothItems;
using CagedItems;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a cage.
    /// </summary>
    [Serializable]
    public class Cage
    {
        /// <summary>
        /// Animals in the cage.
        /// </summary>
        private List<ICageable> cagedItems;

        /// <summary>
        /// Image update for the cage class.
        /// </summary>
        [NonSerialized]
        private Action<ICageable> onImageUpdate;

        /// <summary>
        /// Initializes a new instance of the cage class.
        /// </summary>
        /// <param name="height">Cage height.</param>
        /// <param name="width">Cage width.</param>
        /// <param name="animalType">Type of animal in the cage.</param>
        public Cage(int height, int width)
        {
            this.cagedItems = new List<ICageable>();
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Gets a value of the cage height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets a value of the cage width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets a value of the animal list.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        /// <summary>
        /// Gets or sets a value of the image update in the cage.
        /// </summary>
        public Action<ICageable> OnImageUpdate
        {
            get
            {
                return this.onImageUpdate;
            }

            set
            {
                this.onImageUpdate = value;
            }
        }

        /// <summary>
        /// Adds animals to the cage.
        /// </summary>
        /// <param name="cagedItem">cagedItem added to cage.</param>
        public void Add(ICageable cagedItem)
        {
            this.cagedItems.Add(cagedItem);
            cagedItem.OnImageUpdate += this.HandleImageUpdate;
            this.OnImageUpdate?.Invoke(cagedItem);
        }

        /// <summary>
        /// Removes animals from the cage.
        /// </summary>
        /// <param name="cagedItem">cagedItem removed from the cage.</param>
        public void Remove(ICageable cagedItem)
        {
            this.cagedItems.Remove(cagedItem);
            cagedItem.OnImageUpdate -= this.HandleImageUpdate;
            this.OnImageUpdate?.Invoke(cagedItem);
        }

        /// <summary>
        /// Shows the cage's information.
        /// </summary>
        /// <returns>string.</returns>
        public override string ToString()
        {
            string result = $"{this.cagedItems[0].GetType()} cage ({this.Width} x {this.Height})";
            foreach (ICageable cagedItem in this.cagedItems)
            {
                result += $"{Environment.NewLine}{cagedItem} ({cagedItem.XPosition} x {cagedItem.YPosition})";
            }

            result += $"{Environment.NewLine}";

            return result;
        }

        /// <summary>
        /// Method to handle the image update in the cage.
        /// </summary>
        /// <param name="item">Item being referred to.</param>
        private void HandleImageUpdate(ICageable item)
        {
            this.OnImageUpdate?.Invoke(item);
        }
    }
}
