using DotnetCourse.Week06.AsyncAwait.Application.Telemetry;
using DotnetCourse.Week06.AsyncAwait.Domain;
using DomainTelemetry = DotnetCourse.Week06.AsyncAwait.Domain.Telemetry;

namespace DotnetCourse.Week06.AsyncAwait.Infrastructure.Telemetry;

public sealed class TelemetryService : ITelemetryService
{
    public async Task<DomainTelemetry> ReceiveTelemetryAsync(Spacecraft spacecraft)
    {
        if (spacecraft is null)
            throw new ArgumentNullException(nameof(spacecraft));

        await Task.Delay(1000);

        var temperature = 15 + spacecraft.Id * 2.5;
        var batteryPercent = 100 - spacecraft.Id * 7;

        return new DomainTelemetry(
            spacecraft.Id,
            temperature,
            batteryPercent);
    }
}