using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// aurthor: ching ho, Li
// student id: 30493763
// last update date: 06-Feb-2026
// last update time: 11:29 AM
// description: This program calculates the factorial of a given number using two different algorithms and compares their performance.
// github repo:https://github.com/30493763/statisticsApplication.git

namespace statisticsApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("=== Statistics Application ===");
                Console.WriteLine();

                // get user input with static method GetUserInput()
                // integers stored in a list (that can be accessed by index and resized dynamically.)
                // user can enter as many as they want until they enter -1 to finish
                List<int> numbers = GetUserInput();

                if (numbers.Count == 0)
                {
                    Console.WriteLine("No valid numbers entered. Exiting...");
                    Console.ReadKey();
                    return;
                }

                DisplayStatistics(numbers);

                // Ask if user wants to run again
                Console.WriteLine();
                Console.Write("Do you want to calculate statistics again? (y/n): ");
                string response = Console.ReadLine()?.ToLower();

                if (response == "n")
                {
                    continueRunning = false;
                    Console.WriteLine("\nThank you for using Statistics Application. Goodbye!");
                }
                else if (response == "y")
                {
                    Console.Clear(); // Clear console for next run
                }
                else
                {
                    Console.WriteLine("Invalid input. Exiting application...");
                    continueRunning = false;
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        //***********************************************************************************************************************************
        //         HELPER METHODS
        //***********************************************************************************************************************************

        // GetUserInput: Prompts the user to enter integers one at a time until they enter -1 to finish. Validates input and returns a list of integers.
        static List<int> GetUserInput()
        {
            const string INVALID_INPUT_MESSAGE = "Invalid input. Please enter a valid number (integer) or enter -1 to finish.";
            List<int> numbers = new List<int>();
            Console.WriteLine("Enter integers one at a time (enter -1 to finish):");
            Console.WriteLine();
            
            while (true)
            {
                Console.Write("Enter number: ");
                if (int.TryParse(Console.ReadLine(), out int parsedValue)) //Use int.TryParse() to avoid crash if the user enters non integer input.
                {
                    int number = (int)parsedValue;
                    if (number == -1) 
                        break;
                    else if (number ==0 || number < -1) 
                        Console.WriteLine($"{INVALID_INPUT_MESSAGE}");
                    else
                    {
                        numbers.Add(number);
                        Console.WriteLine($"Added: {number} (Total count: {numbers.Count})");
                    }
                }
                else
                    Console.WriteLine($"{INVALID_INPUT_MESSAGE}");
            }
            return numbers;
        }

        static double CalculateMean(List<int> numbers)
        {
            double sum = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sum += numbers[i];
            }
            return sum / numbers.Count;
        }

        static double CalculateMedian(List<int> numbers)
        {
            List<int> sorted = new List<int>(numbers);
            sorted.Sort(); //Use numbers.Sort() to sort the list.
            int count = sorted.Count; //Use numbers.Count to get the number of elements in the list.

            if (count % 2 == 0)
            {
                // Even count: average of two middle values
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
            }
            else
            {
                // Odd count: middle value
                return sorted[count / 2];
            }
        }

        static List<int> CalculateMode(List<int> numbers)
        {
            // Use Dictionary to store frequencies
            Dictionary<int, int> frequencies = new Dictionary<int, int>();
            
            for (int i = 0; i < numbers.Count; i++)
            {
                if (frequencies.ContainsKey(numbers[i]))
                {
                    frequencies[numbers[i]]++;
                }
                else
                {
                    frequencies[numbers[i]] = 1;
                }
            }
            
            if (frequencies.Count == 0)
                return new List<int>();
            
            // Find the maximum frequency
            int maxFrequency = 0;
            foreach (var freq in frequencies.Values)
            {
                if (freq > maxFrequency)
                {
                    maxFrequency = freq;
                }
            }
            
            // Get all numbers with the highest frequency
            List<int> modes = new List<int>();
            foreach (var kvp in frequencies)
            {
                if (kvp.Value == maxFrequency)
                {
                    modes.Add(kvp.Key);
                }
            }
            
            return modes;
        }

        static int CalculateRange(List<int> numbers)
        {
            return numbers.Max() - numbers.Min(); //Use numbers.Max() and numbers.Min() to get max and min values.
        }

        // Standard Deviation: Measures the amount of variation or dispersion in a set of values. It is calculated as the square root of the average of the squared differences from the mean.
        static double CalculateStandardDeviation(List<int> numbers)
        {
            double mean = CalculateMean(numbers);
            double sumOfSquares = 0;
            
            for (int i = 0; i < numbers.Count; i++)
            {
                sumOfSquares += Math.Pow(numbers[i] - mean, 2);
            }
            
            return Math.Sqrt(sumOfSquares / numbers.Count); // Use Math.Sqrt() to calculate the square root.
        }

        // method to display all the statistics in a formatted manner
        static void DisplayStatistics(List<int> numbers)
        {
            Console.WriteLine();
            Console.WriteLine("=== Statistical Results ===");
            Console.WriteLine($"Data Set: [{string.Join(", ", numbers)}]"); //Use string.Join(", ", mode) to print a list of mode values.
            Console.WriteLine($"Count: {numbers.Count}");
            Console.WriteLine();
            
            Console.WriteLine($"Mean (Average): {CalculateMean(numbers):F2}");
            Console.WriteLine($"Median: {CalculateMedian(numbers):F2}");
            
            List<int> modes = CalculateMode(numbers);
            
            // Check if there's no mode (all values appear equally)
            Dictionary<int, int> frequencies = new Dictionary<int, int>(); //Use Dictionary<int, int> to store frequencies.
            for (int i = 0; i < numbers.Count; i++)
            {
                if (frequencies.ContainsKey(numbers[i]))
                {
                    frequencies[numbers[i]]++;
                }
                else
                {
                    frequencies[numbers[i]] = 1;
                }
            }
            
            if (modes.Count == frequencies.Count)
            {
                Console.WriteLine("Mode: No mode (all values appear equally)");
            }
            else
            {
                Console.WriteLine($"Mode: {string.Join(", ", modes)} (appears {frequencies[modes[0]]} time(s))");
            }
            
            Console.WriteLine($"Range: {CalculateRange(numbers)}");
            Console.WriteLine($"Standard Deviation: {CalculateStandardDeviation(numbers):F2}");
            Console.WriteLine($"Minimum: {numbers.Min()}");
            Console.WriteLine($"Maximum: {numbers.Max()}");
        }
    }
}
