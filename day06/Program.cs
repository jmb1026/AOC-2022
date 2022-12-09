var data = File.ReadAllText("input.txt");
// Console.WriteLine(data);

bool markerFound = false;
for (var i = 3; i < data.Length && !markerFound; i++)
{
    var counts = new Dictionary<char, int>();
    for (var j = 3; j >= 0; j--)
    {
        char c = data[i-j];
        if (counts.ContainsKey(c))
        {
            break;
        }

        counts.Add(c, 1);
    }

    if (counts.Keys.Count == 4)
    {
        markerFound = true;
        Console.WriteLine($"Character Process Count: {i+1}");
    }
}
