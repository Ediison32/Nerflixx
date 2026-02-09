namespace projectWeb.Application.DTOs.Title;

public class TitleDto
{
    public Guid Id { get; set; }
    public string TitleName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Status { get; set; }
    public string AgeRating { get; set; } = null!;
    public float ImdbRating { get; set; }
    public string? MovieUrl { get; set; }
    public string? TotalSeasons { get; set; }
    public string TypeName { get; set; } = null!;
    public string? TrailerUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? BackdropImageUrl { get; set; }
    public List<string> Genres { get; set; } = new();
}
