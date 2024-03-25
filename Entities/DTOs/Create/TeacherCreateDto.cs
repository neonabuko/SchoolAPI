using Microsoft.AspNetCore.Mvc;

namespace WizardAPI.Entities.DTOs.Create;

public record TeacherCreateDto(
    string Name,
    DateOnly Birthday
    );