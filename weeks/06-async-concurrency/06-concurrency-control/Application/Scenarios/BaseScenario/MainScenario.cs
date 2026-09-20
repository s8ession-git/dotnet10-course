public sealed class MainScenario
{
    private readonly IImageProcessor imageProcessor;
    private int processedCount;
    private int activeCount;
    private int currentActive;


    public MainScenario(IImageProcessor imageProcessor)
    {
        this.imageProcessor = imageProcessor ?? throw new ArgumentNullException(nameof(imageProcessor));
    }


}