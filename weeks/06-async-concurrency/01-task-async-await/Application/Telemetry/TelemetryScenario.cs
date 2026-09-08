using DotnetCourse.Week06.AsyncAwait.Domain;
using DomainTelemetry = DotnetCourse.Week06.AsyncAwait.Domain.Telemetry;

namespace DotnetCourse.Week06.AsyncAwait.Application.Telemetry;

public sealed class TelemetryScenario
{
    private readonly ITelemetryService telemetryService;

    public TelemetryScenario(ITelemetryService telemetryService)
    {
        this.telemetryService = telemetryService
            ?? throw new ArgumentNullException(nameof(telemetryService));
    }

    public async Task<DomainTelemetry> RunAsync(Spacecraft spacecraft)
    {
        return await telemetryService.ReceiveTelemetryAsync(spacecraft);
    }
}