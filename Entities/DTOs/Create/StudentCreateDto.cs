namespace WizardAPI.Entities.DTOs.Create;

public record StudentCreateDto(
    string Name,
    string? Birthday,
    string RegistrationId,
    int? InteractiveGroupId
    );