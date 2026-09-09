using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class TaskFromResultExperiment
{
    public static Task<string> RunAsync(ITelemetryService service)
    {
        Task<string> stationNameTask = service.GetStationNameAsync();
        Console.WriteLine($"Task status: {stationNameTask.Status}");
        Console.WriteLine($"Completed immediately: {stationNameTask.IsCompleted}");

        return stationNameTask;
    }
}