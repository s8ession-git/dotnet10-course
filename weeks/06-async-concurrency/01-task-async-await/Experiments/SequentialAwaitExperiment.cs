using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;

namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class SequentialAwaitExperiment
{
    public static async Task<Telemetry> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft1,
        Spacecraft spacecraft2
    )
    {
        Task<Telemetry> telemetryTask1 = service.ReceiveTelemetryAsync(spacecraft1);

        Console.WriteLine("Telemetry request 1 was started.");
        Telemetry telemetry1 = await telemetryTask1;
        Console.WriteLine("Telemetry request 1 was completed.");

        Task<Telemetry> telemetryTask2 = service.ReceiveTelemetryAsync(spacecraft2);
        Console.WriteLine("Telemetry request 2 was started.");
        Telemetry telemetry2 = await telemetryTask2;
        Console.WriteLine("Telemetry request 2 was completed.");

        return telemetry2;
    }
}
