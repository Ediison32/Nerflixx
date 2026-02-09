namespace DefaultNamespace;

public class WatchHistory
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid TitleId { get; set; }
    public Title Title { get; set; } = null!;

    public Guid? EpisodeId { get; set; }
    public Episode? Episode { get; set; }

    public int ProgressSeconds { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}