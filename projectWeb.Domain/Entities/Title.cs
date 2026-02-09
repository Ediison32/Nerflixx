namespace DefaultNamespace;

public class Title
{
    public Guid Id { get; set; }
    public string TitleName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Status { get; set; } // "Returning Series", "Ended", "Canceled"
    public string AgeRating { get; set; } = null!;
    public float ImdbRating { get; set; }
    public string? movieUrl { get; set; }
    public string? TotalSeasons { get; set; }
    public TitleType Type { get; set; } 


    // Muy importante para streaming
    public string? TrailerUrl { get; set; } 
    public string? CoverImageUrl { get; set; }
    public string? BackdropImageUrl { get; set; }
    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public ICollection<TitleGenre> TitleGenres { get; set; } = new List<TitleGenre>();
}

