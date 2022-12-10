enum NodeType
{
    File,
    Dir
}

interface Node
{
    string Name { get; }
    int Size { get; }

    Node? Parent { get; }

    bool isDir();
    bool isFile();
}

abstract class BaseNode : Node
{
    private NodeType _type;
    private string _name;
    private List<Node> _children = new();
    private Node? _parent = null;

    protected BaseNode(NodeType type, string name, Node? parent)
    {
        _type = type;
        _name = name;
        _parent = parent;
    }

    public string Name { get => _name; }
    public abstract int Size { get; }

    public Node? Parent { get => _parent; }

    public bool isDir() => _type == NodeType.Dir;
    public bool isFile() => _type == NodeType.File;
}

class FileNode : BaseNode
{
    int _size = 0;

    public FileNode(string name, int size, Node parent): base(NodeType.File, name, parent)
    {
        _size = size;
    }

    public override int Size { get => _size; }
}

class DirNode : BaseNode
{
    private List<Node> _children = new();

    public DirNode(string name, Node? parent = null) : base(NodeType.Dir, name, parent)
    {
    }

    public override int Size { get => _children.Sum(c => c.Size); }

    public IEnumerable<Node> Children { get => _children; }

    public void AddChild(Node child)
    {
        _children.Add(child);
    }
}

class FS
{
    private DirNode? _root = null;
    private DirNode? _current = null;

    public void AddRoot()
    {
        _root = new DirNode("/");
        _current = _root;
    }

    public string CurrentName => (_current is not null) ? _current.Name : string.Empty;

    public void AddDir(string name)
    {
        if (_current == null) 
            throw new InvalidOperationException("No Current Directory");

        _current.AddChild(new DirNode(name, _current));
    }

    public void AddFile(string name, int size)
    {
        if (_current == null) 
            throw new InvalidOperationException("No Current Directory");

        _current.AddChild(new FileNode(name, size, _current));
    }

    public void MoveToDirNamed(string name)
    {
        if (_current == null) 
            throw new InvalidOperationException("No Current Directory");

        var child = _current.Children.Where(c => c.Name == name && c.isDir());
        if (child.Count() != 1)
            throw new InvalidOperationException($"{child.Count()} directories named {name}");

        _current = (DirNode)child.First();
    }

    public void MoveToParent()
    {
        if (_current == null) 
            throw new InvalidOperationException("No Current Directory");

        if (_current.Parent == null)
            throw new InvalidOperationException("Already at root directory");

        _current = (DirNode)_current.Parent;
    }

    public void MoveToRoot()
    {
        if (_root == null)
            throw new InvalidOperationException("No root");

        _current = _root;
    }

    public IEnumerable<DirNode> DirectoryWalk()
    {
        List<DirNode> result = new();

        if (_root == null)
            throw new InvalidOperationException("No root");

        Queue<DirNode> toProcess = new();
        toProcess.Enqueue(_root);

        while(toProcess.Any())
        {
            var node = toProcess.Dequeue();
            if (node.Size <= 100000)
            {
                result.Add(node);
            }

            foreach(var child in node.Children)
            {
                if (child.isDir())
                {
                    toProcess.Enqueue((DirNode)child);
                }
            }
        }

        return result;
    }
}
