public sealed class ConsoleCatalogPresenter
{
    private readonly ICatalogSourceClient client;

    public ConsoleCatalogPresenter(ICatalogSourceClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task ShowAsync(IEnumerable<CatalogSource> sources)
    {
        foreach (CatalogSource source in sources)
        {
            try
            {
                CatalogSnapshot snapshot = await client.FetchAsync(source);
                Console.WriteLine($"{source.Name} -> {source.DelayMs} ms -> {snapshot.TotalItemsCount} records");
            }
            catch (CatalogSourceException exception)
            {
                Console.WriteLine($"{source.Name} -> {source.DelayMs} ms -> error: {exception.Message}");
            }
        }
    }
}