namespace DefaultNamespace;

public class Subscription
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public Guid PlanId { get; set; } // referencia externa
    public string Status { get; set; } = "active"; // active, past_due, canceled

    public DateTime CurrentPeriodEnd { get; set; }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}