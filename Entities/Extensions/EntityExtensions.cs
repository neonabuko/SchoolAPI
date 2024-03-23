using WizardAPI.Entities.DTOs.View;
using WizardAPI.Entities.Enums;

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

        var oralGradeInt = interactiveClass.Oral ?? 4;
        var oralGrade = Enum.GetName(typeof(Grades), (Grades)oralGradeInt);

        var hwGradeInt = interactiveClass.HwGrade ?? 4;
        var hwGrade = Enum.GetName(typeof(Grades), (Grades)hwGradeInt);
        
        return new InteractiveClassViewDto(
            interactiveClass.Id,
            interactiveClass.Lesson,
            formattedDateTime,
            oralGrade,
            interactiveClass.HwDelivered,
            hwGrade,
            interactiveClass.StudentPresent,
            interactiveClass.StudentId
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
            student.Id,
            student.Name,
            formattedBirthday,
            student.InteractiveGroupId
        );
    }
}