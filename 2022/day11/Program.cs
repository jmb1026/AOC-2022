var lines = File.ReadAllLines("input.txt");

List<Monkey> monkeys = new();
Monkey? current = null;

bool insideTest = false;
int currentTrueResult = 0;
int currentFalseResult = 0;
int currentDenom = 0;

foreach (var line in lines)
{
    if (line.Contains("Monkey"))
    {
        current = new Monkey();
        monkeys.Add(current);
    }
    else if (line.Contains("Starting items:"))
    {
        var items = line.Split("Starting items: ")[1].Split(", ");
        foreach (var item in items)
        {
            current!.Catch(int.Parse(item));
        }
    }
    else if (line.Contains("Test:") || insideTest)
    {
        insideTest = true;

        if (line.Contains("If true: "))
        {
            currentTrueResult = int.Parse(line[^1].ToString());
        }
        else if (line.Contains("If false: "))
        {
            currentFalseResult = int.Parse(line[^1].ToString());

            insideTest = false;
            current!.Test = CreateTest(currentDenom, currentTrueResult, currentFalseResult);
        }
        else
        {
            currentDenom = int.Parse(line.Split("Test: divisible by ")[1]);
        }

    }
    else if (line.Contains("Operation"))
    {

    }
}

Func<int, int> CreateTest(int denom, int trueResult, int falseResult)
{
    return (int item) => (item % denom == 0) ? trueResult : falseResult;
}

class Monkey
{
    private List<int> _items = new();

    IEnumerable<int> Items { get => _items; }

    public Func<int, int> Operation { get; set; }

    public Func<int, int> Test { get; set; }

    public void Catch(int item) => _items.Add(item);

    public bool Inspect(List<Monkey> monkeys)
    {
        if (_items.Any()) return false;

        var item = _items[0];
        _items.Remove(item);

        item = Operation(item);
        ThrowTo(monkeys[Test(item)], item);

        return true;
    }

    static void ThrowTo(Monkey monkey, int item)
    {
        monkey.Catch(item);
    }
}