using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


// Needed to add appropriate nuget packages including Newtonsoft to convert the JSON file


namespace jsonConversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("records.json");
            var records = JsonConvert.DeserializeObject<RootObject>(json).Records;

            Console.WriteLine("Enter '#' to see the numbers list or 'N' to see the names list:");
            string userInput = Console.ReadLine();

            // Process user input and display the appropriate list
            if (userInput == "#")
            {
                var sortedNumbers = GetSortedValues(records, "number");
                Console.WriteLine("Numbers List:");
                sortedNumbers.ForEach(Console.WriteLine);
            }
            else if (userInput.ToUpper() == "N")
            {
                var sortedTexts = GetSortedValues(records, "text");
                Console.WriteLine("Names List:");
                sortedTexts.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            // var sortedNumbers = GetSortedValues(records, "number");
            // var sortedTexts = GetSortedValues(records, "text");

            // Example usage
            // Console.WriteLine("Sorted Numbers:");
            // sortedNumbers.ForEach(Console.WriteLine);

            // Console.WriteLine("\nSorted Texts:");
            // sortedTexts.ForEach(Console.WriteLine);


        }

        private static List<string> GetSortedValues(List<Record> records, string type)
        {
            if (type == "number")
            {
                return records.Select(r => r.Number.ToString())
                              .OrderBy(n => int.Parse(n))
                              .ToList();
            }
            else // Assuming type is "text"
            {
                return records.Select(r => new String(r.Text.Where(char.IsLetter).ToArray()))
                              .Select(t => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(t))
                              .OrderBy(t => t)
                              .ToList();
            }
        }

        public class Record
        {
            public int Number { get; set; }
            public string Text { get; set; }
        }

        public class RootObject
        {
            public List<Record> Records { get; set; }
        }
    }
}
