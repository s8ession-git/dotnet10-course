public interface ITelemetryService
{
    Task<Telemetry> ReceiveTelemetryAsync(Spacecraft spacecraft);
}