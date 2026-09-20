// 1. Создаем изменяемый список (List)
List<ImageFile> imageList = new List<ImageFile>();  

for (int i = 0; i < 100; i++) 
{
    imageList.Add(new ImageFile("image-" + i + ".jpg", 1000));
}

// 2. Присваиваем его коллекции только для чтения
IReadOnlyCollection<ImageFile> images = imageList;

MainImageProcessor imageProcessor = new();
MainScenario scenario = new(imageProcessor);
ConcurrencyLoggingPresenter presenter = new(scenario);
UnsafeLoggingPresenter unsafePresenter = new(scenario);

using CancellationTokenSource cts = new CancellationTokenSource();
//await presenter.ShowAsync(images, 3, cts.Token);

await unsafePresenter.ShowAsync(images, cts.Token);
await presenter.ShowUnboundedAsync(images, cts.Token);
