namespace WizardAPI.Entities.DTOs.Edit;

public record InteractiveClassEditDto(
    string? Lesson,
    string? DateTime,
    int? Oral,
    bool? HwDelivered,
    int? HwGrade,
    bool? StudentPresent,
    int? StudentId
    );