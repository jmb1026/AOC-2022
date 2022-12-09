var data = File.ReadAllText("input.txt");
// Console.WriteLine(data);

const int distinctCharacterCount = 14;

bool markerFound = false;
for (var i = (distinctCharacterCount-1); i < data.Length && !markerFound; i++)
{
    var counts = new Dictionary<char, int>();
    for (var j = (distinctCharacterCount-1); j >= 0; j--)
    {
        char c = data[i-j];
        if (counts.ContainsKey(c))
        {
            break;
        }

        counts.Add(c, 1);
    }

    if (counts.Keys.Count == distinctCharacterCount)
    {
        markerFound = true;
        Console.WriteLine($"Character Process Count: {i+1}");
    }
}
