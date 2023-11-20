var lines = File.ReadAllLines("input.txt");

var totalPriority = 0;

for (var index = 0; index < lines.Length; index += 3)
{
    var one = lines[index];
    var two = lines[index + 1];
    var three = lines[index + 2];

    var item = one.Intersect(two).Intersect(three).First();

    var priority = (item - 64 <= 26) ? (26 + item - 64) : (item - 96);
    totalPriority += priority;
}

Console.WriteLine(totalPriority);