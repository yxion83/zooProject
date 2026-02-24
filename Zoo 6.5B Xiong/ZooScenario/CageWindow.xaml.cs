using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using CagedItems;
using Utilities;
using Zoos;
using Timer = System.Timers.Timer;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class CageWindow : Window
    {
        /// <summary>
        /// Animals inside the cage window.
        /// </summary>
        private Cage cage;

        /// <summary>
        /// Initializes a new instance of the CageWindow class.
        /// </summary>
        /// <param name="cage">cage in the cage window.</param>
        public CageWindow(Cage cage)
        {
            this.cage = cage;
            this.InitializeComponent();

            this.cage.OnImageUpdate = item =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate()
                    {
                        int zIndex = 0;

                        foreach (Viewbox v in this.cageGrid.Children)
                        {
                            if (v.Tag == item)
                            {
                                this.cageGrid.Children.Remove(v);
                                break;
                            }

                            zIndex++;
                        }

                        if (item.IsActive == true)
                        {
                            this.DrawItem(item, zIndex);
                        }
                    }));
                }
                catch (TaskCanceledException)
                {
                }
            };
        }

        /// <summary>
        /// Draws items in cage window.
        /// </summary>
        /// <param name="item">Item drawn in the cage.</param>
        /// <param name="zIndex">Current Index.</param>
        private void DrawItem(ICageable item, int zIndex)
        {
            Viewbox viewBox = this.GetViewBox(800, 400, item.XPosition, item.YPosition, item.ResourceKey, item.DisplaySize);

            viewBox.HorizontalAlignment = HorizontalAlignment.Left;
            viewBox.VerticalAlignment = VerticalAlignment.Top;

            TransformGroup transformGroup = new TransformGroup();

            if (item.HungerState == HungerState.Tired)
            {
                SkewTransform tiredSkew = new SkewTransform();
                tiredSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;
                transformGroup.Children.Add(tiredSkew);
                transformGroup.Children.Add(new ScaleTransform(0.75, 0.5));
            }

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewBox.
                viewBox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance.
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewBox horizontally so the animal faces to the left
                flipTransform.ScaleX = -1;

                // Add the flipTransfrom to the transforms Children.
                transformGroup.Children.Add(flipTransform);
            }

            viewBox.RenderTransform = transformGroup;
            viewBox.Tag = item;
            this.cageGrid.Children.Insert(zIndex, viewBox);
        }

        /// <summary>
        /// Draws all animals in the zoo to the cage window.
        /// </summary>
        private void DrawAllItems()
        {
            this.cageGrid.Children.Clear();
            int zIndex = 0;

            this.cage.CagedItems.ToList().ForEach(c => this.DrawItem(c, zIndex++));
        }

        /// <summary>
        /// Allows user to view the animal.
        /// </summary>
        /// <param name="maxXPosition">Sets animals max x positon.</param>
        /// <param name="maxYPosition">Sets animals max y positon.</param>
        /// <param name="xPosition">Sets animals x positon.</param>
        /// <param name="yPosition">Sets animals y positon.</param>
        /// <param name="resourceKey">ResourceKey of the viewbox.</param>
        /// <param name="displayScale">DisplaySize of the viewbox.</param>
        /// <returns>Viewbox.</returns>
        private Viewbox GetViewBox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewBox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewBox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxXPosition;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxYPosition;

            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewBox.
            return finishedViewBox;
        }

        /// <summary>
        /// Loads the event to view the animal in the cage window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        /// <summary>
        /// Method that triggers when window is closed.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Closed(object sender, EventArgs e)
        {
            this.cage.OnImageUpdate = null;
        }
    }
}
