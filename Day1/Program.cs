var input = File.ReadAllLines(@"Input.txt");
var food = new List<List<int>>();
var calories = new List<int>();
foreach (var line in input)
{
    if (!string.IsNullOrEmpty(line)) {
        calories.Add(int.Parse(line));
    } else
    {
        food.Add(calories);
        calories = new List<int>();
    }
}
if (calories.Any())
{
    food.Add(calories);
}


Part1();
Part2();

void Part1()
{
    var maxCalories = food.Select(item => item.Sum()).Max();
    Console.WriteLine($"Max calories carried by an elf: {maxCalories}"); // 69281
}

void Part2()
{
    var totalCalories = food.Select(item => item.Sum()).
        OrderByDescending(i=>i).Take(3).Sum();
    Console.WriteLine($"Total calories carried by top 3 elves: {totalCalories}"); // 201524
}