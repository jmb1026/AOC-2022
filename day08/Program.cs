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

int maxScore = 0;
for(var i = 0; i < rowCount; i++)
{
    for(var j = 0; j < colCount; j++)
    {
        var score = CalculateScenicScore(i, j);
        maxScore = Math.Max(maxScore, score);
    }
}

Console.WriteLine($"Max Scenic Score: {maxScore}");

int CalculateScenicScore(int row, int col)
{
    if (row == 0 || row == rowCount-1)
        return 0;

    if (col == 0 || col == colCount-1)
        return 0;

    int height = grid[row, col];

    int up = 0;
    for (int i = row - 1; i >= 0; i--)
    {
        if (grid[i, col] >= height)
        {
            up++;
            break;
        }

        up++;
    }

    int down = 0;
    for (int i = row + 1; i < rowCount; i++)
    {
        if (grid[i, col] >= height)
        {
            down++;
            break;
        }

        down++;
    }

    int left = 0;
    for (int j = col - 1; j >= 0; j--)
    {
        if (grid[row, j] >= height)
        {
            left++;
            break;
        }

        left++;
    }

    int right = 0;
    for (int j = col + 1; j < colCount; j++)
    {
        if (grid[row, j] >= height)
        {
            right++;
            break;
        }

        right++;
    }

    return up * down * left * right;
}

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
