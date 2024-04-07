namespace WizardAPI.Entities.DTOs.Edit;

public record InteractiveGroupEditDto(
    string? Name,
    string? DateTime,
    int? TeacherId
    );