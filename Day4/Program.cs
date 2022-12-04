var input = File.ReadAllLines(@"Input.txt");

Part1();
Part2();

void Part1()
{
    var count = 0; 
    foreach (var line in input)
    {
        var parts = line.Split(',');
        var sectionsElf1 = GetAssignedSections(parts[0]).ToList();
        var sectionsElf2 = GetAssignedSections(parts[1]).ToList();
        var intersection = sectionsElf1.Intersect(sectionsElf2).ToList();
        if (intersection.SequenceEqual(sectionsElf1) || intersection.SequenceEqual(sectionsElf2))
        {
            Console.WriteLine($"Line {line} contains overlaps");
            count++;
        }
    }
    Console.WriteLine($"Number of overlaps: {count}"); // 588
}

void Part2()
{
    var count = 0;
    foreach (var line in input)
    {
        var parts = line.Split(',');
        var sectionsElf1 = GetAssignedSections(parts[0]).ToList();
        var sectionsElf2 = GetAssignedSections(parts[1]).ToList();
        var intersection = sectionsElf1.Intersect(sectionsElf2).ToList();
        if (intersection.Any())
        {
            Console.WriteLine($"Line {line} contains overlaps");
            count++;
        }
    }
    Console.WriteLine($"Number of overlaps: {count}"); // 911
}

IEnumerable<int> GetAssignedSections(string section)
{
    var startIndex = int.Parse(section.Split('-')[0]);
    var endIndex = int.Parse(section.Split('-')[1]);
    return Enumerable.Range(startIndex, endIndex-startIndex+1);
}
