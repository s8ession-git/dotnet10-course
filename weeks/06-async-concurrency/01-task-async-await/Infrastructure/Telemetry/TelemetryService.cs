public sealed class TelemetryService : ITelemetryService
{
    public async Task<Telemetry> ReceiveTelemetryAsync(Spacecraft spacecraft)
    {
        if (spacecraft is null)
            throw new ArgumentNullException(nameof(spacecraft));

        await Task.Delay(1000);

        var temperature = 15 + spacecraft.Id * 2.5;
        var batteryPercent = 100 - spacecraft.Id * 7;

        return new Telemetry(
            spacecraft.Id,
            temperature,
            batteryPercent);
    }
}