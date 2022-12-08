var input = File.ReadAllLines(@"Input.txt");
var rowCount = input.Length;
var colCount = input.Length;
var map = new int[rowCount, colCount];
for (var i = 0; i < rowCount; i++)
{
    for (int j = 0; j < colCount; j++)
    {
        map[i,j] = int.Parse(input[i][j].ToString());
    }
}

Part1();
Part2();

void Part1()
{
    var visibleTreeCount = 0;
    for (int i = 0; i < rowCount; i++)
    {
        for (int j = 0; j < colCount; j++)
        {
            if (IsTreeVisible(i, j))
            {
                visibleTreeCount++;
            }
        }
    }
    Console.WriteLine($"Number of visible trees:{visibleTreeCount}"); // 1816
}

void Part2()
{
    var scenicScore = 0;
    for (int i = 0; i < rowCount; i++)
    {
        for (int j = 0; j < colCount; j++)
        {
            scenicScore = Math.Max(scenicScore, CalculateScenicScore(i, j));
        }
    }
    Console.WriteLine($"Highest scenic score:{scenicScore}"); // 383520
}


bool IsTreeVisible(int row, int col)
{
    if (row == 0 || row == rowCount - 1 || col == 0 || col == colCount - 1)
    {
        return true;
    }

    if (CheckDirection(row, col, Direction.Left))
    {
        return true;
    }
    if (CheckDirection(row, col, Direction.Right))
    {
        return true;
    }
    if (CheckDirection(row, col, Direction.Top))
    {
        return true;
    }
    if (CheckDirection(row, col, Direction.Bottom))
    {
        return true;
    }

    return false;
}

bool CheckDirection(int row, int col, Direction direction)
{
    int height = map[row, col];

    switch (direction)
    {
        case Direction.Left:
            for (int c = col-1; c >= 0; c--)
            {
                if (map[row, c] >= height)
                {
                    return false;
                }
            }
            return true;
        case Direction.Right:
            for (int c = col + 1; c <= colCount-1; c++)
            {
                if (map[row, c] >= height)
                {
                    return false;
                }
            }
            return true;
        case Direction.Top:
            for (int r = row - 1; r >= 0; r--)
            {
                if (map[r, col] >= height)
                {
                    return false;
                }
            }
            return true;
        case Direction.Bottom:
            for (int r = row + 1; r <= rowCount-1; r++)
            {
                if (map[r, col] >= height)
                {
                    return false;
                }
            }
            return true;
    }

    return false;
}

int CalculateScenicScore(int row, int col)
{
    var score = CalculateScenicScoreDirection(row, col, Direction.Left);
    score *= CalculateScenicScoreDirection(row, col, Direction.Right);
    score *= CalculateScenicScoreDirection(row, col, Direction.Top);
    score *= CalculateScenicScoreDirection(row, col, Direction.Bottom);
    return score;
}

int CalculateScenicScoreDirection(int row, int col, Direction direction)
{
    var height = map[row, col];

    switch (direction)
    {
        case Direction.Left:
            if (col == 0) return 0;
            int count = 0;
            for (int c = col - 1; c >= 0; c--)
            {
                count++;
                if (map[row, c] >= height)
                {
                    return count;
                }
            }
            return count;
        case Direction.Right:
            if (col == colCount -1) return 0;
            count = 0;
            for (int c = col + 1; c <= colCount - 1; c++)
            {
                count++;
                if (map[row, c] >= height)
                {
                    return count;
                }
            }
            return count;
        case Direction.Top:
            if (row == 0) return 0;
            count = 0;
            for (int r = row - 1; r >= 0; r--)
            {
                count++;
                if (map[r, col] >= height)
                {
                    return count;
                }
            }
            return count;
        case Direction.Bottom:
            if (row == rowCount -1) return 0;
            count = 0;
            for (int r = row + 1; r <= rowCount - 1; r++)
            {
                count++;
                if (map[r, col] >= height)
                {
                    return count;
                }
            }
            return count;
    }

    return 0;
}

internal enum Direction
{
    Left = 1,
    Right =2,
    Top = 3,
    Bottom = 4
}