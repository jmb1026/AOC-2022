var lines = File.ReadAllLines("input.txt");

var x = new Register{ Value = 1 };
List<Instruction> instructions = new();

foreach(var line in lines)
{
    if (line == "noop")
    {
        instructions.Add(new NoopInstruction());
    }
    else
    {
        var addx = line.Split(" ");
        var value = int.Parse(addx[1]);
        instructions.Add(new AddXInstruction(x, value));
    }
}

List<char> pixels = new(40*6);

int cycle = 1;
while(instructions.Any())
{
    var inst = instructions.First();
    
    int pixel = (cycle - 1) % 40;
    if (x.Value - 1 <= pixel && pixel <= x.Value + 1)
    {
        pixels.Add('#');
    }
    else
    {
        pixels.Add('.');
    }

    inst.Cycle();
    if (inst.Complete)
    {
        Console.WriteLine($"After cycle {cycle}, {inst.GetType().Name}({inst.Progress}/{inst.Cost}) completed. Register x is {x.Value}");
        instructions.Remove(inst);
    }
    else
    {
        Console.WriteLine($"After cycle {cycle}, {inst.GetType().Name}({inst.Progress}/{inst.Cost}) is progressing. Register x is {x.Value}");
    }

    cycle++;
}

for (var i = 0; i < 40*6; i++)
{
    if (i % 40 == 0) Console.WriteLine();
    Console.Write(pixels[i]);
}

class Register
{
    public int Value { get; set; }
}

interface Instruction
{
    int Cost { get; }

    int Progress { get; }

    bool Complete { get; }

    void Cycle();
}

class NoopInstruction : Instruction
{
    private int _cycle = 0;

    public NoopInstruction()
    {
        Cost = 1;
    }

    public int Cost { get; }

    public int Progress { get => _cycle; }

    public bool Complete { get => _cycle == Cost; }


    public void Cycle() => _cycle++;
}

class AddXInstruction : Instruction
{
    private Register _x;
    private int _value;
    private int _cycle = 0;

    public AddXInstruction(Register x, int value)
    {
        _x = x;
        _value = value;
        Cost = 2;
    }

    public int Cost { get; }

    public int Progress { get => _cycle; }

    public bool Complete { get => _cycle == Cost; }

    public void Cycle()
    {
        _cycle++;
        if (Complete)
        {
            _x.Value += _value;
        }
    }
}