var telemetryService = new TelemetryService();
var telemetryScenario = new TelemetryScenario(telemetryService);
var telemetry = await telemetryScenario.RunAsync();

new ConsoleTelemetryPresenter().Show(telemetry);
