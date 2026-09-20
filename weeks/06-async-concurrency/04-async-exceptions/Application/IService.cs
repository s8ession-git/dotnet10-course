public interface IService
{
    Task<CatalogSnapshot> ImportAsync(CatalogSource source);
}