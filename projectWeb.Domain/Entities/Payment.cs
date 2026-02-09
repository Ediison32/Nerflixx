namespace DefaultNamespace;

public class Payment
{
    public Guid Id { get; set; }

    public Guid SubscriptionId { get; set; }
    public Subscription Subscription { get; set; } = null!;

    public decimal Amount { get; set; }
    public string Status { get; set; } = null!; // succeeded, failed
    public DateTime PaidAt { get; set; }
}