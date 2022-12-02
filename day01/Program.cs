using System;
using System.IO;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputFile = "input.txt";

            var lines = File.ReadAllLines(inputFile);

            int max = 0;
            int sum = 0;
            foreach (var line in lines)
            {
                if (line != string.Empty)
                {
                    int number = int.Parse(line);
                    sum += number;
                }
                else
                {
                    if (sum > max)
                    {
                        max = sum;
                    }

                    sum = 0;
                }
            }

            Console.WriteLine($"max: {max}");
        }
    }
}
