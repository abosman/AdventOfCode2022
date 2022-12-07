var input = File.ReadAllLines(@"Input.txt");
var root = new FileObject { Name = "/", Type = FileObjectType.Directory , Parent = null};
var currentFolder = root;
foreach (var line in input)
{
    if (line.StartsWith("$"))
    {
        if (line.Substring(2, 2) == "cd")
        {
            currentFolder = ChangeDirectory(currentFolder!, line[5..]);
        }
    }
    else
    {
        CreateFileObject(currentFolder!, line.Split(" "));
    }
}
Part1();
Part2();

void Part1()
{
    PrintFolders(root, 0);
    Console.WriteLine($"Sum of the total sizes of directories at most 100000: {SummarizeFolderSize(root)}"); // 1315285
}

void Part2()
{
    const int totalDiskSpaceAvailable = 70000000;
    const int spaceNeededForUpdate = 30000000;
    var unusedSpace = totalDiskSpaceAvailable - root.Size;
    var extraSpaceNeeded = spaceNeededForUpdate - unusedSpace;
    Console.WriteLine($"Best directory to delete: {BestDirectoryToDelete(root,extraSpaceNeeded,root).Size}"); // 9847279
}

FileObject? ChangeDirectory(FileObject currentDirectory, string argument)
{
    return argument switch
    {
        "/" => root,
        ".." => currentDirectory.Parent,
        _ => currentDirectory.Children.First(c => c.Name == argument)
    };
}

void CreateFileObject(FileObject currentDirectory, string[] arguments)
{
    var fileObject = arguments[0] == "dir" ? new FileObject{Name = arguments[1], Type = FileObjectType.Directory, 
        Parent = currentDirectory} : new FileObject{Name = arguments[1], Type = FileObjectType.File, Size = int.Parse(arguments[0]),Parent = currentDirectory};
    currentDirectory.Children.Add(fileObject);
}

void PrintFolders(FileObject fileObject, int level)
{
    Console.WriteLine(new string(' ', level * 2) + fileObject);
    foreach (var child in fileObject.Children)
    {
        PrintFolders(child,level+1);
    }
}

FileObject BestDirectoryToDelete(FileObject currentFileObject, int spaceNeeded, FileObject bestFileObject)
{
    if (currentFileObject.Size > spaceNeeded && currentFileObject.Size < bestFileObject.Size)
    {
        bestFileObject = currentFileObject;
    }
    return currentFileObject.Children.Where(c => c.Type == FileObjectType.Directory).Aggregate(bestFileObject, (current, child) => BestDirectoryToDelete(child, spaceNeeded, current));
}

int SummarizeFolderSize(FileObject fileObject)
{
    var size = 0;
    if (fileObject.Size <= 100000)
    {
        Console.WriteLine(fileObject);
        size += fileObject.Size;
    }

    size += fileObject.Children.Where(c => c.Type == FileObjectType.Directory)
        .Sum(SummarizeFolderSize);
    return size;
}

record FileObject
{
    private int _size;
    public required string Name { get; set; }
    public required FileObjectType Type { get; set; }

    public int Size
    {
        get => Type == FileObjectType.File ? _size : CalculateFolderSize(this);
        set => _size = value;
    }


    public List<FileObject> Children { get; set; } = new();

    public FileObject? Parent { get; set; }

    public int CalculateFolderSize(FileObject fileObject)
    {
        var size = 0;
        foreach (var fileObjectChild in fileObject.Children)
        {
            if (fileObjectChild.Type == FileObjectType.File)
            {
                size += fileObjectChild.Size;
            }
            else
            {
                size += CalculateFolderSize(fileObjectChild);
            }
        }
        return size;
    }

    public override string ToString()
    {
        return $"- {Name} ({Type}, {Size})";
    }
}

enum FileObjectType
{
    Directory = 1,
    File = 2
}
