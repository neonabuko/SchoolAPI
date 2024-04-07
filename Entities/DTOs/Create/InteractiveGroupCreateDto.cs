namespace WizardAPI.Entities.DTOs.Create;

public record InteractiveGroupCreateDto(
    string Name,
    string DateTime,
    int? TeacherId
    );