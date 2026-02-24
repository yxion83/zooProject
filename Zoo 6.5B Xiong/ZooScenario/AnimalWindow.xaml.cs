using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using Reproducers;
using static System.Net.Mime.MediaTypeNames;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for AnimalWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class AnimalWindow : Window
    {
        /// <summary>
        /// The animals in the zoo.
        /// </summary>
        private Animal animal;

        /// <summary>
        /// Initializes a new instance of the AnimalWindow class.
        /// </summary>
        /// <param name="animal">Animal in the zoo.</param>
        public AnimalWindow(Animal animal)
        {
            this.animal = animal;
            this.InitializeComponent();
        }

        /// <summary>
        /// Method to load the Animal Window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            // Converting values into string to be put into textbox.
            this.nameTextBox.Text = this.animal.Name.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();
            this.ageTextBox.Text = this.animal.Age.ToString();

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.animal.Gender;

            this.pregnancyStatusLabel.Content = this.animal.IsPregnant ? "Yes" : "No";
        }

        /// <summary>
        /// Button to confirm.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Updates name value in the text box.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Name = this.nameTextBox.Text;
            }
            catch (FormatException)
            {
                MessageBox.Show("Names can only include letters and spaces.");
            }
        }

        /// <summary>
        /// Updates age value in the text box.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Age must be between 0 and 100.");
            }
        }

        /// <summary>
        /// Updates weight value in the text box.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void weightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Weight = double.Parse(this.weightTextBox.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Weight must be between 0 and 1000.");
            }
        }

        /// <summary>
        /// Button to make the animal pregnant.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void makePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            this.animal.MakePregnant();
            this.makePregnantButton.IsEnabled = false;
            this.pregnancyStatusLabel.Content = "Yes";
        }

        /// <summary>
        /// Button to select the animals gender.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.animal.Gender = (Gender)this.genderComboBox.SelectedItem;
            this.makePregnantButton.IsEnabled = (this.animal.Gender == Gender.Female) ? true : false;
        }
    }
}
