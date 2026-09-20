public sealed class ExperimentWrappingScenario : IScenario
{
    private readonly IService service = new ExperimentWrappingService();

    public async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        return await service.ImportAsync(source);
    }
}