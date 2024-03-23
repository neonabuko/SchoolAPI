namespace WizardAPI.Entities.DTOs.Create;

public record StudentCreateDto(
    string Name,
    DateOnly Birthday
    );