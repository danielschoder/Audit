namespace Audit.Contracts.Responses;

public record Alive(
    DateTime UtcNow,
    string? Version);
