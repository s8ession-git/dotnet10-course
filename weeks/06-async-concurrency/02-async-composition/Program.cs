Console.WriteLine("Project 2: several independent operations");

Task<string> firstTask = LoadAsync("first", 500);
Task<string> secondTask = LoadAsync("second", 200);

string[] results = await Task.WhenAll(firstTask, secondTask);

foreach (string result in results)
{
	Console.WriteLine(result);
}

static async Task<string> LoadAsync(string name, int delayMilliseconds)
{
	await Task.Delay(delayMilliseconds);
	return $"Completed: {name}";
}
