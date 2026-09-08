public sealed class ConsoleTelemetryPresenter
{
    public void Show(Telemetry telemetry)
    {
        Console.WriteLine($"Temperature: {telemetry.Temperature}");
        Console.WriteLine($"Battery: {telemetry.BatteryPercent}%");
    }
}