namespace WizardAPI.Entities.DTOs.View;

public record InteractiveGroupViewDto(
    string Id,
    string Name,
    string DateTime,
    string? TeacherId
    );