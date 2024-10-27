namespace IntegrationEvents;

public class IntegrationEvent
{
    public Guid Id { get; set; }

    public Guid CorrelationId { get; set; }

    public DateTime CreationDate { get; }
}
