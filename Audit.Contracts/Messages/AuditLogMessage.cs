namespace IntegrationEvents;

public class AuditLogMessage : IntegrationEvent
{
    public string? EntityId { get; set; }

    public string? EntityName { get; set; }

    public string? FieldName { get; set; }

    public string? OldContent { get; set; }

    public string? NewContent { get; set; }

    public string? ChangedBy { get; set; }

    public string? ChangedById { get; set; }

    public DateTime ChangedDateTime { get; set; }
}
