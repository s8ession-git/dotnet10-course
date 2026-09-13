public interface ICatalogSnapshotService
{
    Task<CatalogSnapshot> GetSnapshotAsync(CatalogSource source);
}