using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Presentation;

public sealed class ConsoleTelemetryPresenter
{
    public void Show(Telemetry telemetry)
    {
        Console.WriteLine($"Temperature: {telemetry.Temperature}");
        Console.WriteLine($"Battery: {telemetry.BatteryPercent}%");
    }

    public async Task GetAndShow(Task<string> task)
    {
        Console.WriteLine("Task status: " + task.Status);
        string result = await task;
        Console.WriteLine("Result: " + result);
    }
}