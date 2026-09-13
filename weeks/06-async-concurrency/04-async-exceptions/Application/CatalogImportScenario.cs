public sealed class CatalogImportScenario
{
    private readonly CatalogImportService importService;

    public CatalogImportScenario(CatalogImportService importService)
    {
        this.importService = importService ?? throw new ArgumentNullException(nameof(importService));
    }

    public async Task<CatalogSnapshot> RunAsync(CatalogSource source)
    {
        return await importService.ImportAsync(source);
    }
}