namespace DefaultNamespace;

public class Customer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // referencia al usuario del sistema principal
    public string ProviderCustomerId { get; set; } = null!;

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}