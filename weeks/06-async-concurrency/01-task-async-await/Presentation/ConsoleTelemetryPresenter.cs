using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Presentation;

public sealed class ConsoleTelemetryPresenter
{
    public void Show(Telemetry telemetry)
    {
        Console.WriteLine($"Temperature: {telemetry.Temperature}");
        Console.WriteLine($"Battery: {telemetry.BatteryPercent}%");
    }
}