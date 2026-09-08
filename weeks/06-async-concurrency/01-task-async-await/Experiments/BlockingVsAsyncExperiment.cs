namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class BlockingVsAsyncExperiment
{
    public static async Task RunAsync()
    {
        Console.WriteLine("Blocking operation started");
        Thread.Sleep(300);
        Console.WriteLine("Blocking operation completed");

        Console.WriteLine("Async operation started");
        await Task.Delay(300);
        Console.WriteLine("Async operation completed");
    }
}
