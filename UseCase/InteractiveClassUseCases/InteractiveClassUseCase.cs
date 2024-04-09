using System.Globalization;
using SchoolAPI.Entities;
using SchoolAPI.Entities.DTOs.Create;
using SchoolAPI.Entities.DTOs.Edit;
using SchoolAPI.Entities.DTOs.View;
using SchoolAPI.Entities.Enums;
using SchoolAPI.Entities.Extensions;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;
using SchoolAPI.UseCase.SchoolUseCasesImpl;

namespace SchoolAPI.UseCase.LessonUseCases;

public class LessonUseCase(
    SchoolRepositoryImpl<Lesson> LessonRepository,
    SchoolRepositoryImpl<Student> studentRepository) : SchoolUseCaseImpl<Lesson>(LessonRepository)
{
    public async Task<LessonViewDto> CreateLessonAsync(LessonCreateDto LessonCreateDto)
    {
        if (LessonCreateDto.StudentId != null) {
            if (
                 GetAllLessonsFromStudentAsync((int)LessonCreateDto.StudentId)
                .Result
                .Where(c => c.Name == LessonCreateDto.Name)
                .ToList()
                .Count > 0
            ) throw new DbNameConflictException("Student already has a lesson with name '" + LessonCreateDto.Name + "'");
        }

        DateTime.TryParseExact(
            LessonCreateDto.DateTime,
            "dd/MM HH:mm",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var dateTime
        );

        Enum.TryParse(LessonCreateDto.Oral ?? "None", true, out Grades oral);
        Enum.TryParse(LessonCreateDto.HwGrade ?? "None", true, out Grades hwGrade);

        Lesson newLesson = new()
        {
            Name = LessonCreateDto.Name,
            DateTime = dateTime,
            Oral = oral,
            HwDelivered = bool.Parse(LessonCreateDto.HwDelivered ?? "false"),
            HwGrade = hwGrade,
            StudentPresent = bool.Parse(LessonCreateDto.StudentPresent ?? "false"),
            StudentId = LessonCreateDto.StudentId
        };

        await LessonRepository.CreateAsync(newLesson);
        return newLesson.AsViewDto();
    }

    public async Task<ICollection<LessonViewDto>> GetAllLessonesAsync()
    {
        return (await LessonRepository.GetAllAsync()).Select(i => i.AsViewDto()).ToList();
    }

    public async Task<ICollection<LessonViewDto>> GetAllLessonsFromStudentAsync(int studentId)
    {
        var Lessones = await LessonRepository.GetAllAsync();
        return Lessones
            .Where(i => i.StudentId == studentId)
            .Select(i => i.AsViewDto())
            .ToList();
    }

    public async Task<LessonViewDto> GetStudentsMostRecentLessonAsync(int studentId)
    {
        var Lessones = await LessonRepository.GetAllAsync();
            return Lessones
                .Where(i => i.StudentId == studentId)
                .Where(i => i.DateTime.Day == DateTime.Now.Day)
                .Select(i => i.AsViewDto()).ToList().First();
    }

    public Task<ICollection<StudentViewDto>> GetStudentsThatHaveLessonTodayAsync(int groupId)
    {
        var classes = LessonRepository.GetAllAsync().Result
            .Where(i => i.DateTime.Date == DateTime.Now.Date).ToList();

        return Task.FromResult<ICollection<StudentViewDto>>(classes.Select(
                iClass =>
                    studentRepository.GetAsync(iClass.StudentId ?? throw new NullReferenceException()).Result
                    ?? throw new NullReferenceException())
            .ToList().Where(s => s.GroupId == groupId).Select(s => s.AsViewDto()).ToList());
    }

    public async Task<LessonViewDto> GetLessonAsync(int id)
    {
        return (await LessonRepository.GetAsync(id) ?? throw new NullReferenceException()).AsViewDto();
    }

    public async Task UpdateLessonAsync(int id, LessonEditDto LessonEditDto)
    {
        var toUpdate = await LessonRepository.GetAsync(id) ?? throw new NullReferenceException();

        Enum.TryParse(LessonEditDto.Oral ?? "None", true, out Grades oral);
        Enum.TryParse(LessonEditDto.HwGrade ?? "None", true, out Grades hwGrade);
        
        toUpdate.Name = LessonEditDto.Name ?? toUpdate.Name;
        toUpdate.DateTime = LessonEditDto.DateTime != null
            ? DataConverters.StringToDateTime(LessonEditDto.DateTime)
            : toUpdate.DateTime;
        toUpdate.Oral = oral;
        toUpdate.HwDelivered = bool.Parse(LessonEditDto.HwDelivered ?? "false");
        toUpdate.HwGrade = hwGrade;
        toUpdate.StudentPresent = bool.Parse(LessonEditDto.StudentPresent ?? "false");
        if (LessonEditDto.StudentId != null)
        {
            toUpdate.StudentId = int.Parse(LessonEditDto.StudentId);
        }
        else
        {
            toUpdate.StudentId = null;
        }

        await LessonRepository.UpdateAsync(toUpdate);
    }
}