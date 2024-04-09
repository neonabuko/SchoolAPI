namespace SchoolAPI.Entities.DTOs.Create;

public record GroupCreateDto(
    string Name,
    string DateTime,
    int? TeacherId
    );