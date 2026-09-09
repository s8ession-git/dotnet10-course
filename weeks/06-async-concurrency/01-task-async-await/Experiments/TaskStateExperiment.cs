using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class TaskStateExperiment
{
    public static async Task<Telemetry> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft
    )
    {
        Task<Telemetry> task = service.ReceiveTelemetryAsync(spacecraft);
        Console.WriteLine($"Immediately after creation: {task.Status}");
        Console.WriteLine($"Completed: {task.IsCompleted}");

        Telemetry telemetry = await task;
        Console.WriteLine($"Immediately after await: {task.Status}");
        Console.WriteLine($"Completed: {task.IsCompleted}");

        return telemetry;
    }
}
