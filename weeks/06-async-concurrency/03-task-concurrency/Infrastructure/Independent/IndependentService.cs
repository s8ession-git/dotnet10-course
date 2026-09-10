public sealed class IndependentService : ILoadEnvironmentConfigService
{
    public async Task<string> LoadEnvironmentConfigAsync()
    {
        await Task.Delay(1000);
        return "Production";
    }
}