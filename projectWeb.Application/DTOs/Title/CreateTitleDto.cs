using Microsoft.AspNetCore.Http;

namespace projectWeb.Application.DTOs.Title;

public class CreateTitleDto
{
    public string TitleName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Status { get; set; } // "Returning Series", "Ended", "Canceled"
    public string AgeRating { get; set; } = null!;
    public float ImdbRating { get; set; }
    public string? TotalSeasons { get; set; }
    public DefaultNamespace.TitleType Type { get; set; }
    
    // Files to upload
    public IFormFile? MovieFile { get; set; } // For movies
    public IFormFile? TrailerFile { get; set; }
    public IFormFile? CoverImage { get; set; }
    public IFormFile? BackdropImage { get; set; }
    
    // Genre IDs
    public List<Guid> GenreIds { get; set; } = new();
}
