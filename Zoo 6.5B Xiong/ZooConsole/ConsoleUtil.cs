using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;
using Utilities;

namespace ZooConsole
{
    /// <summary>
    /// Class that represents the consoleutil.
    /// </summary>
    internal static class ConsoleUtil
    {
        /// <summary>
        /// Method to capitalize the first letter.
        /// </summary>
        /// <param name="value">String passed in.</param>
        /// <returns>String that has been capitalized.</returns>
        public static string InitialUpper(string value)
        {
            string capitalize = null;

            if (value != null && value.Length > 0)
            {
                capitalize = char.ToUpper(value[0]) + value.Substring(1);
            }

            return capitalize;
        }

        /// <summary>
        /// Method for consile util to read alphabetic values.
        /// </summary>
        /// <param name="prompt">Command being prompt.</param>
        /// <returns>The alphabetic values being read.</returns>
        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        /// <summary>
        /// Method for the console util to read the double values.
        /// </summary>
        /// <param name="prompt">Command being prompt.</param>
        /// <returns>Double values being read.</returns>
        public static double ReadDoubleValue(string prompt)
        {
            double result = 0.0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Method for the console util to read the gender.
        /// </summary>
        /// <returns>Gender being read.</returns>
        public static Gender ReadGender()
        {
            Gender result = Gender.Female;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse<Gender>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender. Possible gender types: " + GetTypes(typeof(Gender)));
                }
            }

            return result;
        }

        /// <summary>
        /// Method for the console to read the wallet color.
        /// </summary>
        /// <returns>String value of wallet color.</returns>
        public static WalletColor ReadWalletColor()
        {
            WalletColor result = WalletColor.Brown;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Wallet Color");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse<WalletColor>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid wallet color. Possible color types: " + GetTypes(typeof(WalletColor)));
                }
            }

            return result;
        }

        /// <summary>
        /// Method for the console util to read the integer values.
        /// </summary>
        /// <param name="prompt">Command being prompt.</param>
        /// <returns>Int values being read.</returns>
        public static int ReadIntValue(string prompt)
        {
            int result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        /// <summary>
        /// Method to read the animal's type.
        /// </summary>
        /// <returns>Animal Type.</returns>
        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Dingo;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Animal Type");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse<AnimalType>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type. Possible animal types: " + GetTypes(typeof(AnimalType)));
                }
            }

            return result;
        }

        /// <summary>
        /// Method to read the string values.
        /// </summary>
        /// <param name="prompt">String command being prompt.</param>
        /// <returns>String values being read.</returns>
        public static string ReadStringValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "] ");

                string stringValue = Console.ReadLine().ToLower().Trim();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        /// <summary>
        /// Method for consileutil to get types.
        /// </summary>
        /// <param name="type">Type being passed in.</param>
        /// <returns>String value of type.</returns>
        public static string GetTypes(Type type)
        {
            StringBuilder typeList = new StringBuilder();

            foreach (string at in Enum.GetNames(type))
            {
                typeList.Append(at + "|");
            }

            return "|" + typeList.ToString();
        }

        /// <summary>
        /// Method for console util to write help details with multiple parameters.
        /// </summary>
        /// <param name="command">Command being referred too.</param>
        /// <param name="overview">Overview.</param>
        /// <param name="arguments">Argument being passed in.</param>
        public static void WriteHelpDetail(string command, string overview, Dictionary<string, string> arguments)
        {
            if (arguments != null)
            {
                string upperCommand = command.ToUpper();
                Console.WriteLine($"Command name:  {upperCommand}");
                Console.WriteLine($"Overview: {overview}");
                Console.WriteLine($"Usage: {upperCommand} {arguments.Keys.Flatten(" ")}");
                Console.WriteLine("Parameter:");

                arguments.ToList().ForEach(kvp => Console.WriteLine($"{kvp.Key}: {kvp.Value}"));
            }
            else
            {
                string upperCommand = command.ToUpper();
                Console.WriteLine($"Command name:  {upperCommand}");
                Console.WriteLine($"Overview: {overview}");
            }
        }

        /// <summary>
        /// Writes help detail for commands with a parameter.
        /// </summary>
        /// <param name="command">Command being referred too.</param>
        /// <param name="overview">Overview.</param>
        /// <param name="argument">Argument being passed in.</param>
        /// <param name="argumentUsage">Argument usage.</param>
        public static void WriteHelpDetail(string command, string overview, string argument, string argumentUsage)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add(argument, argumentUsage);
            WriteHelpDetail(command, overview, dictionary);
        }

        /// <summary>
        /// Writes help detail with only command and description.
        /// </summary>
        /// <param name="command">Command being referred to.</param>
        /// <param name="overview">Overview.</param>
        public static void WriteHelpDetail(string command, string overview)
        {
            WriteHelpDetail(command, overview, null);
        }
    }
}
