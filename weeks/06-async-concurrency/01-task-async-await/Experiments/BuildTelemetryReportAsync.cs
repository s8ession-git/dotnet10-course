using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class BuildTelemetryReportAsync
{
    public static async Task<string> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft
    )
    {
        Task<Telemetry> telemetryTask =
            service.ReceiveTelemetryAsync(spacecraft);

        Telemetry telemetry = await telemetryTask;

        string report =
            $"Spacecraft {telemetry.SpacecraftId}: " +
            $"temperature {telemetry.Temperature}, " +
            $"battery {telemetry.BatteryPercent}%";

        return report;
    }
}