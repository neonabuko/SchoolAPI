namespace WizardAPI.Entities.DTOs.Create;

public record InteractiveClassCreateDto(
    string Lesson,
    string DateTime,
    int? Oral,
    string? HwDelivered,
    int? HwGrade,
    string? StudentPresent,
    int? StudentId
    );