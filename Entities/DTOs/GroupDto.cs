namespace WizardAPI.Entities.DTOs;

public record GroupDto(
    int TeacherId,
    string DateTime,
    ICollection<int> StudentIds
    );