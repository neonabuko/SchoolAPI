namespace WizardAPI.Entities.DTOs.View;

public record StudentViewDto(
    int Id,
    string Name,
    string BirthDay,
    int? InteractiveGroupId
    );