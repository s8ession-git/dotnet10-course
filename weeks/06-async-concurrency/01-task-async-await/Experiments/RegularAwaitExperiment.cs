using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class RegularAwaitExperiment
{
    public static async Task<Telemetry> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft)
    {
        Telemetry telemetry =
            await service.ReceiveTelemetryAsync(spacecraft);

        return telemetry;
    }
}