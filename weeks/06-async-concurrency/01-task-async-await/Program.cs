Console.WriteLine("Project 1: one asynchronous operation");

string result = await LoadMessageAsync();
Console.WriteLine(result);

static async Task<string> LoadMessageAsync()
{
	await Task.Delay(300);
	return "The message was loaded asynchronously.";
}
