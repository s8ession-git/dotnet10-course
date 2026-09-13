CatalogImportScenario scenario = new(new CatalogImportService());
ImportPresenter presenter = new(scenario);

CatalogSource[] sources =
[
	new CatalogSource("Alpha", 1200, false, 125),
	new CatalogSource("Beta", 600, true, 0),
	new CatalogSource("Gamma", 900, false, 230)
];

await presenter.ShowAsync(sources);

CatalogImportWrappingScenario wrappingScenario = new(new CatalogImportService());

try
{
	await wrappingScenario.RunAsync(new CatalogSource("Beta", 600, true, 0));
}
catch (CatalogImportException exception)
{
	Console.WriteLine(nameof(CatalogImportException));
	Console.WriteLine($"InnerException: {exception.InnerException?.GetType().Name}");
}
