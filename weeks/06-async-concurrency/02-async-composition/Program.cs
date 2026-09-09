using System.Diagnostics;

internal static class Program
{
	private static async Task Main()
    {
        var stopwatch = Stopwatch.StartNew();
        string result = await RunScenario.RunAsync("2.4.1");
        Console.WriteLine(result);
        stopwatch.Stop();
        Console.WriteLine($"Total execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}