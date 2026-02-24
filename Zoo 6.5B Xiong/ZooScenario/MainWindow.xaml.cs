using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using CagedItems;
using Foods;
using Microsoft.Win32;
using People;
using Reproducers;
using VendingMachines;
using Zoos;
using Path = System.IO.Path;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Autosaves.
        /// </summary>
        private const string AutoSaveFileName = "Autosave.zoo";

        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

#if DEBUG
            this.Title += " [DEBUG]";
#endif
        }

        /// <summary>
        /// Created button to admit guest.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            Guest ethel = new Guest("Ethel", 42, 30.00m, WalletColor.Salmon, Gender.Male, account);

            try
            {
                Ticket ticket = this.comoZoo.SellTicket(ethel);
                this.comoZoo.AddGuest(ethel, ticket);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.GetType().ToString() + ": " + ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Created button to feed animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;
            if (guest != null && animal != null)
            {
                guest.FeedAnimal(animal);
            }
            else
            {
                MessageBox.Show("User must choose both a guest and an animal to feel an animal.");
            }

            this.guestListBox.SelectedItem = guest;
            this.animalListBox.SelectedItem = animal;
        }

        /// <summary>
        /// Botton to decrease temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature--;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Button to increase temperature.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Loads the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            bool result = this.LoadZoo("Autosave.zoo");
            if (result == false)
            {
                this.comoZoo = Zoo.NewZoo();
            }
            else
            {
                this.AttachDelegates();
            }

            this.changeMoveBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));
            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));
        }

        /// <summary>
        /// Button to add new animals.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimalType type = (AnimalType)this.animalTypeComboBox.SelectedItem;
                Animal chip = AnimalFactory.CreateAnimal(type, "Chip", 2, 2.4, Gender.Male);
                Window window = new AnimalWindow(chip);
                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    this.comoZoo.AddAnimal(chip);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An animal must be selected before adding an animal to the zoo.");
            }
        }

        /// <summary>
        /// Button to add a guest to the zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Account checking = new Account();
            Guest guest = new Guest("Bob", 2, 200, WalletColor.Crimson, Gender.Male, checking);

            try
            {
                Ticket ticket = this.comoZoo.SellTicket(guest);
                Window window = new GuestWindow(guest);
                window.ShowDialog();

                if (window.DialogResult == true)
                {
                    this.comoZoo.AddGuest(guest, ticket);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Button to remove animal from zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            if (animal != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveAnimal(animal);
                }
            }
            else if (animal == null)
            {
                MessageBox.Show("Select an animal to remove.");
            }
        }

        /// <summary>
        /// Button to remove guest from zoo.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveGuest(guest);
                }
            }
            else if (guest == null)
            {
                MessageBox.Show("Select a guest to remove.");
            }
        }

        /// <summary>
        /// Edits guest values in list box.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void guestListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                Window window = new GuestWindow(guest);
                window.ShowDialog();
            }
        }

        /// <summary>
        /// Edits animals values in list box.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void animalListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Window window = new AnimalWindow(animal);
                window.ShowDialog();

                if (window.DialogResult == true && animal.IsPregnant == true)
                {
                    this.comoZoo.RemoveAnimal(animal);
                    this.comoZoo.AddAnimal(animal);
                }
            }
        }

        /// <summary>
        /// Button to show cage window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                Window window = new CageWindow(cage);
                window.Show();
            }
            else
            {
                MessageBox.Show("User must select an animal to show.");
            }
        }

        /// <summary>
        /// Button to adopt an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                Predicate<Guest> findGuest = guest => guest.AdoptedAnimal == null;
                Guest visitor = this.comoZoo.FindGuest(findGuest);

                if (visitor != null && animal != null)
                {
                    visitor.AdoptedAnimal = animal;
                    cage.Add(visitor);
                }
                else
                {
                    MessageBox.Show("No guests avaiable to adopt the animal.");
                }
            }
            else if (animal == null)
            {
                MessageBox.Show("User Must select an animal to adopt.");
            }
        }

        /// <summary>
        /// Button to unadopt an animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;
            if (guest != null && animal != null)
            {
                if (guest.AdoptedAnimal == animal)
                {
                    Cage cage = this.comoZoo.FindCage(guest.AdoptedAnimal.GetType());
                    guest.AdoptedAnimal = null;
                    cage.Remove(guest);
                }
                else
                {
                    MessageBox.Show($"Animal named {animal.Name} is not the guest's adopted animal.");
                }
            }
            else
            {
                MessageBox.Show("User must select a guest and animal.");
            }
        }

        /// <summary>
        /// Button to change the move behavior of the animal.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void changeMoveBehaviorButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            object type = this.changeMoveBehaviorComboBox.SelectedItem;

            if (animal != null && type != null)
            {
                IMoveBehavior moveBehavior = MoveBehaviorFactory.CreateMoveBehavior((MoveBehaviorType)type);
                animal.MoveBehavior = moveBehavior;
            }
            else
            {
                MessageBox.Show("User must select both an animal and movement type.");
            }
        }

        /// <summary>
        /// Botton to give birth to new animals.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void birthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthAnimal();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Button click to save.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";
            if (dialog.ShowDialog() == true)
            {
                this.SaveZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// Button click to load file.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";
            if (dialog.ShowDialog() == true)
            {
                this.LoadZoo(dialog.FileName);
            }
        }

        /// <summary>
        /// Method to save the zoo as a file.
        /// </summary>
        /// <param name="fileName">Zoo's file name.</param>
        private void SaveZoo(string fileName)
        {
            this.comoZoo.SaveToFile(fileName);
            this.SetWindowTitle(fileName);
        }

        /// <summary>
        /// Method to load files.
        /// </summary>
        /// <param name="fileName">Name of the file being loaded.</param>
        /// <returns>Success of loaded file.</returns>
        private bool LoadZoo(string fileName)
        {
            bool result = true;

            try
            {
                this.comoZoo = Zoo.LoadFromFile(fileName);
                this.SetWindowTitle(fileName);
            }
            catch
            {
                MessageBox.Show("File could not be loaded.");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Method to clear the window.
        /// </summary>
        private void ClearWindow()
        {
            this.guestListBox.Items.Clear();
            this.animalListBox.Items.Clear();
        }

        /// <summary>
        /// Method to set the title of the window.
        /// </summary>
        /// <param name="fileName">Windows title name.</param>
        private void SetWindowTitle(string fileName)
        {
            // Set the title of the window using the current file name
            this.Title = $"Object-Oriented Programming 2: Zoo [{Path.GetFileName(fileName)}]";
        }

        /// <summary>
        /// Button to restart.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start over?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.ClearWindow();
                this.comoZoo = Zoo.NewZoo();
                this.SetWindowTitle("Object-Oriented Programming 2: Zoo");
                this.AttachDelegates();
            }
        }

        /// <summary>
        /// Method to autosave zoo when the window closes.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveZoo("Autosave.zoo");
        }

        /// <summary>
        /// Method to attach delegates.
        /// </summary>
        private void AttachDelegates()
        {
            this.comoZoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                // Controls the appearance of the Windows of the Zoo's temperature.
                this.temperatureBorder.Height = 2 * currentTemp;
                this.temperatureLabel.Content = currentTemp;
                this.temperatureLabel.Content = string.Format("{0:0.0} °F", this.temperatureLabel.Content);

                double colorLevel = ((currentTemp - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

                this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                    Convert.ToByte(colorLevel),
                    Convert.ToByte(255 - colorLevel),
                    Convert.ToByte(255 - colorLevel)));
            };

            this.comoZoo.OnBirthingRoomTemperatureChange += (previousTemp, currentTemp) =>
            {
                Console.WriteLine(previousTemp.ToString());
                Console.WriteLine(currentTemp.ToString());
            };

            this.comoZoo.OnAddGuest = guest =>
            {
                this.guestListBox.Items.Add(guest);
                guest.OnTextChange += this.UpdateGuestDisplay;
            };

            this.comoZoo.OnRemoveGuest = guest =>
            {
                this.guestListBox.Items.Remove(guest);
                guest.OnTextChange -= this.UpdateGuestDisplay;
            };

            this.comoZoo.OnAddAnimal = animal =>
            {
                this.animalListBox.Items.Add(animal);
                animal.OnTextChange += this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnRemoveAnimal = animal =>
            {
                this.animalListBox.Items.Remove(animal);
                animal.OnTextChange -= this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnDeserialized();
        }

        /// <summary>
        /// Method to update guests being displayed.
        /// </summary>
        /// <param name="guest">Guest being referred to in update.</param>
        private void UpdateGuestDisplay(Guest guest)
        {
            int index = this.guestListBox.Items.IndexOf(guest);

            if (index >= 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.guestListBox.Items.RemoveAt(index);

                    this.guestListBox.Items.Insert(index, guest);

                    this.guestListBox.SelectedItem = this.guestListBox.Items[index];
                });
            }
        }

        /// <summary>
        /// Method to update animals being displayed.
        /// </summary>
        /// <param name="animal">Animal being referred to in update.</param>
        private void UpdateAnimalDisplay(Animal animal)
        {
            int index = this.animalListBox.Items.IndexOf(animal);

            if (index >= 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    // disconnect the animal.
                    this.animalListBox.Items.RemoveAt(index);

                    // create a new animal item in the same spot.
                    this.animalListBox.Items.Insert(index, animal);

                    // Re-select the animal.
                    this.animalListBox.SelectedItem = this.animalListBox.Items[index];
                });
            }
        }
    }
}