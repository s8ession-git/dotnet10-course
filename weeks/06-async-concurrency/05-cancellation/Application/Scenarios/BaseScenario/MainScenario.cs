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
        ArgumentNullException.ThrowIfNull(mediaFile);
        
        await analyzer.AnalyzeAsync(mediaFile, cancellationToken);
        await transcoder.TranscodeAsync(mediaFile, cancellationToken);
        await thumbnailGenerator.GenerateThumbnailAsync(mediaFile, cancellationToken);

        return new MediaProcessingResult(mediaFile.Name, true);
    }

    public async Task<MediaProcessingResult> ProcessFramesAsync(MediaFile mediaFile, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(mediaFile);

        for (int frame = 0; frame < 10; frame++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            int frameDelay = 100;
            await Task.Delay(frameDelay);
            Console.WriteLine($"Processed frame {frame + 1}/10");
        }

        return new MediaProcessingResult(mediaFile.Name, true);
    }

    public async Task ProcessWithCancellationToken(CancellationToken cancellationToken)
    {
        await Task.Delay(1000);
        Console.WriteLine("Processed.");
    }
}