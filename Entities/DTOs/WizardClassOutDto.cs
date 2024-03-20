namespace WizardAPI.Entities.DTOs;

public record WizardClassOutDto(
    string Lesson,
    string DateTime,
    string Oral,
    bool HwDelivered,
    string HwGrade,
    bool StudentPresent
    );