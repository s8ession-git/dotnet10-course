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
ManualCancellationTaskStatusPresenter cancellationTaskStatusPresenter = new(scenario);

await cancellationTaskStatusPresenter.ShowAsync(mediaFileA);
