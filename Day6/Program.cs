var input = File.ReadAllText(@"Input.txt");
var testStream = input.ToCharArray();
const int startOfPacketMarkerSize = 4;
const int startOfMessageMarkerSize = 14; 

Part1();
Part2();

void Part1()
{
    var pos = FindMarkers(startOfPacketMarkerSize);
    Console.WriteLine($"First start of packet marker {pos}"); // 1343
}

void Part2()
{
    var pos = FindMarkers(startOfMessageMarkerSize);
    Console.WriteLine($"First start of message marker {pos}"); // 2193
}

int FindMarkers(int markerSize)
{
    var pos = 0;
    while (true)
    {
        var buffer = testStream[pos..(pos + markerSize)];
        if (buffer.Distinct().Count() == markerSize)
        {
            return pos + markerSize;
        }
        pos++;
    }
}
