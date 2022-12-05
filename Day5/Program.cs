using System.Text;
using System.Text.RegularExpressions;

//var stacksA = new List<Stack<char>>
//{
//    new(new[] {'Z', 'N'}),
//    new(new[] {'M', 'C', 'D'}),
//    new(new[] {'P' })
//};

var stacksA = new List<Stack<char>>
{
    new(new[] {'D', 'H', 'N', 'Q', 'T', 'W', 'V', 'B'}),
    new(new[] {'D', 'W', 'B'}),
    new(new[] {'T', 'S', 'Q', 'W', 'J', 'C'}),
    new(new[] {'F', 'J', 'R', 'N', 'Z', 'T', 'P'}),
    new(new[] {'G', 'P', 'V', 'J', 'M', 'S', 'T'}),
    new(new[] {'B', 'W', 'F', 'T', 'N'}),
    new(new[] {'B', 'L', 'D', 'Q', 'F', 'H', 'V', 'N'}),
    new(new[] {'H', 'P', 'F', 'R' }),
    new(new[] {'Z', 'S', 'M', 'B', 'L', 'N', 'P', 'H'})
};

//var stacksB = new List<List<char>>
//{
//    new(new[] {'Z', 'N'}),
//    new(new[] {'M', 'C', 'D'}),
//    new(new[] {'P' })
//};

var stacksB = new List<List<char>>
{
    new(new[] {'D', 'H', 'N', 'Q', 'T', 'W', 'V', 'B'}),
    new(new[] {'D', 'W', 'B'}),
    new(new[] {'T', 'S', 'Q', 'W', 'J', 'C'}),
    new(new[] {'F', 'J', 'R', 'N', 'Z', 'T', 'P'}),
    new(new[] {'G', 'P', 'V', 'J', 'M', 'S', 'T'}),
    new(new[] {'B', 'W', 'F', 'T', 'N'}),
    new(new[] {'B', 'L', 'D', 'Q', 'F', 'H', 'V', 'N'}),
    new(new[] {'H', 'P', 'F', 'R' }),
    new(new[] {'Z', 'S', 'M', 'B', 'L', 'N', 'P', 'H'})
};

var input = File.ReadAllLines(@"Input.txt");
var pattern = @"move (\d{1,2}) from (\d) to (\d)";
var r = new Regex(pattern, RegexOptions.IgnoreCase);

Part1();
Part2();

void Part1()
{
    foreach (var line in input)
    {
        var m = r.Match(line);
        var count = int.Parse(m.Groups[1].Value);
        for (var i = 0; i < count; i++)
        {
            var from = int.Parse(m.Groups[2].Value)-1;
            var to = int.Parse(m.Groups[3].Value)-1;
            stacksA[to].Push(stacksA[from].Pop());
        }
    }

    var message = new StringBuilder();
    foreach (var stack in stacksA)
    {
        message.Append(stack.Peek());
    }
    Console.WriteLine($"Message: {message}"); // PSNRGBTFT
}

void Part2()
{
    foreach (var line in input)
    {
        var m = r.Match(line);
        var count = int.Parse(m.Groups[1].Value);
        var from = int.Parse(m.Groups[2].Value) - 1;
        var to = int.Parse(m.Groups[3].Value) - 1;
        var items = stacksB[from].GetRange(stacksB[from].Count-count,count);

        stacksB[to].AddRange(items);
        stacksB[from].RemoveRange(stacksB[from].Count - count, count);
    }

    var message = new StringBuilder();
    foreach (var stack in stacksB)
    {
        message.Append(stack.Last());
    }
    Console.WriteLine($"Message: {message}"); // BNTZFPMMW
}
