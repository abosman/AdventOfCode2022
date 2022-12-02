var shapes = new Dictionary<string, string>
{
    { "A", "rock" },
    { "B", "paper" },
    { "C", "scissors" },
    { "X", "rock" },
    { "Y", "paper" },
    { "Z", "scissors" },

};

var shapes2 = new Dictionary<string, string>
{
    { "A", "rock" },
    { "B", "paper" },
    { "C", "scissors" },
};

var result = new Dictionary<string, string>
{
    { "X", "lose" },
    { "Y", "draw" },
    { "Z", "win" },
};

var input = File.ReadAllLines(@"Input.txt");
var gameRounds = input.Select(line => line.Split(" ")).Select(parts => new Tuple<string, string>(shapes[parts[0]], shapes[parts[1]])).ToList();


Part1();

gameRounds = input.Select(line => line.Split(" ")).Select(parts => new Tuple<string, string>(shapes2[parts[0]], result[parts[1]])).ToList();

Part2();

void Part1()
{
    var totalScore = gameRounds.Sum(round => ShapeScore(round.Item2) + CalculateRoundScore(round.Item2, round.Item1));
    Console.WriteLine($"Total score: {totalScore}"); // 14264
}

void Part2()
{
    var totalScore = (from round in gameRounds let myShape = 
        DetermineMyShape(round.Item1, round.Item2) select CalculateRoundScore(myShape, round.Item1) + 
                                                          ShapeScore(myShape)).Sum();
    Console.WriteLine($"Total score: {totalScore}"); // 12382
}

int CalculateRoundScore(string myShape, string elvesShape)
{
    if (myShape.Equals(elvesShape))
    {
        return 3;
    }

    switch (myShape)
    {
        case "rock" when elvesShape.Equals("scissors"):
        case "scissors" when elvesShape.Equals("paper"):
        case "paper" when elvesShape.Equals("rock"):
            return 6;
        case "rock" when elvesShape.Equals("paper"):
        case "scissors" when elvesShape.Equals("rock"):
        case "paper" when elvesShape.Equals("scissors"):
            return 0;
    }

    return 0;
}

int ShapeScore(string myShape) => myShape == "rock" ? 1 : myShape == "paper" ? 2 : 3;

string DetermineMyShape(string elvesShape, string outcome)
{
    switch (outcome)
    {
        case "draw":
            return elvesShape;
        case "lose" when elvesShape=="rock":
        case "win" when elvesShape == "paper":
            return "scissors";
        case "lose" when elvesShape == "scissors":
        case "win" when elvesShape == "rock":
            return "paper";
        case "lose" when elvesShape == "paper":
        case "win" when elvesShape == "scissors":
            return "rock";
    }

    return "";
}

