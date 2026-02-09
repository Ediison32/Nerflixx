using System.ComponentModel.DataAnnotations;
namespace DefaultNamespace;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    public Role Role { get; set; }
    
    // Auth - Refresh Token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    //Users (1) ────────< WatchHistory (N)

    public ICollection<UserEntitlement> Entitlements { get; set; } = new List<UserEntitlement>();
    public ICollection<WatchHistory> WatchHistory { get; set; } = new List<WatchHistory>();
    public ICollection<MyList> MyList { get; set; } = new List<MyList>();
}