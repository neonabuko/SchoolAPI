namespace WizardAPI.Entities.DTOs.Create;

public record InteractiveGroupCreateDto(
    string Name,
    string DaysOfTheWeek,
    string Time
    );