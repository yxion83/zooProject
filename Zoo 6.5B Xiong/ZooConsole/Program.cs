using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Animals;
using BirthingRooms;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// Class that represents the Zoo console.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The Zoo created.
        /// </summary>
        private static Zoo zoo;

        /// <summary>
        /// Main method of entry point.
        /// </summary>
        /// <param name="args">The argument.</param>
        public static void Main(string[] args)
        {
            Console.Title = "Object-Oriented Programming 2: Zoo";
            Console.WriteLine("Welcome to the Como Zoo!");

            zoo = Zoo.NewZoo();
            ConsoleHelper.AttachDelegates(zoo);
            bool exit = false;
            string command;
            try
            {
                while (!exit)
                {
                    Console.Write("] ");
                    command = Console.ReadLine();
                    command = command.ToLower().Trim();
                    string[] commandWords = command.Split();

                    switch (commandWords[0])
                    {
                        case "exit":
                            exit = true;
                            break;
                        case "restart":
                            zoo = Zoo.NewZoo();
                            ConsoleHelper.AttachDelegates(zoo);
                            Console.WriteLine("A new Como Zoo has been created.");
                            zoo.BirthingRoomTemperature = 77;
                            break;
                        case "help":
                            if (commandWords.Length == 2)
                            {
                                ConsoleHelper.ShowHelpDetail(commandWords[1]);
                            }
                            else if (commandWords.Length == 1)
                            {
                                ConsoleHelper.ShowHelp();
                            }
                            else
                            {
                                Console.WriteLine("Too many parameters were entered.");
                            }

                            break;
                        case "temp":
                            ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                            break;
                        case "show":
                            try
                            {
                                ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Additional parameters are needed.");
                            }

                            break;
                        case "add":
                            ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                            break;
                        case "remove":
                            ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                            break;
                        case "sort":
                            try
                            {
                                SortResult result = null;

                                if (commandWords[1] == "animals")
                                {
                                    result = zoo.SortAnimals(commandWords[2], commandWords[3]);
                                    result.Objects.ToList().ForEach(a =>
                                    {
                                        Console.WriteLine(a.ToString());
                                    });
                                }
                                else if (commandWords[1] == "guests")
                                {
                                    result = zoo.SortGuests(commandWords[2], commandWords[3]);
                                    result.Objects.ToList().ForEach(g =>
                                    {
                                        Console.WriteLine(g.ToString());
                                    });
                                }

                                Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                                Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                                Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());
                                Console.WriteLine("SWAP COUNT: " + result.SwapCount);
                                Console.WriteLine("COMPARE COUNT: " + result.CompareCount);
                                Console.WriteLine("TIME: " + result.ElapsedMilliseconds);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort by -- guests or animals] [sort type] [sort value -- animalWeight or guestName].");
                            }

                            break;
                        case "search":
                            if (commandWords[1] == "binary")
                            {
                                int loopCounter = 0;
                                string animalName = ConsoleUtil.InitialUpper(commandWords[2]);
                                SortResult animals = zoo.SortAnimals("bubble", "animalname");
                                animals.Objects = new List<object>().Cast<object>().ToList();

                                int minPosition = 0;
                                int maxPosition = zoo.Animals.Count() - 1;

                                while (minPosition <= maxPosition)
                                {
                                    int middlePosition = (minPosition + maxPosition) / 2;
                                    loopCounter++;
                                    int compare = string.Compare(animalName, ((Animal)animals.Objects[middlePosition]).Name);

                                    if (compare > 0)
                                    {
                                        minPosition = middlePosition + 1;
                                    }
                                    else if (compare < 0)
                                    {
                                        maxPosition = middlePosition - 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{animalName} found. {loopCounter} loops complete.");
                                        break;
                                    }
                                }
                            }
                            else if (commandWords[1] == "linear")
                            {
                                int loopCounter = 0;
                                string animalName = ConsoleUtil.InitialUpper(commandWords[2]);
                                foreach (Animal a in zoo.Animals)
                                {
                                    loopCounter++;

                                    if (a.Name == animalName)
                                    {
                                        Console.WriteLine($"{animalName} found. {loopCounter} loops complete.");
                                    }
                                }
                            }
                            else if (commandWords[1] == "guest")
                            {
                            }

                            break;
                        case "query":
                            try
                            {
                                string query = ConsoleHelper.QueryHelper(zoo, commandWords[1]);
                                Console.WriteLine(query);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Inccorrect query.");
                            }

                            break;
                        case "save":
                            try
                            {
                                ConsoleHelper.SaveFile(zoo, commandWords[1]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("User must enter the name of the file.");
                            }

                            break;
                        case "load":
                            try
                            {
                                zoo = ConsoleHelper.LoadFile(commandWords[1]);
                                ConsoleHelper.AttachDelegates(zoo);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("User must enter the name of the file they want to load.");
                            }

                            break;
                        default:
                            Console.WriteLine($"The command '{command}' does not exist.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
