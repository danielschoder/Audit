namespace Audit.Contracts.DTOs;

public record DbContentChangeDto(
    Guid Id,
    string? EntityId,
    string? EntityName,
    string? FieldName,
    string? OldContent,
    string? NewContent,
    string? ChangedBy,
    string? ChangedById,
    DateTime ChangedDateTime);
