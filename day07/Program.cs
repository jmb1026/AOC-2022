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

var directories = fs.DirectoryWalk();
Console.WriteLine($"Total Size of Directories <= 100000: {directories.Sum(d => d.Size)}");

