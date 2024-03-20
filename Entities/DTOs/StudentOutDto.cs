namespace WizardAPI.Entities.DTOs;

public record StudentOutDto(
    string Name,
    ICollection<WizardClassOutDto> Classes
    );