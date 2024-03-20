using Microsoft.EntityFrameworkCore;
using WizardAPI.Data;
using WizardAPI.Entities;
using WizardAPI.Repositories.WizardRepositoriesImpl;

namespace WizardAPI.Repositories.TeacherRepositories;

public class TeacherRepository(WizardContext context) : WizardRepositoryImpl<Teacher>(context)
{
}