using Microsoft.AspNetCore.Http;

namespace projectWeb.Application.DTOs.Episode;

public class CreateEpisodeDto
{
    public Guid TitleId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public int DurationMinutes { get; set; }
    
    // Files to upload
    public IFormFile VideoFile { get; set; } = null!;
    public IFormFile? ThumbnailImage { get; set; }
}
