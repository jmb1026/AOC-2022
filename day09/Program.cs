var lines = File.ReadAllLines("input.txt");

var grid = new int[256, 256];
var head = new Pos(127, 127);
var tail = new Pos(127, 127);
grid[127, 127] = 1;

foreach(var line in lines)
{
    var motion = line.Split(" ");
    var dir = motion[0];
    var steps = int.Parse(motion[1]);

    for (int i = 0; i < steps; i++)
    {
        var prevHead = head;

        if (dir == "L") head = head.MoveLeft();
        else if (dir == "R") head = head.MoveRight();
        else if (dir == "U") head = head.MoveUp();
        else if (dir == "D") head = head.MoveDown();

        if (!head.IsAdjacent(tail))
        {
            tail = prevHead;
            grid[tail.X, tail.Y] = 1;
        }
    }
}

int count = 0;
for (int i = 0; i < 256; i++)
{
    for (int j = 0; j < 256; j++)
    {
        count += grid[i, j];
    }
}

Console.WriteLine($"Tail visited {count} locations");

struct Pos
{
    public Pos(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public int X { get; }
    public int Y { get; }

    public Pos MoveLeft() => new Pos(X - 1, Y);
    public Pos MoveRight() => new Pos(X + 1, Y);
    public Pos MoveUp() => new Pos(X, Y - 1);
    public Pos MoveDown() => new Pos(X, Y + 1);

    public bool IsAdjacent(Pos other)
    {
        if (X == other.X && Y == other.Y)
        {
            return true;
        }

        // N
        if (X == other.X && Y-1 == other.Y)
        {
            return true;
        }

        // NE
        if (X+1 == other.X && Y-1 == other.Y)
        {
            return true;
        }

        // E
        if (X+1 == other.X && Y == other.Y)
        {
            return true;
        }

        // SE
        if (X+1 == other.X && Y+1 == other.Y)
        {
            return true;
        }
        
        // S
        if (X == other.X && Y+1 == other.Y)
        {
            return true;
        }

        // SW
        if (X-1 == other.X && Y+1 == other.Y)
        {
            return true;
        }

        // W
        if (X-1 == other.X && Y == other.Y)
        {
            return true;
        }

        // NW
        if (X-1 == other.X && Y-1 == other.Y)
        {
            return true;
        }

        return false;
    }
}