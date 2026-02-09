namespace DefaultNamespace;

public class TitleGenre
{
    public Guid TitleId { get; set; }
    public Title Title { get; set; } = null!;

    public Guid GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}