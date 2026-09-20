public interface ICatalogSourceClient
{
    Task<CatalogSnapshot> FetchAsync(CatalogSource source);
}