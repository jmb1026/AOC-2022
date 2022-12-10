var lines = File.ReadAllLines("input.txt");

var fs = new FS();
foreach(var line in lines)
{
    bool isCommand = true;
    if (!line.Contains("$"))
    {
        isCommand = false;
    }

    if (isCommand)
    {
        if (line[2] == 'c' && line[3] == 'd')
        {
            var where = line.Substring(5);
            if (where == "/")
            {
                fs.AddRoot();
            }
            else if (where == "..")
            {
                fs.MoveToParent();
            }
            else
            {
                fs.MoveToDirNamed(where);
            } 
        }
        else if (line[2] == 'l' && line[3] == 's')
        {
            // Nothing to do
        }
    }
    else
    {
        var item = line.Split(" ");
        if (item[0] == "dir")
        {
            Console.WriteLine($"Adding Dir {item[1]} to {fs.CurrentName}");
            fs.AddDir(item[1]);
        }
        else
        {
            int size = int.Parse(item[0]);
            Console.WriteLine($"Adding File {item[1]}({size}) to {fs.CurrentName}");
            fs.AddFile(item[1], size);
        }
    }
}

const int totalSpace = 70000000;
const int requiredSpace = 30000000;
var directories = fs.DirectoryWalk();

var root = directories.First();
int unusedSpace = totalSpace - root.Size;

Console.WriteLine($"Unused Space: {unusedSpace}");

DirNode toDelete = root;
int minOverflow = totalSpace;

for(var i = 1; i < directories.Count(); i++)
{
    var node = directories.ElementAt(i);

    int space = unusedSpace + node.Size;
    if (space > requiredSpace)
    {
        int overflow = space - requiredSpace;
        if (overflow < minOverflow)
        {
            minOverflow = overflow;
            toDelete = node;
        }
    }
}

Console.WriteLine($"Deleting {toDelete.Name} would free up {toDelete.Size} bytes giving unused space of {unusedSpace+toDelete.Size} bytes");
