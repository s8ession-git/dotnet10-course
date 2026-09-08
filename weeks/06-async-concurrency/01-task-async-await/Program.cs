using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;
using DotnetCourse.Week06.AsyncAwait.Infrastructure.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Presentation;

namespace DotnetCourse.Week06.AsyncAwait;

internal static class Program
{
	private static async Task Main()
	{
		var spacecraft = new Spacecraft(1, "Aurora");
		var telemetryService = new TelemetryService();
		var telemetryScenario = new TelemetryScenario(telemetryService);
		var telemetry = await telemetryScenario.RunAsync(spacecraft);

		new ConsoleTelemetryPresenter().Show(telemetry);
	}
}
