public sealed class ExperimentExceptionBeforeAwaitScenario
{
    public async Task RunAsync()
    {
        Task<int> task = ValidateThenLoadAsync(-1);

        Console.WriteLine($"Task status: {task.Status}");

        try
        {
            int result = await task;
            Console.WriteLine($"Result: {result}");
        }
        catch (ArgumentOutOfRangeException exception)
        {
            Console.WriteLine($"Caught: {exception.GetType().Name}: {exception.Message}");
        }
    }

    private static async Task<int> ValidateThenLoadAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");
        }

        await Task.Delay(200);
        return id * 10;
    }
}
