// 1. Создаем изменяемый список (List)
List<ImageFile> imageList = new List<ImageFile>();  

for (int i = 0; i < 10; i++) 
{
    imageList.Add(new ImageFile("image-" + i + ".jpg", 2000 + i * 100));
}

// 2. Присваиваем его коллекции только для чтения
IReadOnlyCollection<ImageFile> images = imageList;

MainImageProcessor imageProcessor = new();
MainScenario scenario = new(imageProcessor);
ConcurrencyLoggingPresenter presenter = new(scenario);
UnsafeLoggingPresenter unsafePresenter = new(scenario);

using CancellationTokenSource cts = new CancellationTokenSource();
Task task = presenter.ShowAsync(images, 2, cts.Token);

await Task.Delay(500);
cts.Cancel();

try { await task; }
catch (OperationCanceledException)
{
    Console.WriteLine("Operation cancelled.");
}

Console.WriteLine($"Task status: {task.Status}");
Console.WriteLine($"Task is completed: {task.IsCompleted}");
Console.WriteLine($"Task is canceled: {task.IsCanceled}");
Console.WriteLine($"Task is faulted: {task.IsFaulted}");
