namespace SchoolAPI.Entities.DTOs.Create;

public record LessonCreateDto(
    string Name,
    string DateTime,
    string? Oral,
    string? HwDelivered,
    string? HwGrade,
    string? StudentPresent,
    int? StudentId
    );