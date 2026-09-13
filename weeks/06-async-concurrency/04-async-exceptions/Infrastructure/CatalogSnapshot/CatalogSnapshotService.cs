public sealed class CatalogSnapshotService : ICatalogSnapshotService
{
    public async Task<CatalogSnapshot> GetSnapshotAsync(CatalogSource source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (source.DelayMs > 0) await Task.Delay(source.DelayMs);
        if (source.ShouldFail) throw new CatalogSourceException("Catalog snapshot failed.");

        int recordsCount = Random.Shared.Next(1, 1000);

        CatalogSnapshot catalogSnapshot = new(source.SourceName, recordsCount);
        return catalogSnapshot;
    }

}