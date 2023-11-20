var lines = File.ReadAllLines("input.txt");

var height = lines.Count();
var width = lines[0].Count();

var grid = new Grid(width, height);

int y = 0;

foreach(var line in lines)
{
    for (int x = 0; x < line.Length; x++)
    {
        var c = new Cell(x, y, line[x]);

        grid[x, y] = c;
        if (line[x] == 'E') grid.Target = c;
    }

    y++;
}

var startingPoints = grid.StartingPoints;
int minSteps = int.MaxValue;

foreach(var start in startingPoints)
{
    grid.Reset();
    var steps = FindPath(grid, start);
    Console.WriteLine($"Starting at {start}, Steps: {steps}");
    minSteps = Math.Min(minSteps, steps);
}

Console.WriteLine($"Steps: {minSteps}");

int FindPath(Grid grid, Cell source)
{
    if (grid.Target == null)
        throw new Exception();

    var queue = new Queue<Cell>();
    queue.Enqueue(source);

    var current = queue.Dequeue();
    current.Steps = 0;

    while(current != grid.Target)
    {
        // Console.WriteLine($"Visiting {current}");
        current.Visited = true;

        var neighbors = grid.GetNeighborsOf(current).Where(c => grid.IsReachable(current, c) && !c.Visited);
        foreach(var n in neighbors)
        {
            if (!queue.Contains(n) && !n.Visited)
            {
                n.Steps = current.Steps + 1;
                queue.Enqueue(n);
            }
        }

        if (queue.Any())
        {
            current = queue.Dequeue();
        }
        else if (current != grid.Target)
        {
            return int.MaxValue;
        }
    }

    return current.Steps;
}

class Grid
{
    private Cell[] _cells;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;

        _cells = new Cell[Width * Height];
    }

    public int Width { get; }
    public int Height { get; }

    public Cell this[int x, int y] 
    { 
        get => GetCell(x, y); 
        set => SetCell(x, y, value); 
    }

    public Cell? Target { get; set; }

    public IEnumerable<Cell> GetNeighborsOf(Cell c)
    {
        int x = c.X;
        int y = c.Y;

        var result = new List<Cell>();

        if (x > 0) result.Add(GetCell(x-1, y));
        if (x < Width-1) result.Add(GetCell(x+1, y));

        if (y > 0) result.Add(GetCell(x, y-1));
        if (y < Height-1) result.Add(GetCell(x, y+1));

        return result;
    }

    public bool IsReachable(Cell source, Cell dest)
    {
        int sourceElevation = (int)source.Elevation;
        int destElevation = (int)dest.Elevation;

        bool result = destElevation <= (sourceElevation + 1);
        return result;
    }

    public IEnumerable<Cell> StartingPoints { get => _cells.Where(c => c.Elevation == 'a'); } 

    public void Reset()
    {
        foreach(var c in _cells)
        {
            c.Steps = int.MaxValue;
            c.Visited = false;
        }
    }

    private Cell GetCell(int x, int y)
    {
        return _cells[y * Width + x];
    }

    private void SetCell(int x, int y, Cell cell)
    {
        _cells[y * Width + x] = cell;
    }
}

class Cell
{
    private char _elevation;

    public Cell(int x, int y, char elevation)
    {
        X = x;
        Y = y;
        _elevation = elevation;
        Visited = false;
        Steps = int.MaxValue;
    }

    public int X { get; }
    public int Y { get; }

    public char Elevation 
    { 
        get
        {
            if (_elevation == 'S')
            {
                return 'a';
            }

            if (_elevation == 'E')
            {
                return 'z';
            }

            return _elevation;    
        }
    }

    public bool Visited { get; set; }

    public int Steps { get; set; }

    public override string ToString()
    {
        return $"({X}, {Y}) {_elevation}";
    }
}
