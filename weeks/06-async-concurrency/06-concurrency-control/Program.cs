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
    new("image-a1.jpg", 100),
    new("image-b1.jpg", 200),
    new("image-c1.jpg", 300),
    new("image-d1.jpg", 400),
    new("image-e1.jpg", 500),
    new("image-f1.jpg", 600),
    new("image-g1.jpg", 700),
    new("image-h1.jpg", 800),
    new("image-i1.jpg", 900),
    new("image-j1.jpg", 1000),
    new("image-a2.jpg", 100),
    new("image-b2.jpg", 200),
    new("image-c2.jpg", 300),
    new("image-d2.jpg", 400),
    new("image-e2.jpg", 500),
    new("image-f2.jpg", 600),
    new("image-g2.jpg", 700),
    new("image-h2.jpg", 800),
    new("image-i2.jpg", 900),
    new("image-j2.jpg", 1000),
};

MainImageProcessor imageProcessor = new();
MainScenario scenario = new(imageProcessor);
ConcurrencyLoggingPresenter presenter = new(scenario);
UnsafeLoggingPresenter unsafePresenter = new(scenario);

using CancellationTokenSource cts = new CancellationTokenSource();
//await presenter.ShowAsync(images, 3, cts.Token);
//await presenter.ShowUnboundedAsync(images, cts.Token);

await unsafePresenter.ShowAsync(images, cts.Token);