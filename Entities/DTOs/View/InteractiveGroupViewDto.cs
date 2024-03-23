namespace WizardAPI.Entities.DTOs.View;

public record InteractiveGroupViewDto(
    int Id,
    string Name,
    string DaysOfTheWeek,
    string Time,
    int? TeacherId
    );