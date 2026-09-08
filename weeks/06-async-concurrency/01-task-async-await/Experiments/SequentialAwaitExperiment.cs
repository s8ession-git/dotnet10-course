namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class SequentialAwaitExperiment
{
    public static async Task RunAsync()
    {
        Console.WriteLine("First operation started");
        await Task.Delay(300);
        Console.WriteLine("First operation completed");

        Console.WriteLine("Second operation started");
        await Task.Delay(300);
        Console.WriteLine("Second operation completed");
    }
}
