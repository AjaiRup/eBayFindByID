using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using eBayFindByID.FindingAPI;
using System.IO;
using System.Text.RegularExpressions;

namespace eBayFindByID
{
    public class Number
    {
        public static string UpcNumber { get; set; }
    }

    public class UserInput
    {
        public static void Start()
        {
            Number.UpcNumber  = null;
            Console.WriteLine("What is the 12-digit UPC Number you would like to search?");
            Number.UpcNumber = Console.ReadLine();
            //Confirm UPC Code is 12 digits with no letters
            while (Number.UpcNumber.Length != 12 || Regex.IsMatch(Number.UpcNumber, @"^\d+$") == false)
            {
                Console.WriteLine("Invalid UPC format, please try again or press escape to exit");
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if (pressedKey.Key != ConsoleKey.Escape)
                {
                    int n = int.Parse(pressedKey.KeyChar.ToString());
                    Number.UpcNumber = n + Console.ReadLine() ;
                }
                else
                {
                    Environment.Exit(1);
                }

            }

        }
    }
}
