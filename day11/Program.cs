var lines = File.ReadAllLines("input.txt");

foreach(var line in lines)
{

}

var test = new Monkey();
test.Catch(85);
test.Catch(79);
test.Catch(63);
test.Catch(72);

test.Operation = (int item) => (item * 17) / 3;
test.Test = (int item) => (item % 2 == 0) ? 2 : 6;

var monkeys = new List<Monkey>();

for(int i = 0; i < 20; i++)
{
    foreach(var monkey in monkeys)
    {
        while(monkey.Inspect());
    }
}


class Monkey
{
    private List<int> _items = new();

    IEnumerable<int> Items { get => _items; }

    private Func<int, int>  Operation { get; set; }

    private Func<int, int>  Test { get; set; }

    public void Catch(int item) => _items.Add(item);

    public bool Inspect(List<Monkey> monkeys)
    {
        if (_items.Any()) return false;

        var item = _items[0];
        _items.Remove(item);

        item = Operation(item);
        ThrowTo(monkeys[Test(item)]);

        return true;
    }

    void ThrowTo(Monkey monkey)
    {
        monkey.Catch(item);
    }
}