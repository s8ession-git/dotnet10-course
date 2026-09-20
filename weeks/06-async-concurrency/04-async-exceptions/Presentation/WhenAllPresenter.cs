public sealed class WhenAllPresenter
{
    public void Show(Task alphaTask, Task betaTask, Task gammaTask, Task allTask, long elapsedMs)
    {
        Console.WriteLine($"Alpha -> {alphaTask.Status}");
        Console.WriteLine($"Beta -> {betaTask.Status}");
        Console.WriteLine($"Gamma -> {gammaTask.Status}");
        Console.WriteLine($"WhenAll -> {allTask.Status}");
        Console.WriteLine($"Elapsed: {elapsedMs} ms");
    }
}
