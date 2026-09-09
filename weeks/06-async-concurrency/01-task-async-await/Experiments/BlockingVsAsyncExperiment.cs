using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;


namespace DotnetCourse.Week06.AsyncAwait.Experiments;

public static class BlockingVsAsyncExperiment
{
    public static async Task<Telemetry> RunAsync(
        ITelemetryService service,
        Spacecraft spacecraft
    )
    {
        Task<Telemetry> telemetryTask = service.ReceiveTelemetryAsync(spacecraft);
        Console.WriteLine("Telemetry request was started.");
            
        Console.WriteLine("Blocking operation started");
        Thread.Sleep(1000);
        Console.WriteLine("Blocking operation completed");


        Telemetry telemetry = await telemetryTask;
        Console.WriteLine("Async operation (Telemetry request) completed.");
        return telemetry;
    }
}
