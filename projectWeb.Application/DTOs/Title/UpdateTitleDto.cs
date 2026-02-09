using Microsoft.AspNetCore.Http;

namespace projectWeb.Application.DTOs.Title;

public class UpdateTitleDto
{
    public string? TitleName { get; set; }
    public string? Description { get; set; }
    public int? ReleaseYear { get; set; }
    public int? DurationMinutes { get; set; }
    public string? Status { get; set; }
    public string? AgeRating { get; set; }
    public float? ImdbRating { get; set; }
    public string? TotalSeasons { get; set; }
    public DefaultNamespace.TitleType? Type { get; set; }
    
    // Files to upload (optional, only if updated)
    public IFormFile? MovieFile { get; set; }
    public IFormFile? TrailerFile { get; set; }
    public IFormFile? CoverImage { get; set; }
    public IFormFile? BackdropImage { get; set; }
    
    // Genre IDs (optional, only if updated)
    public List<Guid>? GenreIds { get; set; }
}
