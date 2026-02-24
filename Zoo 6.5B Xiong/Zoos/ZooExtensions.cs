using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;

namespace Zoos
{
    /// <summary>
    /// Class that represents the extensions for the zoo.
    /// </summary>
    public static class ZooExtensions
    {
        /// <summary>
        /// Finds an animal based on name.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <param name="match">The name of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public static Animal FindAnimal(this Zoo zoo, Predicate<Animal> match)
        {
            // Define a variable to hold a matching animal.
            Animal animal = null;

            zoo.Animals.ToList().ForEach(a =>
            {
                animal = zoo.Animals.ToList().Find(match);
            });

            // Return the matching animal.
            return animal;
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <param name="match">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo zoo, Predicate<Guest> match)
        {
            // Define a variable to hold matching guest.
            Guest guest = null;

            zoo.Guests.ToList().ForEach(g =>
            {
                guest = zoo.Guests.ToList().Find(match);
            });

            // Return the matching guest.
            return guest;
        }

        /// <summary>
        /// Method to get a list of adopted animals.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>List of adopted animals.</returns>
        public static IEnumerable<object> GetAdoptedAnimals(this Zoo zoo)
        {
            return from g in zoo.Guests
                   where g.AdoptedAnimal != null
                   select new { g.Name, AnimalName = g.AdoptedAnimal.Name, AnimalType = g.AdoptedAnimal.GetType().Name };
        }

        /// <summary>
        /// Method to get the average weight by animal type.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>Average weight based on animal type.</returns>
        public static IEnumerable<object> GetAverageWeightByAnimalType(this Zoo zoo)
        {
            return from a in zoo.Animals
                   group a by a.GetType().Name.ToString() into ar
                   orderby ar.Key
                   select new { AnimalType = ar.Key, AverageWeight = ar.Average(g => g.Weight) };
        }

        /// <summary>
        /// Method to get female dingos in the zoo.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>Female dingos.</returns>
        public static IEnumerable<object> GetFemaleDingos(this Zoo zoo)
        {
            return
                    from a in zoo.Animals
                    where a.Gender == Gender.Female && a.GetType() == typeof(Dingo)
                    select new { a.Name, a.Age, a.Weight, a.Gender };
        }

        /// <summary>
        /// Method to get flying animals in the zoo.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>List of flying animals.</returns>
        public static IEnumerable<object> GetFlyingAnimals(this Zoo zoo)
        {
            return from a in zoo.Animals
                   where a.MoveBehavior.GetType() == typeof(FlyBehavior)
                   select new { AnimalType = a.GetType().Name, a.Name };
        }

        /// <summary>
        /// Method to get guest by age.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>List of guest by age.</returns>
        public static IEnumerable<object> GetGuestsByAge(this Zoo zoo)
        {
            return from g in zoo.Guests
                   orderby g.Age ascending
                   select new { g.Name, g.Age, g.Gender };
        }

        /// <summary>
        /// Method to geat heave animals in the zoo.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>Heavy animals.</returns>
        public static IEnumerable<object> GetHeavyAnimals(this Zoo zoo)
        {
            return from a in zoo.Animals
                   where a.Weight > 200
                   select new { Type = a.GetType().Name, a.Name, a.Age, a.Weight };
        }

        /// <summary>
        /// Method to get the total balance in the wallet.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>Total balance by wallet.</returns>
        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo zoo)
        {
            return from g in zoo.Guests
                   group g by g.Wallet.Color.ToString() into gr
                   orderby gr.Key
                   select new { gr.Key, TotalMoneyBalance = gr.Sum(g => g.Wallet.MoneyBalance) };
        }

        /// <summary>
        /// Method to get younger guest in the zoo.
        /// </summary>
        /// <param name="zoo">Zoo being referred to.</param>
        /// <returns>The younger guest.</returns>
        public static IEnumerable<object> GetYoungGuests(this Zoo zoo)
        {
            return
                    from g in zoo.Guests
                    where g.Age <= 10
                    select new { g.Name, g.Age };
        }
    }
}
