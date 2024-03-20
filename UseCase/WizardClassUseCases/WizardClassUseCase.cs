using System.Globalization;
using WizardAPI.Entities;
using WizardAPI.Entities.DTOs;
using WizardAPI.Entities.Extensions;
using WizardAPI.Repositories.WizardRepositoriesImpl;
using WizardAPI.UseCase.WizardUseCasesImpl;

namespace WizardAPI.UseCase.WizardClassUseCases;

public class WizardClassUseCase(WizardRepositoryImpl<WizardClass> wizardClassRepository, 
    WizardRepositoryImpl<Student> studentRepository) : WizardUseCaseImpl<WizardClass>(wizardClassRepository)
{

    public async Task CreateWizardClassAsync(WizardClassDto wizardClassDto)
    {
        var dateString = wizardClassDto.DateTime;
        DateTime.TryParseExact(dateString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out var dateTime);

        var student = studentRepository.GetAsync(wizardClassDto.StudentId).Result;
        
        WizardClass newWizardClass = new()
        {
            Student = student ?? throw new NullReferenceException(),
            Lesson = wizardClassDto.Lesson,
            TeacherId = wizardClassDto.TeacherId,
            DateTime = dateTime,
            HwDelivered = wizardClassDto.HwDelivered,
            HwGrade = wizardClassDto.HwGrade,
            Oral = wizardClassDto.Oral,
            StudentPresent = wizardClassDto.StudentPresent
        };

        await wizardClassRepository.CreateAsync(newWizardClass);
    }

    public async Task<ICollection<WizardClassOutDto>> GetAllWizardClassesAsync()
    {
        return (await wizardClassRepository.GetAllAsync()).Select(t => t.AsOutDto()).ToList();
    }

    public async Task<WizardClassDto> GetWizardClassAsync(int id)
    {
        return (await wizardClassRepository.GetAsync(id) ?? throw new NullReferenceException()).AsDto();
    }

    public async Task UpdateWizardClassAsync(int id, WizardClassDto wizardClassDto)
    {
        var toUpdate = await wizardClassRepository.GetAsync(id);
        var dateString = wizardClassDto.DateTime;
        DateTime.TryParseExact(dateString, "dd/MM HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out var dateTime);
        
        if (toUpdate != null)
        {
            toUpdate.Lesson = wizardClassDto.Lesson;
            toUpdate.DateTime = dateTime;
            toUpdate.HwDelivered = wizardClassDto.HwDelivered;
            toUpdate.HwGrade = wizardClassDto.HwGrade;
            toUpdate.Oral = wizardClassDto.Oral;
            toUpdate.StudentPresent = wizardClassDto.StudentPresent;
        }

        if (toUpdate != null) await wizardClassRepository.UpdateAsync(toUpdate);
    }
}