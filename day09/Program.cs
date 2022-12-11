var lines = File.ReadAllLines("input.txt");

var tailGrid = new int[256, 256];

var knots = new List<Knot>(10);
for (var i = 0; i < 10; i++)
{
    if (i == 0)
    {
        knots.Add(new Knot(new Pos(127, 127)));
    }
    else
    {
        knots.Add(new Knot(new Pos(127, 127), knots[i-1]));
    }
}

foreach(var line in lines)
{
    var motion = line.Split(" ");
    var dir = motion[0][0];
    var steps = int.Parse(motion[1]);

    Console.WriteLine($"{dir}{steps}");
    for (int i = 0; i < steps; i++)
    {
        knots[0].Move(dir);
        for (int knotIndex = 1; knotIndex < knots.Count(); knotIndex++)
        {
            knots[knotIndex].FollowLeader();
        }

        RecordTailPos();
    }
}

var tailCount = GetTailCount();
Console.WriteLine($"Unique Tail Positions: {tailCount}");

void RecordTailPos()
{
    var tailPos = knots.Last().P;
    tailGrid[tailPos.X, tailPos.Y] = 1;
}

int GetTailCount()
{
    int count = 0;
    for (int i = 0; i < 256; i++)
    {
        for (int j = 0; j < 256; j++)
        {
            count += tailGrid[i, j];
        }
    }

    return count;
}

class Knot
{
    private Knot? _leader;

    public Knot(Pos p, Knot? leader = null)
    {
        P = p;

        _leader = leader;
    }

    public Pos P { get; private set; }

    public void FollowLeader()
    {
        if (_leader == null)
            throw new Exception("No leader to follow!");

        if (P.IsAdjacent(_leader.P)) return;

        // Left/Right Correction
        if (P.Y == _leader.P.Y)
        {
            var L = P.MoveLeft();
            if (L.IsAdjacent(_leader.P))
            {
                P = L;
                return;
            }

            var R = P.MoveRight();
            if (R.IsAdjacent(_leader.P))
            {
                P = R;
                return;
            }
        }
        
        // Up/Down Correction
        if (P.X == _leader.P.X)
        {
            var U = P.MoveUp();
            if (U.IsAdjacent(_leader.P))
            {
                P = U;
                return;
            }

            var D = P.MoveDown();
            if (D.IsAdjacent(_leader.P))
            {
                P = D;
                return;
            }
        }

        // Diagonal Correction
        {
            var NE = P.MoveUp().MoveRight();
            if (NE.IsAdjacent(_leader.P))
            {
                P = NE;
                return;
            }
        }

        {
            var SE = P.MoveDown().MoveRight();
            if (SE.IsAdjacent(_leader.P))
            {
                P = SE;
                return;
            }
        }

        {
            var SW = P.MoveDown().MoveLeft();
            if (SW.IsAdjacent(_leader.P))
            {
                P = SW;
                return;
            }
        }

        {
            var NW = P.MoveUp().MoveLeft();
            if (NW.IsAdjacent(_leader.P))
            {
                P = NW;
                return;
            }
        }
    }

    public void Move(char dir)
    {
        if (dir == 'L') P = P.MoveLeft();
        else if (dir == 'R') P = P.MoveRight();
        else if (dir == 'U') P = P.MoveUp();
        else if (dir == 'D') P = P.MoveDown();
    }
}

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