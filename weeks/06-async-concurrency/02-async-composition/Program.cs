using System.Diagnostics;

internal static class Program
{
	private static async Task Main()
    {
        var deployment = new DeploymentReceiptService();
        var health = new HealthCheckResultService();
        var independent = new IndependentService();

        var ConsoleResultPresenter = new ConsoleResultPresenter(deployment, independent, health);
        await ConsoleResultPresenter.PrintResultAsync("2.4.1");
    }
}