using System.Text;

var lines = File.ReadAllLines("input.txt");

const int crateCharCount = 4;

bool movingCrates = false;
StackList? stacks = null;


foreach(var line in lines)
{
    if (!movingCrates)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            movingCrates = true;
            continue;
        }

        if (stacks is null)
        {
            var stackCount = (line.Length + 1) / crateCharCount;
            Console.WriteLine($"stackCount: {stackCount}");
            stacks = new StackList(stackCount);
        }

        for (var i = 0; i < stacks.Count; i++) 
        {
            var crate = line[i * crateCharCount + 1];
            if (crate != ' ')
            {
                Console.WriteLine($"Appending {crate} to crate {i+1}");
                stacks[i].Append(crate);
            }
        }
    }
    else
    {
        var moveSplit = line.Split("move ");
        var count = int.Parse(moveSplit[1].Split(" ").First());

        var fromSplit = line.Split("from ");
        var from = int.Parse(fromSplit[1].First().ToString());

        var toSplit = line.Split("to ");
        var to = int.Parse(toSplit[1].First().ToString());

        Console.WriteLine($"move {count} from {from} to {to}");
        stacks!.Move(count, from, to);
    }
}

Console.WriteLine(stacks!.Tops());

class Stack
{
    private List<char> _crates = new();

    public void Append(char crate)
    {
        _crates.Add(crate);
    }

    public IEnumerable<char> Pop(int count)
    {
        var crates = new char[count];
        _crates.CopyTo(0, crates, 0, count);
        _crates.RemoveRange(0, count);


        return crates;
    }

    public void Push(IEnumerable<char> crates)
    {
        _crates.InsertRange(0, crates);
    }

    public char Top { get => _crates[0]; }
}

class StackList
{
    private List<Stack> _stacks = new();

    public int Count { get => _stacks.Count; }

    public Stack this[int index] { get => _stacks[index]; }

    public StackList(int stackCount)
    {
        for (var i = 0; i < stackCount; i++)
        {
            _stacks.Add(new Stack());
        }
    }

    public void Move(int count, int from, int to)
    {
        var crates = _stacks[from-1].Pop(count);
        _stacks[to-1].Push(crates);
    }

    public string Tops()
    {
        var sb = new StringBuilder();
        for(var i = 0; i < _stacks.Count; i++)
        {
            sb.Append(_stacks[i].Top);
        }

        return sb.ToString();
    }
}