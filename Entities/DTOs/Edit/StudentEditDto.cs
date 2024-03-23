namespace WizardAPI.Entities.DTOs.Edit;

public record StudentEditDto(
    string? Name,
    DateOnly? Birthday,
    int? InteractiveGroupId
    );