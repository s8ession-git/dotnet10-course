public sealed class CatalogImportWrappingScenario
{
    private readonly CatalogImportService importService;

    public CatalogImportWrappingScenario(CatalogImportService importService)
    {
        this.importService = importService ?? throw new ArgumentNullException(nameof(importService));
    }

    public async Task<CatalogSnapshot> RunAsync(CatalogSource source)
    {
        return await importService.ImportWithWrappingAsync(source);
    }
}