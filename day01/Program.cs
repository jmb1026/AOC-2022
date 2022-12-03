﻿using System;
using System.Collections.Generic;
using System.IO;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bag> bags = new();

            const string inputFile = "input.txt";
            var lines = File.ReadAllLines(inputFile);

            var currentBag = CreateBag();
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    currentBag.Add(int.Parse(line));
                }
                else
                {
                    currentBag = CreateBag();
                }
            }

            bags.Sort((x, y) => y.Total - x.Total);

            int total = bags[0].Total + bags[1].Total + bags[2].Total;
            Console.WriteLine(total);

            Bag CreateBag()
            {
                var bag = new Bag();
                bags.Add(bag);

                return bag;
            }
        }
    }
}
