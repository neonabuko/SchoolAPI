namespace WizardAPI.Entities.DTOs;

public record GroupOutDto(
    string TeacherName,
    string DateTime,
    ICollection<StudentOutDto> Students);