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
ExtraScenarioPresenter extraScenarioPresenter = new(scenario);

using CancellationTokenSource cts = new CancellationTokenSource();
Task task = extraScenarioPresenter.ShowAsync(cts);
await Task.Delay(100);
cts.Cancel();

await task;
