using Microsoft.AspNetCore.Http;

namespace projectWeb.Application.DTOs.Episode;

public class UpdateEpisodeDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? SeasonNumber { get; set; }
    public int? EpisodeNumber { get; set; }
    public int? DurationMinutes { get; set; }
    
    // Files to upload (optional, only if updated)
    public IFormFile? VideoFile { get; set; }
    public IFormFile? ThumbnailImage { get; set; }
}
