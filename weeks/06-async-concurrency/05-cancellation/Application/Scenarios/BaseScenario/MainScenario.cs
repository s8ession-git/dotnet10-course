public sealed class MainScenario
{
    private readonly ITranscoder transcoder;
    private readonly IAnalyzer analyzer;
    private readonly IThumbnailGenerator thumbnailGenerator;

    public MainScenario(ITranscoder transcoder, IAnalyzer analyzer, IThumbnailGenerator thumbnailGenerator)
    {
        this.transcoder = transcoder ?? throw new ArgumentNullException(nameof(transcoder));
        this.analyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));
        this.thumbnailGenerator = thumbnailGenerator ?? throw new ArgumentNullException(nameof(thumbnailGenerator));
    }

    public async Task<MediaProcessingResult> ProcessAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        await transcoder.TranscodeAsync(mediaFile, cancellationToken);
        await analyzer.AnalyzeAsync(mediaFile, cancellationToken);
        await thumbnailGenerator.GenerateThumbnailAsync(mediaFile, cancellationToken);

        return new MediaProcessingResult(mediaFile.Name, true);
    }
}