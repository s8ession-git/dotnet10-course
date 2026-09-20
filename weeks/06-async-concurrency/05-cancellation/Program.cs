MediaFile mediaFileA = new("video-a.mp4", 10);
MediaFile mediaFileB = new("video-b.mp4", 20);
MediaFile mediaFileC = new("video-c.mp4", 30);

MainTranscoder transcoder = new();
MainAnalyzer analyzer = new();
MainThumbnailGenerator thumbnailGenerator = new();

MainScenario scenario = new(transcoder, analyzer, thumbnailGenerator);

MainScenarioPresenter presenter = new(scenario);
OrdinaryCompletionPresenter completionPresenter = new(scenario);
ManualCancellationPresenter cancellationPresenter = new(scenario);
ManualCancellationStatusPresenter cancellationStatusPresenter = new(scenario);
CancelAfterPresenter cancellationAfterPresenter = new(scenario);
PropagationTokenPresenter propagationTokenPresenter = new(scenario);

using CancellationTokenSource cts = new CancellationTokenSource();
Task task = propagationTokenPresenter.ShowAsync(mediaFileA, cts.Token);

await Task.Delay(1000);
cts.Cancel();

try { await task; }
catch (OperationCanceledException)
{
    Console.WriteLine("Operation cancelled.");
}

