namespace DefaultNamespace;

public class Episode
{
    public Guid Id { get; set; }
    public Guid TitleId { get; set; }
    public string Title { get; set; }
    public string movieUrl { get; set;  }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; } // La foto del capitulo

    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    
    public int DurationMinutes { get; set; } 
    

}