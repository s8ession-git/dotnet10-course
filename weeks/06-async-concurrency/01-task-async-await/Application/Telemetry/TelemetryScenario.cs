public sealed class TelemetryScenario
{
    private readonly ITelemetryService telemetryService;

    public TelemetryScenario(ITelemetryService telemetryService)
    {
        this.telemetryService = telemetryService
            ?? throw new ArgumentNullException(nameof(telemetryService));
    }

    public async Task<Telemetry> RunAsync()
    {
        var spacecraft = new Spacecraft(1, "Aurora");
        return await telemetryService.ReceiveTelemetryAsync(spacecraft);
    }
}