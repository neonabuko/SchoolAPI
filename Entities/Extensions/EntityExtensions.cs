using SchoolAPI.Entities.DTOs.View;

namespace SchoolAPI.Entities.Extensions;

public static class EntityExtensions
{
    public static TeacherViewDto AsViewDto(this Teacher teacher)
    {
        var birthday = teacher.Birthday;
        var formattedBirthday = birthday.ToString("dd/MM/yyyy");
        return new TeacherViewDto(
            teacher.Id.ToString(),
            teacher.Name,
            formattedBirthday
            );
    }

    public static LessonViewDto AsViewDto(this Lesson Lesson)
    {
        var formattedDateTime = Lesson.DateTime.ToString("dd/MM HH:mm");
        
        return new LessonViewDto(
            Lesson.Id.ToString(),
            Lesson.Name,
            formattedDateTime,
            Lesson.Oral.ToString()?.ToUpper(),
            Lesson.HwDelivered.ToString(),
            Lesson.HwGrade.ToString()?.ToUpper(),
            Lesson.StudentPresent.ToString(),
            Lesson.StudentId.ToString()
        );
    }

    public static GroupViewDto AsViewDto(this Group Group)
    {
        var formattedDateTime = Group.DateTime.ToString("dd/MM HH:mm");
        return new GroupViewDto(
            Group.Id.ToString(),
            Group.Name,
            formattedDateTime,
            Group.TeacherId.ToString()
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
            student.RegistrationId.ToString(),
            student.GroupId.ToString()
        );
    }
}