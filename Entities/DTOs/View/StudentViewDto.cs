namespace WizardAPI.Entities.DTOs.View;

public record StudentViewDto(
    string Id,
    string Name,
    string BirthDay,
    string? InteractiveGroupId
    );