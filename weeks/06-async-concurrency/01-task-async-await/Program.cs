using System.Diagnostics;
using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;
using DotnetCourse.Week06.AsyncAwait.Experiments;
using DotnetCourse.Week06.AsyncAwait.Infrastructure.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Presentation;

namespace DotnetCourse.Week06.AsyncAwait;

internal static class Program
{
	private static async Task Main()
	{
		var stopwatch = Stopwatch.StartNew();

		var telemetryService = new TelemetryService();

		var spacecraft = new Spacecraft(1, "Apollo 11");

		Telemetry telemetry = telemetryService.ReceiveTelemetryAsync(spacecraft).Result;

		stopwatch.Stop();
		Console.WriteLine($"Total execution time: {stopwatch.ElapsedMilliseconds} ms");
	}

	private static Task<Telemetry> RunRegularAwaitExperimentAsync(
		ITelemetryService service, Spacecraft spacecraft) =>
		RegularAwaitExperiment.RunAsync(service, spacecraft);

	private static Task<Telemetry> RunStartThenAwaitExperimentAsync(
		ITelemetryService service, Spacecraft spacecraft) => 
		StartThenAwaitExperiment.RunAsync(service, spacecraft);

	private static Task<Telemetry> RunSequentialAwaitExperimentAsync(
		ITelemetryService service, Spacecraft spacecraft1, Spacecraft spacecraft2) => 
		SequentialAwaitExperiment.RunAsync(service, spacecraft1, spacecraft2);

	private static Task<Telemetry> RunTaskStateExperimentAsync(
		ITelemetryService service, Spacecraft spacecraft) => 
		TaskStateExperiment.RunAsync(service, spacecraft);

	private static Task<string> RunTaskFromResultExperimentAsync(
		ITelemetryService service) => 
		TaskFromResultExperiment.RunAsync(service);

	private static Task<Telemetry> RunBlockingVsAsyncExperimentAsync(
		ITelemetryService service, Spacecraft spacecraft) => 
		BlockingVsAsyncExperiment.RunAsync(service, spacecraft);

	private static Task<Telemetry> RunBuildTelemetryReportAsync(
		ITelemetryService service, Spacecraft spacecraft) => 
		BuildTelemetryReportAsync.RunAsync(service, spacecraft);

}
