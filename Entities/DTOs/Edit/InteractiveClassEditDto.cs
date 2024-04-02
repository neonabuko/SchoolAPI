using WizardAPI.Entities.Enums;

namespace WizardAPI.Entities.DTOs.Edit;

public record InteractiveClassEditDto(
    string? Lesson,
    string? DateTime,
    string? Oral,
    string? HwDelivered,
    string? HwGrade,
    string? StudentPresent,
    string? StudentId
    );