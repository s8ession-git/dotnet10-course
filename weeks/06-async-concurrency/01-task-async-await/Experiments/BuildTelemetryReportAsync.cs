using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class BuildTelemetryReportAsync
{
    public static async Task<Telemetry> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft
    )
    {
        Telemetry telemetry = await service.ReceiveTelemetryAsync(spacecraft);
        Console.WriteLine($"Telemetry: {telemetry}");
        return telemetry;
    }
}