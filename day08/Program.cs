var lines = File.ReadAllLines("input.txt");

int rowCount = lines.Count();
int colCount = lines[0].Length;
var grid = new int[rowCount, colCount];

for(var i = 0; i < rowCount; i++)
{
    var line = lines[i];
    for(var j = 0; j < colCount; j++)
    {
        grid[i, j] = int.Parse(line[j].ToString());
    }
}

int visibleTrees = 0;
for(var i = 0; i < rowCount; i++)
{
    if (i == 0 || i == rowCount-1) 
    {
        visibleTrees += colCount;
    }
    else
    {
        for(var j = 0; j < colCount; j++)
        {
            if (j == 0 || j == colCount-1)
            {
                visibleTrees++;
            }
            else
            {
                visibleTrees += IsVisible(i, j);
            }
        }
    }
}

Console.WriteLine($"Visible Trees: {visibleTrees}");

int IsVisible(int row, int col)
{
    int height = grid[row, col];

    bool up = true;
    bool down = true;
    bool left = true;
    bool right = true;

    // Up
    for (int i = row - 1; i >= 0; i--)
    {
        if (grid[i, col] >= height)
        {
            up = false;
            break;
        }
    }

    // Down
    for (int i = row + 1; i < rowCount; i++)
    {
        if (grid[i, col] >= height)
        {
            down = false;
            break;
        }
    }

    // Left
    for (int j = col - 1; j >= 0; j--)
    {
        if (grid[row, j] >= height)
        {
            left = false;
            break;
        }
    }

    // Right
    for (int j = col + 1; j < colCount; j++)
    {
        if (grid[row, j] >= height)
        {
            right = false;
            break;
        }
    }

    if (up || down || left || right)
    {
        return 1;
    }

    return 0;
}
