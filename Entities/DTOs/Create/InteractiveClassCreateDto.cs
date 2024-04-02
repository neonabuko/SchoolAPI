using WizardAPI.Entities.Enums;

namespace WizardAPI.Entities.DTOs.Create;

public record InteractiveClassCreateDto(
    string Lesson,
    string DateTime,
    string? Oral,
    string? HwDelivered,
    string? HwGrade,
    string? StudentPresent,
    int? StudentId
    );