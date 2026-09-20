public sealed class ExperimentPropagationScenario : IScenario
{
    private readonly IService service = new ExperimentPropagationService();

    public async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        return await service.ImportAsync(source);
    }
}