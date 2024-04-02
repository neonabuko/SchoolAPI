namespace WizardAPI.Entities.DTOs.View;

public record InteractiveClassViewDto(
    string Id,
    string Lesson,
    string DateTime,
    string? Oral,
    string? HwDelivered,
    string? HwGrade,
    string? StudentPresent,
    string? StudentId
    );