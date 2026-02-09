namespace DefaultNamespace;

public class UserEntitlement
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null;
    
    public Guid PlanId { get; set; }
    public Plan Plan { get; set; } = null;
    public string Status { get; set; } = "active";
    public DateTime currentPeriodEnd { get; set; }
}