public sealed class CatalogImportService
{
    public async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        return await FetchAsync(source);
    }

    public async Task<CatalogSnapshot> ImportWithWrappingAsync(CatalogSource source)
    {
        try
        {
            return await FetchAsync(source);
        }
        catch (CatalogSourceException exception)
        {
            throw new CatalogImportException("Catalog import failed.", exception);
        }
    }

    private static async Task<CatalogSnapshot> FetchAsync(CatalogSource source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (source.DelayMs > 0) await Task.Delay(source.DelayMs);
        if (source.ShouldFail) throw new CatalogSourceException("Catalog snapshot failed.");

        return new CatalogSnapshot(source.SourceName, source.RecordsCount);
    }
}