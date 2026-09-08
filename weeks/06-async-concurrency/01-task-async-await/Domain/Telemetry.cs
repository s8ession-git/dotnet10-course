namespace DotnetCourse.Week06.AsyncAwait.Domain;

public sealed record Telemetry(
    int SpacecraftId,
    double Temperature,
    double BatteryPercent);