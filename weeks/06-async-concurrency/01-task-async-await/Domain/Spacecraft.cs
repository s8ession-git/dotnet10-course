public sealed class Spacecraft
{
    public int Id { get; }
    public string Name { get; }

    public Spacecraft(int id, string name)
    {
        ValidataConstructor(id, name);
        Id = id;
        Name = name;
    }

    private static void ValidataConstructor(int id, string name)
    {
        if (!(id > 0))
            throw new ArgumentOutOfRangeException(nameof(id));

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Name cannot be null or whitespace.",
                nameof(name));
        }
    }
}