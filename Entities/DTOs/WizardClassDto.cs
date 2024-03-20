namespace WizardAPI.Entities.DTOs;

public record WizardClassDto(
    int StudentId,
    string Lesson,
    string DateTime,
    int TeacherId,
    int Oral,
    bool HwDelivered,
    int HwGrade,
    bool StudentPresent
    );