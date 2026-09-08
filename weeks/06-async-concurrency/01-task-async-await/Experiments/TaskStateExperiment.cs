namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class TaskStateExperiment
{
    public static async Task RunAsync()
    {
        Task task = Task.Delay(300);

        Console.WriteLine($"Immediately after creation: {task.Status}");

        await task;

        Console.WriteLine($"After await: {task.Status}");
    }
}
