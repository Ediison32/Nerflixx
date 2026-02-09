namespace DefaultNamespace;

public class Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int MaxDevices { get; set; }
    public string MaxQuality { get; set; }

    public ICollection<UserEntitlement> UserEntitlements { get; set; } = new List<UserEntitlement>();


}