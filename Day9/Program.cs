var input = File.ReadAllLines(@"Input.txt");
var tailVisited = new HashSet<Tuple<int, int>>();

var head = new Tuple<int,int>(0,0);
var tail = new Tuple<int,int>(0,0);

Part1();
tailVisited = new HashSet<Tuple<int, int>>();
var knots = new List<Tuple<int, int>>
{
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0),
    new(0, 0)
};


Part2();

void Part1()
{
    tailVisited.Add(tail);
    foreach (var line in input)
    {
        for (int i = 0; i < int.Parse(line.Split(" ")[1]); i++)
        {
            head = line[..1] switch
            {
                "L" => new Tuple<int, int>(head.Item1 - 1, head.Item2),
                "R" => new Tuple<int, int>(head.Item1 + 1, head.Item2),
                "U" => new Tuple<int, int>(head.Item1, head.Item2 + 1),
                "D" => new Tuple<int, int>(head.Item1, head.Item2 - 1),
                _ => head
            };
            tail = MoveTail(head, tail);
            tailVisited.Add(tail);
        }
        //Console.WriteLine($"After move: ({line})");
        //Console.WriteLine($"Position of head: ({head.Item1},{head.Item2}");
        //Console.WriteLine($"Position of tail: ({tail.Item1},{tail.Item2}");
        //Console.WriteLine("=================");
        //Console.WriteLine("Positions tail visited");
        //foreach (var tuple in tailVisited)
        //{
        //    Console.WriteLine($"{tuple.Item1},{tuple.Item2}");
        //}
        //Console.WriteLine("=================");
    }

    Console.WriteLine($"Number of positions of tail: {tailVisited.Count}"); // 6037
}

void Part2()
{
    tailVisited.Add(knots[9]);
    foreach (var line in input)
    {
        for (int i = 0; i < int.Parse(line.Split(" ")[1]); i++)
        {
            knots[0] = line[..1] switch
            {
                "L" => new Tuple<int, int>(knots[0].Item1 - 1, knots[0].Item2),
                "R" => new Tuple<int, int>(knots[0].Item1 + 1, knots[0].Item2),
                "U" => new Tuple<int, int>(knots[0].Item1, knots[0].Item2 + 1),
                "D" => new Tuple<int, int>(knots[0].Item1, knots[0].Item2 - 1),
                _ => knots[0]
            };
            for (int j = 1; j < knots.Count; j++)
            {
                knots[j] = MoveTail(knots[j - 1], knots[j]);
                if (j == knots.Count - 1)
                {
                    tailVisited.Add(knots[j]);
                }
            }

        }
        //Console.WriteLine($"After move: ({line})");

        //for (int i = 15; i >= -5; i--)
        //{
        //    for (int j = -11; j <= 15; j++)
        //    {
        //        var index = knots.FindIndex(k => k.Item1 == j && k.Item2 == i);
        //        if (index == -1)
        //        {
        //            Console.Write(".");
        //        }
        //        else
        //        {
        //            Console.Write(index == 0 ? "H" : index);
        //        }
        //    }
        //    Console.WriteLine();
        //}
        //Console.WriteLine("=================");
        //Console.WriteLine("Positions tail visited");
        //foreach (var tuple in tailVisited)
        //{
        //    Console.WriteLine($"{tuple.Item1},{tuple.Item2}");
        //}
        //Console.WriteLine("=================");
    }

    Console.WriteLine($"Number of positions of tail: {tailVisited.Count}"); // 2485
}

Tuple<int,int> MoveTail(Tuple<int, int> headItem, Tuple<int, int> tailItem)
{
    if (Math.Abs(headItem.Item1 - tailItem.Item1) > 1 || Math.Abs(headItem.Item2 - tailItem.Item2) > 1)
    {
        if (headItem.Item1 == tailItem.Item1)
        {
            tailItem = new Tuple<int, int>(tailItem.Item1, Math.Min(headItem.Item2, tailItem.Item2) + 1);
        }
        else if (headItem.Item2 == tailItem.Item2)
        {
            tailItem = new Tuple<int, int>(Math.Min(headItem.Item1, tailItem.Item1) + 1, tailItem.Item2);
        }
        else if (headItem.Item1 < tailItem.Item1 && headItem.Item2 < tailItem.Item2) {

            tailItem = new Tuple<int, int>(tailItem.Item1-1, tailItem.Item2 -1);
        }
        else if (headItem.Item1 > tailItem.Item1 && headItem.Item2 < tailItem.Item2)
        {
            tailItem = new Tuple<int, int>(tailItem.Item1 + 1, tailItem.Item2 - 1);
        }
        else if (headItem.Item1 < tailItem.Item1 && headItem.Item2 > tailItem.Item2)
        {
            tailItem = new Tuple<int, int>(tailItem.Item1 - 1, tailItem.Item2 + 1);
        }
        else if (headItem.Item1 > tailItem.Item1 && headItem.Item2 > tailItem.Item2)
        {
            tailItem = new Tuple<int, int>(tailItem.Item1 + 1, tailItem.Item2 + 1);
        }
    }
    return tailItem;
}
