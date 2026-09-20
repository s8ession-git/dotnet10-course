MainTranscoder transcoder = new();
MainAnalyzer analyzer = new();
MainThumbnailGenerator thumbnailGenerator = new();

MainScenario scenario = new(transcoder, analyzer, thumbnailGenerator);
MainScenarioPresenter presenter = new(scenario);

MediaFile mediaFileA = new("video-a.mp4", 10);
MediaFile mediaFileB = new("video-b.mp4", 20);
MediaFile mediaFileC = new("video-c.mp4", 30);

using CancellationTokenSource cts = new CancellationTokenSource();
Task task = presenter.ShowAsync(mediaFileA, cts.Token);

//cts.Cancel();

await task;