namespace WizardAPI.Entities.DTOs.View;

public record InteractiveClassViewDto(
    int Id,
    string Lesson,
    string DateTime,
    string? Oral,
    bool? HwDelivered,
    string? HwGrade,
    bool? StudentPresent,
    int? StudentId
    );