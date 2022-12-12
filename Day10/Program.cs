var input = File.ReadAllLines(@"Input.txt");
var cycle = 1;
var x = 1;
var totalSignalStrength = 0;
var spritePos = 0;
const int cols = 40;
const int rows = 6;
var crt = new char[rows, cols];
Part1();
cycle = 1;
x = 1;
spritePos = 0;
Part2();

void Part1()
{
    Process(true);
    Console.WriteLine($"Sum of signal strengths: {totalSignalStrength}"); // 14040
    Console.WriteLine();
}

void Part2()
{
    Process(false);
    PrintCrt(); // ZGCJZJFL
}
void Process(bool part1)
{
    foreach (var line in input)
    {
        if (line.StartsWith("noop"))
        {
            if (part1)
            {
                CalculateSignalStrength();
            }
            else
            {
                UpdateCrt();
            }

            cycle++;
        }
        else if (line.StartsWith("addx"))
        {
            var v = int.Parse(line.Split(" ")[1]);
            for (var i = 0; i < 2; i++)
            {
                if (part1)
                {
                    CalculateSignalStrength();
                }
                else
                {
                    UpdateCrt();
                }

                cycle++;
                if (i == 1)
                {
                    x += v;
                    if (!part1)
                    {
                        spritePos = x - 1;
                    }
                }
            }
        }
    }
}


void CalculateSignalStrength()
{
    if ((cycle - 20) % 40 == 0)
    {
        Console.WriteLine($"Value of X during cycle {cycle}: {x}");
        totalSignalStrength += cycle * x;
    }
}

void UpdateCrt()
{
    var index = (cycle - 1) % cols;
    crt[(cycle - 1) / cols, index] = index >= spritePos && index <= spritePos + 2 ? '#' : '.';
}

void PrintCrt()
{
    for (var i = 0; i < crt.GetLength(0); i++)
    {
        for (var j = 0; j < crt.GetLength(1); j++)
        {
            Console.Write(crt[i, j]);
        }
        Console.WriteLine();
    }
}