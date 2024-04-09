namespace SchoolAPI.Entities.DTOs.View;

public record LessonViewDto(
    string Id,
    string Name,
    string DateTime,
    string? Oral,
    string? HwDelivered,
    string? HwGrade,
    string? StudentPresent,
    string? StudentId
    );