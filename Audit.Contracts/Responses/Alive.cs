namespace Audit.Contracts.Responses;

public class Alive
{
    public DateTime UtcNow { get; set; }

    public string? Version { get; set; }
}
