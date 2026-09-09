using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class StartThenAwaitExperiment
{
    public static async Task<Telemetry> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft)
    {
        Task<Telemetry> telemetryTask =
            service.ReceiveTelemetryAsync(spacecraft);

        Console.WriteLine("Telemetry request was started.");
        Console.WriteLine("Control center is doing other work.");

        Telemetry telemetry = await telemetryTask;

        return telemetry;
    }
}