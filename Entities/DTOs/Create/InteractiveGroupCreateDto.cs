namespace WizardAPI.Entities.DTOs.Create;

public record InteractiveGroupCreateDto(
    string Name,
    string DaysOfTheWeek,
    TimeOnly Time
    );