using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statisticsApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Statistics Application ===");
            Console.WriteLine();
            
            List<int> numbers = GetUserInput();
            
            if (numbers.Count == 0)
            {
                Console.WriteLine("No valid numbers entered. Exiting...");
                Console.ReadKey();
                return;
            }
            
            DisplayStatistics(numbers);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static List<int> GetUserInput()
        {
            List<int> numbers = new List<int>();
            Console.WriteLine("Enter integers one at a time (enter -1 to finish):");
            Console.WriteLine();
            
            while (true)
            {
                Console.Write("Enter number: ");
                string input = Console.ReadLine();
                
                if (double.TryParse(input, out double parsedValue))
                {
                    int number = (int)parsedValue;
                    if (number == -1)
                    {
                        break;
                    }
                    numbers.Add(number);
                    Console.WriteLine($"Added: {number} (Total count: {numbers.Count})");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
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
            sorted.Sort();
            int count = sorted.Count;
            
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
            return numbers.Max() - numbers.Min();
        }

        static double CalculateStandardDeviation(List<int> numbers)
        {
            double mean = CalculateMean(numbers);
            double sumOfSquares = 0;
            
            for (int i = 0; i < numbers.Count; i++)
            {
                sumOfSquares += Math.Pow(numbers[i] - mean, 2);
            }
            
            return Math.Sqrt(sumOfSquares / numbers.Count);
        }

        static void DisplayStatistics(List<int> numbers)
        {
            Console.WriteLine();
            Console.WriteLine("=== Statistical Results ===");
            Console.WriteLine($"Data Set: [{string.Join(", ", numbers)}]");
            Console.WriteLine($"Count: {numbers.Count}");
            Console.WriteLine();
            
            Console.WriteLine($"Mean (Average): {CalculateMean(numbers):F2}");
            Console.WriteLine($"Median: {CalculateMedian(numbers):F2}");
            
            List<int> modes = CalculateMode(numbers);
            
            // Check if there's no mode (all values appear equally)
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
