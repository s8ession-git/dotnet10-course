public sealed class OrdinaryCompletionExperiment
{
    private readonly ITranscoder transcoder;
    private readonly IAnalyzer analyzer;
    private readonly IThumbnailGenerator thumbnailGenerator;

    public OrdinaryCompletionExperiment(ITranscoder transcoder, IAnalyzer analyzer, IThumbnailGenerator thumbnailGenerator)
    {
        this.transcoder = transcoder ?? throw new ArgumentNullException(nameof(transcoder));
        this.analyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));
        this.thumbnailGenerator = thumbnailGenerator ?? throw new ArgumentNullException(nameof(thumbnailGenerator));
    }

    public async Task<MediaProcessingResult[]> ProcessAsync(MediaFile mediaFile)
    {
        Task<MediaProcessingResult> analyzeTask = AnalyzeAsync(mediaFile);
        Task<MediaProcessingResult> transcodeTask = TranscodeAsync(mediaFile);
        Task<MediaProcessingResult> thumbnailTask = GenerateThumbnailAsync(mediaFile);

        return await Task.WhenAll(analyzeTask, transcodeTask, thumbnailTask);
    }

    private async Task<MediaProcessingResult> AnalyzeAsync(MediaFile mediaFile)
    {
        await this.analyzer.AnalyzeAsync(mediaFile, CancellationToken.None);
        return new MediaProcessingResult($"{mediaFile.Name}-analyze", true);
    }

    private async Task<MediaProcessingResult> TranscodeAsync(MediaFile mediaFile)
    {
        await this.transcoder.TranscodeAsync(mediaFile, CancellationToken.None);
        return new MediaProcessingResult($"{mediaFile.Name}-transcode", true);
    }

    private async Task<MediaProcessingResult> GenerateThumbnailAsync(MediaFile mediaFile)
    {
        await this.thumbnailGenerator.GenerateThumbnailAsync(mediaFile, CancellationToken.None);
        return new MediaProcessingResult($"{mediaFile.Name}-thumbnail", true);
    }
}