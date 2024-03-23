namespace WizardAPI.Entities.DTOs.Edit;

public record InteractiveGroupEditDto(
    string? Name,
    string? DaysOfTheWeek,
    TimeOnly? Time,
    int? TeacherId
    );