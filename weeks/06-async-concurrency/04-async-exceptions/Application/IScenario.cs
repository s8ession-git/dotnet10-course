public interface IScenario
{
    Task<CatalogSnapshot> ImportAsync(CatalogSource source);
}