namespace DefaultNamespace;

public class MyList
{
    public Guid id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid TitleId { get; set; }
    public Title Title { get; set; } = null!;

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}