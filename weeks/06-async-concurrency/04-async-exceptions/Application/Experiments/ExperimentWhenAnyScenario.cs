using System.Diagnostics;

public sealed class ExperimentWhenAnyScenario
{
    public async Task RunAsync()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        CatalogSource alpha = new("Alpha", 1000, false, 125);
        CatalogSource beta = new("Beta", 300, true, 100);
        CatalogSource gamma = new("Gamma", 700, false, 230);

        Task<CatalogSnapshot> alphaTask = ImportAsync(alpha);
        Task<CatalogSnapshot> betaTask = ImportAsync(beta);
        Task<CatalogSnapshot> gammaTask = ImportAsync(gamma);

        DisplayScenarioSummary(alpha, beta, gamma);

        Task<CatalogSnapshot> winner = await Task.WhenAny(alphaTask, betaTask, gammaTask);
        Console.WriteLine($"Winner status: {winner.Status}");

        try
        {
            CatalogSnapshot snapshot = await winner;
            Console.WriteLine($"Winner result: {snapshot.Name} -> {snapshot.TotalItemsCount} records");
        }
        catch (CatalogSourceException exception)
        {
            Console.WriteLine($"Winner Exception: {exception.Message}");
        }

        stopwatch.Stop();
        Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
    }

    private static void DisplayScenarioSummary(CatalogSource alpha, CatalogSource beta, CatalogSource gamma)
    {
        Console.WriteLine($"{alpha.Name} -> {(alpha.ShouldFail ? "fault" : "success")} after {alpha.DelayMs} ms");
        Console.WriteLine($"{beta.Name} -> {(beta.ShouldFail ? "fault" : "success")} after {beta.DelayMs} ms");
        Console.WriteLine($"{gamma.Name} -> {(gamma.ShouldFail ? "fault" : "success")} after {gamma.DelayMs} ms");
    }

    private static async Task<CatalogSnapshot> ImportAsync(CatalogSource source)
    {
        await Task.Delay(source.DelayMs);

        if (source.ShouldFail)
        {
            throw new CatalogSourceException("Catalog snapshot failed.");
        }

        return new CatalogSnapshot(source.Name, source.TotalItemsCount);
    }
}