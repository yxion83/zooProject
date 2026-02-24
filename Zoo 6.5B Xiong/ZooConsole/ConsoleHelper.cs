using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Accounts;
using Animals;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// Class that represents the console helper.
    /// </summary>
    internal static class ConsoleHelper
    {
        /// <summary>
        /// Method to add commands.
        /// </summary>
        /// <param name="zoo">Zoo that command is adding to.</param>
        /// <param name="type">Type the command is adding to.</param>
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    ConsoleHelper.AddAnimal(zoo);
                    break;
                case "guest":
                    ConsoleHelper.AddGuest(zoo);
                    break;
                default:
                    throw new Exception("The command only supports adding animals.");
                    break;
            }
        }

        /// <summary>
        /// Method to process command.
        /// </summary>
        /// <param name="zoo">Current zoo being processed.</param>
        /// <param name="type">The animal or guest typed in.</param>
        /// <param name="name">Name of the guest or animal.</param>
        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            ConsoleUtil.InitialUpper(name);

            switch (type)
            {
                case "animal":
                    ConsoleHelper.ShowAnimal(zoo, name);
                    break;
                case "guest":
                    ConsoleHelper.ShowGuest(zoo, name);
                    break;
                case "cage":
                    ConsoleHelper.ShowCage(zoo, name);
                    break;
                case "children":
                    ConsoleHelper.ShowChildren(zoo, name);
                    break;
                default:
                    Console.WriteLine("Uknown type: Only animals and guest should be written to console.");
                    break;
            }
        }

        /// <summary>
        /// Method to set the temperature in console.
        /// </summary>
        /// <param name="zoo">Zoo that is setting the temp.</param>
        /// <param name="temperature">Temperature that is being set.</param>
        public static void SetTemperature(Zoo zoo, string temperature)
        {
            try
            {
                zoo.BirthingRoomTemperature = double.Parse(temperature);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("A number must be submitted as a parameter in order to change the temperature.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered in order for the temperature command to work.");
            }
        }

        /// <summary>
        /// Command to remove guest or animal from zoo.
        /// </summary>
        /// <param name="zoo">Zoo being referenced too.</param>
        /// <param name="type">Type being removed.</param>
        /// <param name="name">Name being removed.</param>
        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            switch (type)
            {
                case "animal":
                    name = ConsoleUtil.InitialUpper(name);
                    ConsoleHelper.RemoveAnimal(zoo, name);
                    break;
                case "guest":
                    name = ConsoleUtil.InitialUpper(name);
                    ConsoleHelper.RemoveGuest(zoo, name);
                    break;
                default:
                    Console.WriteLine("This command only supports removing animals.");
                    break;
            }
        }

        /// <summary>
        /// Method to shoe the cage.
        /// </summary>
        /// <param name="zoo">The zoo being referenced.</param>
        /// <param name="animalName">Name of the animal.</param>
        public static void ShowCage(Zoo zoo, string animalName)
        {
            animalName = ConsoleUtil.InitialUpper(animalName);
            Animal animal = zoo.FindAnimal(a => a.Name == animalName);

            if (animal != null)
            {
                Cage cage = zoo.FindCage(animal.GetType());

                if (cage != null)
                {
                    Console.Write(cage.ToString());
                }
                else
                {
                    Console.WriteLine("Cage does not exist for this animal.");
                }
            }
            else
            {
                Console.WriteLine("Animal does not exist.");
            }
        }

        /// <summary>
        /// Method to show help detail.
        /// </summary>
        /// <param name="command">Command being referred too.</param>
        public static void ShowHelpDetail(string command)
        {
            Dictionary<string, string> arguments;

            switch (command)
            {
                case "show":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to show (ANIMAL, GUEST, or CAGE).");
                    arguments.Add("objectName", "The name of the object to show (use an animal name for CAGE).");
                    ConsoleUtil.WriteHelpDetail(command, "Show details of an object.", arguments);
                    break;
                case "remove":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to remove (ANIMAL or GUEST).");
                    arguments.Add("objectName", "The name of the object to remove.");
                    ConsoleUtil.WriteHelpDetail(command, "Removes an object from the zoo.", arguments);
                    break;
                case "temp":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("temperature", "Temperature to set the temperature to (in farenheight).");
                    ConsoleUtil.WriteHelpDetail(command, "Sets the temperature of the birthing room.", arguments);
                    break;
                case "add":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to add (ANIMAL or GUEST).");
                    ConsoleUtil.WriteHelpDetail(command, "Adds an object to the zoo.", arguments);
                    break;
                case "save":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("fileName", "The name of the file to be saved.");
                    ConsoleUtil.WriteHelpDetail(command, "Saves a file of the zoo.", arguments);
                    break;
                case "load":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("fileName", "The name of the file to be loaded.");
                    ConsoleUtil.WriteHelpDetail(command, "Loads a file of the zoo.", arguments);
                    break;
                case "restart":
                    ConsoleUtil.WriteHelpDetail(command, "Creates a new zoo and corresponding objects.");
                    break;
                case "exit":
                    ConsoleUtil.WriteHelpDetail(command, "Exits the application.");
                    break;
            }
        }

        /// <summary>
        /// Method to save files.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <param name="fileName">Name of file being saved.</param>
        public static void SaveFile(Zoo zoo, string fileName)
        {
            try
            {
                zoo.SaveToFile(fileName);
                Console.WriteLine("Your zoo has been successfully saved.");
            }
            catch (Exception)
            {
                Console.WriteLine("Save was unsuccessful.");
            }
        }

        /// <summary>
        /// Method to load zoo file.
        /// </summary>
        /// <param name="fileName">Zoo file name.</param>
        /// <returns>Saved zoo file.</returns>
        public static Zoo LoadFile(string fileName)
        {
            Zoo savedZoo = null;
            try
            {
                savedZoo = Zoo.LoadFromFile(fileName);
                Console.WriteLine("Zoo was successfully loaded.");
            }
            catch (Exception)
            {
                Console.WriteLine("Zoo file load was unsuccessful.");
            }

            return savedZoo;
        }

        /// <summary>
        /// Method to show help commands and their usage.
        /// </summary>
        public static void ShowHelp()
        {
            Console.WriteLine("Zoo Help Index");
            ConsoleUtil.WriteHelpDetail("help", "Show help detail.", "{command}", "The (optional) command for which to show help details.");
            Console.WriteLine("Known commands:");
            Console.WriteLine("HELP: Shows a list of known commands.");
            Console.WriteLine("EXIT: Exits the application.");
            Console.WriteLine("RESTART: Creates a new zoo.");
            Console.WriteLine("TEMP: Sets the birthing room temperature.");
            Console.WriteLine("SHOW ANIMAL [animal name]: Displays information for specified animal.");
            Console.WriteLine("SHOW GUEST [guest name]: Displays information for specified guest.");
            Console.WriteLine("ADD: Creates a guest or an animal and adds them to the zoo.");
            Console.WriteLine("REMOVE: Removes a guest or an animal from the zoo.");
            Console.WriteLine("SAVE: Saves a file of the zoo.");
            Console.WriteLine("LOAD: Loads a file of the zoo that was saved.");
        }

        /// <summary>
        /// Method when querying to find a specific animal.
        /// </summary>
        /// <param name="zoo">Zoo being referred too.</param>
        /// <param name="query">Query used to find animal.</param>
        /// <returns>String of animal info.</returns>
        public static string QueryHelper(Zoo zoo, string query)
        {
            string result = "Something went wrong";

            switch (query)
            {
                case "totalanimalweight":
                    result = zoo.Animals.ToList().Sum(a => a.Weight).ToString();
                    break;
                case "averageanimalweight":
                    result = zoo.Animals.ToList().Average(a => a.Weight).ToString();
                    break;
                case "animalcount":
                    result = zoo.Animals.ToList().Count.ToString();
                    break;
                case "getheavyanimals":
                    var animalWeightList = zoo.GetHeavyAnimals().ToList();
                    result = "Heavy Animals:\n";
                    animalWeightList.ToList().ForEach(a => result += a.ToString() + "\n");
                    break;
                case "getyoungguests":
                    var youngGuest = zoo.GetYoungGuests().ToList();
                    result = "Young Guests:\n";
                    youngGuest.ToList().ForEach(g => result += g.ToString() + "\n");
                    break;
                case "getfemaledingos":
                    var femaleDingo = zoo.GetFemaleDingos().ToList();
                    result = "Female Dingos:\n";
                    femaleDingo.ForEach(a => result += a.ToString() + "\n");
                    break;
                case "getguestsbyage":
                    var guestByAge = zoo.GetGuestsByAge().ToList();
                    result = "Guests By Age:\n";
                    guestByAge.ForEach(a => result += a.ToString() + "\n");
                    break;
                case "getflyinganimals":
                    var flyingAnimals = zoo.GetFlyingAnimals().ToList();
                    result = "Flying Animals:\n";
                    flyingAnimals.ForEach(a => result += a.ToString() + "\n");
                    break;
                case "getadoptedanimals":
                    var adoptedAnimals = zoo.GetAdoptedAnimals().ToList();
                    result = "Guests and their Adopted Animal:\n";
                    adoptedAnimals.ForEach(a => result += a.ToString() + "\n");
                    break;
                case "totalbalancebycolor":
                    var totalBalance = zoo.GetTotalBalanceByWalletColor().ToList();
                    result = "Guests and their Adopted Animal:\n";
                    totalBalance.ForEach(a => result += a.ToString() + "\n");
                    break;
                case "averageweightbyanimaltype":
                    var averageWeightByType = zoo.GetAverageWeightByAnimalType().ToList();
                    result = "Guests and their Adopted Animal:\n";
                    averageWeightByType.ForEach(a => result += a.ToString() + "\n");
                    break;
            }

            return result;
        }

        /// <summary>
        /// Method to attatch delegates.
        /// </summary>
        /// <param name="zoo">Zoo being referred too.</param>
        public static void AttachDelegates(Zoo zoo)
        {
            zoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                Console.WriteLine($"Previous temperature: {previousTemp:0.0}.");
                Console.WriteLine($"New temperature: {currentTemp:0.0}.");
            };
        }

        /// <summary>
        /// Method to remove an animal through console.
        /// </summary>
        /// <param name="zoo">Zoo being referred too.</param>
        /// <param name="name">Name of animal being removed.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);

            if (animal != null)
            {
                zoo.RemoveAnimal(animal);
                Console.WriteLine($"The following animal was removed: {name}");
            }
            else if (animal == null)
            {
                Console.WriteLine($"Animal {name} could not be found.");
            }
        }

        /// <summary>
        /// Metho to remove guest through console.
        /// </summary>
        /// <param name="zoo">Zoo being referenced.</param>
        /// <param name="name">Name of guest being removed.</param>
        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(g => g.Name == name);

            if (guest != null)
            {
                zoo.RemoveGuest(guest);
                Console.WriteLine($"The following guest was removed: {name}");
            }
            else if (guest == null)
            {
                Console.WriteLine($"Guest {name} could not be found.");
            }
        }

        /// <summary>
        /// Method to add animals in console.
        /// </summary>
        /// <param name="zoo">Zoo that animals are being added to.</param>
        private static void AddAnimal(Zoo zoo)
        {
            Animal animal = AnimalFactory.CreateAnimal(ConsoleUtil.ReadAnimalType(), "Peela", 3, 40, Gender.Male);
            animal.Name = ConsoleUtil.ReadAlphabeticValue("Name");
            animal.Name = ConsoleUtil.InitialUpper(animal.Name);
            animal.Gender = ConsoleUtil.ReadGender();
            bool success = false;
            while (!success)
            {
                try
                {
                    animal.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Age must be between 0 and 100.");
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");
                    success = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Weight must be between 0 and 1000.");
                }
            }

            zoo.AddAnimal(animal);
            ConsoleHelper.ShowAnimal(zoo, animal.Name);
        }

        /// <summary>
        /// Method to show animal in console.
        /// </summary>
        /// <param name="zoo">Zoo that contains the animal.</param>
        /// <param name="name">Name of the animal.</param>
        private static void ShowAnimal(Zoo zoo, string name)
        {
            try
            {
                string animalName = ConsoleUtil.InitialUpper(name);
                Animal animal = zoo.FindAnimal(a => a.Name == animalName);
                if (animal != null)
                {
                    Console.WriteLine($"The following animal was found: {animal}");
                }
                else
                {
                    Console.WriteLine("This animal could not be found.");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("User needs to enter a third parameter of the name of an animal.");
            }
        }

        /// <summary>
        /// Method to show the guest in the console.
        /// </summary>
        /// <param name="zoo">Zoo that contains the guest.</param>
        /// <param name="name">Name of the guest.</param>
        private static void ShowGuest(Zoo zoo, string name)
        {
            try
            {
                string guestName = ConsoleUtil.InitialUpper(name);
                Guest guest = zoo.FindGuest(g => g.Name == guestName);
                if (guest != null)
                {
                    Console.WriteLine($"The following guest was found: {guest}");
                }
                else
                {
                    Console.WriteLine("This guest could not be found.");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("User needs to enter a third parameter of the name of an guest.");
            }
        }

        /// <summary>
        /// Method to add guest in console.
        /// </summary>
        /// <param name="zoo">Zoo the guest is being added to.</param>
        private static void AddGuest(Zoo zoo)
        {
            Account checking = new Account();
            Guest guest = new Guest("Ash", 10, 0m, WalletColor.Crimson, Gender.Male, checking);
            bool sucess = false;

            while (!sucess)
            {
                try
                {
                    guest.Name = ConsoleUtil.ReadAlphabeticValue("Name");
                    sucess = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Names must only contain letter and spaces.");
                }
            }

            guest.Name = ConsoleUtil.InitialUpper(guest.Name);
            guest.Gender = ConsoleUtil.ReadGender();

            sucess = false;

            while (!sucess)
            {
                try
                {
                    guest.Age = ConsoleUtil.ReadIntValue("Age");
                    sucess = true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Age of guest must be between 0 and 120.");
                }
            }

            double moneyBalance = ConsoleUtil.ReadDoubleValue("Wallet's money balance");
            guest.Wallet.AddMoney((decimal)moneyBalance);
            guest.Wallet.Color = ConsoleUtil.ReadWalletColor();
            double checkAmount = ConsoleUtil.ReadDoubleValue("Checking account's money balance");
            checking.AddMoney((decimal)checkAmount);
            Ticket ticket = zoo.SellTicket(guest);
            zoo.AddGuest(guest, ticket);
            ConsoleHelper.ShowGuest(zoo, guest.Name);
        }

        /// <summary>
        /// Method to show children of zoo animals.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <param name="name">Name of animal being referred to.</param>
        private static void ShowChildren(Zoo zoo, string name)
        {
            string animalName = ConsoleUtil.InitialUpper(name);
            Animal animal = zoo.FindAnimal(a => a.Name == animalName);
            WalkTree(animal, string.Empty);
        }

        /// <summary>
        /// Method to show zoo's animal family tree.
        /// </summary>
        /// <param name="animal">Animal being referred to.</param>
        /// <param name="prefix">Prefix..</param>
        private static void WalkTree(Animal animal, string prefix)
        {
            Console.WriteLine($"{prefix} {animal.Name}");

            animal.Children.ToList().ForEach(a =>
            {
                WalkTree(a, prefix + "  ");
            });
        }
    }
}
