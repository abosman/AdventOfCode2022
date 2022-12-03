using System;

var input = File.ReadAllLines(@"Input.txt");

Part1();
Part2();

void Part1()
{
    int totalPriorities = 0;
    foreach (var line in input)
    {
        var itemsInComponent1 = line.Take(line.Length / 2);
        var itemsInComponent2 = line.TakeLast(line.Length / 2);
        var duplicateItem = itemsInComponent1.Intersect(itemsInComponent2).First();
        var priority = Prioritize(duplicateItem);
        Console.WriteLine($"Duplicate item {duplicateItem} - priority {priority}");
        totalPriorities += priority;
    }
    Console.WriteLine($"Total priorities: {totalPriorities}"); // 8123
}

void Part2()
{
    int totalPriorities = 0;
    for (int i = 0; i < input.Length; i+=3)
    {
        var rucksack1 = input[i];
        var rucksack2 = input[i+1];
        var rucksack3 = input[i+2];
        var duplicateItem = rucksack1.Intersect(rucksack2).Intersect(rucksack3).First();
        var priority = Prioritize(duplicateItem);
        Console.WriteLine($"Duplicate item {duplicateItem} - priority {priority}");
        totalPriorities += priority;
    }
    Console.WriteLine($"Total priorities: {totalPriorities}"); // 2620
}

int Prioritize(char c)
{
    return char.IsLower(c) ? c - 96 : c - 38;
}
