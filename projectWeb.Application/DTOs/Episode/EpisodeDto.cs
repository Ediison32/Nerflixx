namespace projectWeb.Application.DTOs.Episode;

public class EpisodeDto
{
    public Guid Id { get; set; }
    public Guid TitleId { get; set; }
    public string Title { get; set; } = null!;
    public string MovieUrl { get; set; } = null!;
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public int DurationMinutes { get; set; }
}
