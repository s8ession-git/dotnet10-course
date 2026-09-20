IReadOnlyCollection<ImageFile> images = new List<ImageFile>
{
    new("image-a.jpg", 100),
    new("image-b.jpg", 200),
    new("image-c.jpg", 300),
    new("image-d.jpg", 400),
    new("image-e.jpg", 500),
    new("image-f.jpg", 600),
    new("image-g.jpg", 700),
    new("image-h.jpg", 800),
    new("image-i.jpg", 900),
    new("image-j.jpg", 1000),
};

MainImageProcessor imageProcessor = new();
MainScenario scenario = new(imageProcessor);
ConcurrencyLoggingPresenter presenter = new(scenario);

using CancellationTokenSource cts = new CancellationTokenSource();
await presenter.ShowAsync(images, 3, cts.Token);