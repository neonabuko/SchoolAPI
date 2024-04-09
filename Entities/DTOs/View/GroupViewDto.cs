namespace SchoolAPI.Entities.DTOs.View;

public record GroupViewDto(
    string Id,
    string Name,
    string DateTime,
    string? TeacherId
    );