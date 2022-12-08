var pairs = File.ReadAllLines("input.txt");

var count = 0;

foreach(var input in pairs)
{
    var pair = input.Split(",");
    var first = pair[0].Split("-");
    var second = pair[1].Split("-");

    var firstRange = new 
    { 
        Min = int.Parse(first[0]), 
        Max = int.Parse(first[1]) 
    };
    var secondRange = new 
    { 
        Min = int.Parse(second[0]), 
        Max = int.Parse(second[1]) 
    };

    // Part 1
    // if (secondRange.Min >= firstRange.Min && secondRange.Max <= firstRange.Max)
    // {
    //     count++;
    // }
    // else if (firstRange.Min >= secondRange.Min && firstRange.Max <= secondRange.Max)
    // {
    //     count++;
    // }

    if (firstRange.Min > secondRange.Max || firstRange.Max < secondRange.Min)
    {
        continue;
    }

    count++;
}

Console.WriteLine(count);