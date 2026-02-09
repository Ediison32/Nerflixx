namespace DefaultNamespace;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<TitleGenre> TitleGenres { get; set; } = new List<TitleGenre>();
}