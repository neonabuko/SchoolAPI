namespace SchoolAPI.Entities.DTOs.Create;

public record StudentCreateDto(
    string Name,
    string? Birthday,
    string? RegistrationId,
    int? GroupId
    );