using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using CagedItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoos list of cages.
        /// </summary>
        private Dictionary<Type, Cage> cages;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's information booth.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// Zoos  delegate for birthing room temperature change.
        /// </summary>
        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperatureChange;

        /// <summary>
        /// Zoo delegate for adding on guest.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onAddGuest;

        /// <summary>
        /// Zoo delegate for removing guest.
        /// </summary>
        [NonSerialized]
        private Action<Guest> onRemoveGuest;

        /// <summary>
        /// Zoo Delegate for adding animal.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onAddAnimal;

        /// <summary>
        /// Zoo Delegate for removing animals.
        /// </summary>
        [NonSerialized]
        private Action<Animal> onRemoveAnimal;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="boothMoneyBalance">The initial money balance of the zoo's ticket booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        /// <param name="waterBottlePrice">The price of the water bottle.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());
            this.informationBooth = new GivingBooth(attendant);
            this.ticketBooth.AddMoney(boothMoneyBalance);

            this.b168.OnTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.OnBirthingRoomTemperatureChange?.Invoke(previousTemp, currentTemp);
            };

            this.cages = new Dictionary<Type, Cage>();
            foreach (AnimalType a in Enum.GetValues(typeof(AnimalType)))
            {
                this.cages.Add(Animal.ConvertAnimalTypeToType(a), new Cage(400, 800));
            }

            // Animals for sorting
            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));

            // Guests for sorting
            this.AddGuest(new Guest("Anna", 8, 12.56m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Matthew", 42, 10.0m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Doug", 7, 11.10m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Jared", 17, 31.70m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sean", 34, 20.50m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sally", 52, 134.20m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));

            Shark chum = new Shark("Chum", 14, 377.3, Gender.Male);
            this.AddAnimal(chum);
            Squirrel chip = new Squirrel("Chip", 4, 1.0, Gender.Male);
            this.AddAnimal(chip);
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            Guest greg = new Guest("Greg", 35, 100.0m, WalletColor.Crimson, Gender.Male, new Account());
            Guest darla = new Guest("Darla", 7, 10.0m, WalletColor.Brown, Gender.Male, new Account());

            greg.AdoptedAnimal = chip;
            darla.AdoptedAnimal = chum;
            this.AddGuest(greg, new Ticket(0m, 0, 0));
            this.AddGuest(darla, new Ticket(0m, 0, 0));
        }

        /// <summary>
        /// Gets or sets a value of the delegate to remove animals.
        /// </summary>
        public Action<Animal> OnRemoveAnimal
        {
            get
            {
                return this.onRemoveAnimal;
            }

            set
            {
                this.onRemoveAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the delegate to add animals.
        /// </summary>
        public Action<Animal> OnAddAnimal
        {
            get
            {
                return this.onAddAnimal;
            }

            set
            {
                this.onAddAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the being added on.
        /// </summary>
        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }

            set
            {
                this.onAddGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the guest being removed.
        /// </summary>
        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }

            set
            {
                this.onRemoveGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets a value of the zoos birthing room temperature change.
        /// </summary>
        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperatureChange;
            }

            set
            {
                this.onBirthingRoomTemperatureChange = value;
            }
        }

        /// <summary>
        /// Gets a value of the animal list to the animalslistbox.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets a value of the guest list to the guestlistbox.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                // Define accumulator variable.Add current animal's weight to the total.
                double totalWeight = 0;

                // Loop through the list of animals.
                this.animals.ToList().ForEach(a => totalWeight += a.Weight);

                return totalWeight;
            }
        }

        /// <summary>
        /// Method to load zoo from file.
        /// </summary>
        /// <param name="fileName">Zoo file name.</param>
        /// <returns>Zoo filed saved.</returns>
        public static Zoo LoadFromFile(string fileName)
        {
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            return result;
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>Zoo created.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

/*            // Add money to the animal snack machine.
            comoZoo.AnimalSnackMachine.AddMoney(42.75m);*/

            return comoZoo;
        }

        /// <summary>
        /// Methods to sort objects in zoo.
        /// </summary>
        /// <param name="sortType">The sort type.</param>
        /// <param name="sortValue">The sort values.</param>
        /// <param name="list">List.</param>
        /// <returns>Sorting result.</returns>
        public SortResult SortObjects(string sortType, string sortValue, IList list)
        {
            Func<object, object, int> compareFunc = null;

            if (sortValue == "animalname")
            {
                compareFunc = AnimalNameSortComparer;
            }
            else if (sortValue == "guestname")
            {
                compareFunc = GuestNameSortComparer;
            }
            else if (sortValue == "animalweight")
            {
                compareFunc = WeightSortComparer;
            }
            else if (sortValue == "animalage")
            {
                compareFunc = AgeSortComparer;
            }
            else if (sortValue == "moneybalance")
            {
                compareFunc = MoneyBalanceSortComparer;
            }

            SortResult result = null;

            switch (sortType)
            {
                case "bubble":
                    result = list.BubbleSort(compareFunc);
                    break;
                case "selection":
                    result = list.SelectionSort(compareFunc);
                    break;
                case "insertion":
                    result = list.InsertionSort(compareFunc);
                    break;
                case "quick":
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    SortResult sortResult = new SortResult();
                    result = list.QuickSort(0, list.Count - 1, sortResult, compareFunc);

                    stopwatch.Stop();
                    sortResult.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Method to sort animals.
        /// </summary>
        /// <param name="sortType">The sort type.</param>
        /// <param name="sortValue">The sort value.</param>
        /// <returns>Sorted animal list.</returns>
        public SortResult SortAnimals(string sortType, string sortValue)
        {
            SortResult result = this.SortObjects(sortType, sortValue, this.animals);
            return result;
        }

        /// <summary>
        /// Method to sort guest.
        /// </summary>
        /// <param name="sortType">The sort type.</param>
        /// <param name="sortValue">The sort value.</param>
        /// <returns>Sorted guest list.</returns>
        public SortResult SortGuests(string sortType, string sortValue)
        {
            SortResult result = this.SortObjects(sortType, sortValue, this.guests);
            return result;
        }

        /// <summary>
        /// Method to find the zoo's cages.
        /// </summary>
        /// <param name="animalType">Type of animal.</param>
        /// <returns>Cage of animal requested.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage result = null;

            this.cages.TryGetValue(animalType, out result);

            return result;
        }

        /// <summary>
        /// Method to get an animal type.
        /// </summary>
        /// <param name="type">Animal type.</param>
        /// <returns>List of animal types in zoo.</returns>
        public List<Animal> GetAnimals(Type type)
        {
            List<Animal> animalList = new List<Animal>();

            foreach (Animal a in this.animals)
            {
                if (a.GetType() == type)
                {
                    animalList.Add(a);
                }
            }

            return animalList;
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.cages[animal.GetType()].Add(animal);
            animal.OnPregnant += pregnantAnimal => this.b168.PregnantAnimals.Enqueue(pregnantAnimal);

            animal.IsActive = true;
            this.animals.Add(animal);
            this.OnAddAnimal?.Invoke(animal);
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="animal">Animal removed.</param>
        public void RemoveAnimal(Animal animal)
        {
            foreach (Guest g in this.Guests)
            {
                if (g.AdoptedAnimal == animal)
                {
                    g.AdoptedAnimal = null;
                    this.cages[animal.GetType()].Remove(g);
                }
            }

            animal.IsActive = false;
            this.cages[animal.GetType()].Remove(animal);
            this.animals.Remove(animal);
            this.OnRemoveAnimal?.Invoke(animal);
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        public void BirthAnimal()
        {
            try
            {
                // Birth animal.
                IReproducer baby = this.b168.BirthAnimal(this.b168.PregnantAnimals.Dequeue());

                // If the baby is an animal...
                if (baby is Animal)
                {
                    // Add the baby to the zoo's list of animals.
                    this.AddAnimal(baby as Animal);
                }
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The zoo currently has no pregnant animals.");
            }
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket to add.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket == null)
            {
                throw new NullReferenceException("Guest can not be admitted as they have no ticket.");
            }
            else if (ticket != null)
            {
                this.guests.Add(guest);
                this.OnAddGuest?.Invoke(guest);
                guest.GetVendingMachine += this.ProvideVendingMachine;
                ticket.Redeem();
            }
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="guest">Guest being removed.</param>
        public void RemoveGuest(Guest guest)
        {
            if (guest.AdoptedAnimal != null)
            {
                Cage cage = this.FindCage(guest.AdoptedAnimal.GetType());
                cage.Remove(guest);
            }

            guest.IsActive = false;
            this.guests.Remove(guest);
            this.onRemoveGuest?.Invoke(guest);
        }

        /// <summary>
        /// Sells tickets to guest.
        /// </summary>
        /// <param name="guest">The guest buying the ticket.</param>
        /// <returns>Ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            Ticket ticket = guest.VisitTicketBooth(this.ticketBooth);
            guest.VisitInformationBooth(this.informationBooth);

            return ticket;
        }

        /// <summary>
        /// Method to save zoo file.
        /// </summary>
        /// <param name="fileName">File Name of zoo saved.</param>
        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of the zoo
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Method to deserialize.
        /// </summary>
        public void OnDeserialized()
        {
            this.OnBirthingRoomTemperatureChange?.Invoke(this.BirthingRoomTemperature, this.BirthingRoomTemperature);

            this.guests.ForEach(g =>
            {
                this.OnAddGuest?.Invoke(g);
                g.GetVendingMachine += this.ProvideVendingMachine;
            });

            this.animals.ForEach(a =>
            {
                this.OnAddAnimal?.Invoke(a);
                a.OnPregnant += pregnantAnimal => this.b168.PregnantAnimals.Enqueue(pregnantAnimal);
            });
        }

        /// <summary>
        /// Method to compare sorting by animal name.
        /// </summary>
        /// <param name="object1">First animal.</param>
        /// <param name="object2">Second animal.</param>
        /// <returns>Integar.</returns>
        private static int AnimalNameSortComparer(object object1, object object2)
        {
            Animal animal1 = (Animal)object1;
            Animal animal2 = (Animal)object2;

            int compare = string.Compare(animal1.Name, animal2.Name);

            return compare;
        }

        /// <summary>
        /// Method to compare sorting by guest name.
        /// </summary>
        /// <param name="object1">First guest.</param>
        /// <param name="object2">Second guest.</param>
        /// <returns>Integar.</returns>
        private static int GuestNameSortComparer(object object1, object object2)
        {
            Guest guest1 = (Guest)object1;
            Guest guest2 = (Guest)object2;

            int compare = string.Compare(guest1.Name, guest2.Name);

            return compare;
        }

        /// <summary>
        /// Method to compare sort guest's money balance.
        /// </summary>
        /// <param name="object1">First guest's money balance.</param>
        /// <param name="object2">Seond guest's money balance.</param>
        /// <returns>Integar.</returns>
        private static int MoneyBalanceSortComparer(object object1, object object2)
        {
            Guest guest1 = (Guest)object1;
            Guest guest2 = (Guest)object2;

            decimal balanceTotal1 = guest1.Wallet.MoneyBalance + guest1.CheckingAccount.MoneyBalance;
            decimal balanceTotal2 = guest2.Wallet.MoneyBalance + guest2.CheckingAccount.MoneyBalance;

            return balanceTotal1 == balanceTotal2 ? 0 : balanceTotal1 > balanceTotal2 ? 1 : -1;
        }

        /// <summary>
        /// Method to compare sorting by weight.
        /// </summary>
        /// <param name="object1">First animal.</param>
        /// <param name="object2">Second animal.</param>
        /// <returns>Integar.</returns>
        private static int WeightSortComparer(object object1, object object2)
        {
            Animal animal1 = (Animal)object1;
            Animal animal2 = (Animal)object2;

            double weight1 = animal1.Weight;
            double weight2 = animal2.Weight;

            return weight1 == weight2 ? 0 : weight1 > weight2 ? 1 : -1;
        }

        /// <summary>
        /// Method to compare sorting by age.
        /// </summary>
        /// <param name="object1">First animal.</param>
        /// <param name="object2">Second animal.</param>
        /// <returns>Integar.</returns>
        private static int AgeSortComparer(object object1, object object2)
        {
            Animal animal1 = (Animal)object1;
            Animal animal2 = (Animal)object2;

            double age1 = animal1.Age;
            double age2 = animal2.Age;

            return age1 == age2 ? 0 : age1 > age2 ? 1 : -1;
        }

        /// <summary>
        /// Method to add multiple animals to the zoo.
        /// </summary>
        /// <param name="animals">Animals being added to the zoo.</param>
        private void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            animals.ToList().ForEach(animal =>
            {
                this.AddAnimal(animal);
                this.AddAnimalsToZoo(animal.Children);
            });
        }

        /// <summary>
        /// Method to provide a vending machine.
        /// </summary>
        /// <returns>A vending machine.</returns>
        private VendingMachine ProvideVendingMachine()
        {
            return this.animalSnackMachine;
        }
    }
}