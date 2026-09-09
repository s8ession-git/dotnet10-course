public sealed class IndependentService : ILoadEnvironmentConfigService
{
    public Task<string> LoadEnvironmentConfigAsync()
    {
        Task<string> task = Task.FromResult("Development");
        Task.Delay(1000);
        return task;
    }
}