var lines = File.ReadAllLines("input.txt");

var monkeys = new List<Monkey>();

MonkeyParser? parser = null;

foreach(var line in lines)
{
    if (line.Contains("Monkey"))
    {
        parser = new MonkeyParser(monkeys.Count);
        monkeys.Add(parser.Monkey);

    }
    else if (parser != null)
    {
        parser.Parse(line);
    }
}

var worryReducer = monkeys.Select(m => m.Denom).Aggregate((long a, long b) => a * b);

for (long i = 0; i < 10000; i++)
{
    foreach(var monkey in monkeys)
    {
        while(monkey.Inspect(monkeys, worryReducer));
    }
}

monkeys.Sort((a, b) => a.InspectCount < b.InspectCount ? -1 : a.InspectCount > b.InspectCount ? 1 : 0);

foreach (var monkey in monkeys)
{
    Console.WriteLine($"Monkey {monkey.Number} inspected items {monkey.InspectCount} times.");
}

class Monkey
{
    private List<long> _items = new();

    public long Number { get; set; }

    public long Denom { get; set; }

    public long InspectCount { get; private set; }

    IEnumerable<long> Items { get => _items; }

    public Func<long, long>  Operation { get; set; }

    public Func<long, int>  Test { get; set; }

    public void Catch(long item) => _items.Add(item);

    public bool Inspect(List<Monkey> monkeys, long worryReducer)
    {
        if (_items.Count == 0) return false;


        InspectCount++;

        var item = _items[0];
        _items.Remove(item);

        item = Operation(item) % worryReducer;
        var index = Test(item);
        ThrowTo(monkeys[index], item);

        return true;
    }

    void ThrowTo(Monkey monkey, long item)
    {
        monkey.Catch(item);
    }
}

class MonkeyParser
{
    private Monkey _monkey = new Monkey();

    private bool _inTest = false;
    private long _denom = 0;
    private int _trueIndex = 0;
    private int _falseIndex = 0;

    public MonkeyParser(long number)
    {
        _monkey.Number = number;
    }

    public Monkey Monkey { get => _monkey; }

    public void Parse(string line)
    {
        if (_inTest)
        {
            ParseTest(line);
        }
        else if (line.Contains("Starting items"))
        {
            ParseStartingItems(line);
        }
        else if (line.Contains("Operation"))
        {
            ParseOperation(line);
        }
        else if (line.Contains("Test"))
        {
            _inTest = true;
            ParseTest(line);
        }
    }

    private void ParseStartingItems(string line)
    {
        var items = line.Split(": ")[1].Split(", ").Select(i => long.Parse(i));
        foreach (var item in items)
        {
            _monkey.Catch(item);
        }
    }

    private void ParseOperation(string line)
    {
        var equation = line.Split(": new = ")[1];

        if (equation.Contains("+"))
        {
            var op = equation.Split(" + ");
            var left = op[0]; // this should always be 'old'
            var right = op[1];
            _monkey.Operation = Add(right);
        }
        else if (equation.Contains("*"))
        {
            var op = equation.Split(" * ");
            var left = op[0]; // this should always be 'old'
            var right = op[1];
            _monkey.Operation = Multiply(right);
        }

        Func<long, long> Add(string operand)
        {
            if (operand == "old")
            {
                return (long item) => item + item;
            }

            return (long item) => item + long.Parse(operand);
        }
        
        Func<long, long> Multiply(string operand)
        {
            if (operand == "old")
            {
                return (long item) => item * item;
            }

            return (long item) => item * long.Parse(operand);
        }
    }

    private void ParseTest(string line)
    {
        if (line.Contains("Test"))
        {
            _denom = long.Parse(line.Split("divisible by ")[1]);
            _monkey.Denom = _denom;
        }
        else if (line.Contains("If true"))
        {
            _trueIndex = int.Parse(line.Split("monkey ")[1]);
        }
        else if (line.Contains("If false"))
        {
            _falseIndex = int.Parse(line.Split("monkey ")[1]);

            _monkey.Test = (long value) => (value % _denom) == 0 ? _trueIndex : _falseIndex;
            _inTest = false;
        }
    }
}