MediaFile mediaFileA = new("video-a.mp4", 10);
MediaFile mediaFileB = new("video-b.mp4", 20);
MediaFile mediaFileC = new("video-c.mp4", 30);

MainTranscoder transcoder = new();
MainAnalyzer analyzer = new();
MainThumbnailGenerator thumbnailGenerator = new();

MainScenario scenario = new(transcoder, analyzer, thumbnailGenerator);
OrdinaryCompletionExperiment experimentA = new(transcoder, analyzer, thumbnailGenerator);

MainScenarioPresenter presenter = new(scenario);
OrdinaryCompletionPresenter completionPresenter = new(experimentA);

await completionPresenter.ShowAsync(mediaFileA);

//using CancellationTokenSource cts = new CancellationTokenSource();
//Task task = presenter.ShowAsync(mediaFileA, cts.Token);
//cts.Cancel();

