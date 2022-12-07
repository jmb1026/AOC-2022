var lines = File.ReadAllLines("input.txt");

var totalPriority = 0;

foreach(var rs in lines)
{
    var split = rs.Length / 2;

    var firstItems = rs.Substring(0, rs.Length / 2);
    var secondItems = rs.Substring(rs.Length / 2);

    var item = firstItems.Intersect(secondItems).First();

    var priority = (item - 64 <= 26) ? (26 + item - 64) : (item - 96);
    totalPriority += priority;
}

Console.WriteLine(totalPriority);