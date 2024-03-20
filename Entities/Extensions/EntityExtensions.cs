using WizardAPI.Entities.DTOs;
using WizardAPI.Entities.Enums;

namespace WizardAPI.Entities.Extensions;

public static class EntityExtensions
{
    public static TeacherDto AsDto(this Teacher teacher)
    {
        return new TeacherDto(
            teacher.Name,
            teacher.Birthday
            );
    }

    public static WizardClassDto AsDto(this WizardClass wizardClass)
    {
        var dateTime = wizardClass.DateTime;
        var formattedDateTime = dateTime.ToString("dd/MM HH:mm");
        
        return new WizardClassDto(
            wizardClass.Id,
            wizardClass.Lesson,
            formattedDateTime,
            wizardClass.TeacherId,
            wizardClass.Oral,
            wizardClass.HwDelivered,
            wizardClass.HwGrade,
            wizardClass.StudentPresent
        );
    }

    public static WizardClassOutDto AsOutDto(this WizardClass wizardClass)
    {
        // Parse DateTime obj into string
        var dateTime = wizardClass.DateTime;
        var formattedDateTime = dateTime.ToString("dd/MM HH:mm");
        
        // Parse Oral into Grade enum
        var oral = Enum.GetName(typeof(Grades), (Grades)wizardClass.Oral);
        
        // Parse HwGrade into Grade enum
        var hwGrade = Enum.GetName(typeof(Grades), (Grades)wizardClass.HwGrade);
        
        return new WizardClassOutDto(
            wizardClass.Lesson,
            formattedDateTime,
            oral ?? throw new NullReferenceException(),
            wizardClass.HwDelivered,
            hwGrade ?? throw new NullReferenceException(),
            wizardClass.StudentPresent
        );
    }

    public static GroupOutDto AsOutDto(this InteractiveGroup interactiveGroup)
    {
        var dateTime = interactiveGroup.DateTime;
        var formattedDateTime = dateTime.ToString("dd/MM HH:mm");
        var studentsAsOutDto = interactiveGroup.Students.Select(student => student.AsOutDto()).ToList();
        return new GroupOutDto(
            interactiveGroup.Teacher.Name,
            formattedDateTime,
            studentsAsOutDto
        );
    }
    
    public static StudentOutDto AsOutDto(this Student student)
    {
        if (student.InteractiveGroup == null) throw new NullReferenceException();
        var wizardClassesAsOutDto = (student.WizardClasses ?? throw new InvalidOperationException())
            .Select(classes => classes.AsOutDto()).ToList();
        return new StudentOutDto(
            student.Name,
            wizardClassesAsOutDto
        );
    }
}