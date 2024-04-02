using WizardAPI.Entities.DTOs.View;

namespace WizardAPI.Entities.Extensions;

public static class EntityExtensions
{
    public static TeacherViewDto AsViewDto(this Teacher teacher)
    {
        var birthday = teacher.Birthday;
        var formattedBirthday = birthday.ToString("dd/MM/yyyy");
        return new TeacherViewDto(
            teacher.Id,
            teacher.Name,
            formattedBirthday
            );
    }

    public static InteractiveClassViewDto AsViewDto(this InteractiveClass interactiveClass)
    {
        var formattedDateTime = interactiveClass.DateTime.ToString("dd/MM HH:mm");
        
        return new InteractiveClassViewDto(
            interactiveClass.Id.ToString(),
            interactiveClass.Lesson,
            formattedDateTime,
            interactiveClass.Oral.ToString()?.ToUpper(),
            interactiveClass.HwDelivered.ToString(),
            interactiveClass.HwGrade.ToString()?.ToUpper(),
            interactiveClass.StudentPresent.ToString(),
            interactiveClass.StudentId.ToString()
        );
    }

    public static InteractiveGroupViewDto AsViewDto(this InteractiveGroup interactiveGroup)
    {
        return new InteractiveGroupViewDto(
            interactiveGroup.Id,
            interactiveGroup.Name,
            interactiveGroup.DaysOfTheWeek,
            interactiveGroup.Time.ToString("HH:mm"),
            interactiveGroup.TeacherId
        );
    }
    
    public static StudentViewDto AsViewDto(this Student student)
    {
        var birthday = student.Birthday;
        var formattedBirthday = birthday.ToString("dd/MM/yyyy");
        return new StudentViewDto(
            student.Id.ToString(),
            student.Name,
            formattedBirthday,
            student.InteractiveGroupId.ToString()
        );
    }
}