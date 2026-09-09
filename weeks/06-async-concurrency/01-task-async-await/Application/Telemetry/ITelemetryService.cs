using DotnetCourse.Week06.AsyncAwait.Domain;
using DomainTelemetry = DotnetCourse.Week06.AsyncAwait.Domain.Telemetry;

namespace DotnetCourse.Week06.AsyncAwait.Application.Telemetry;

public interface ITelemetryService
{
    Task<DomainTelemetry> ReceiveTelemetryAsync(Spacecraft spacecraft);
    Task<string> GetStationNameAsync();
}