using WizardAPI.Data;
using WizardAPI.Entities;
using WizardAPI.Repositories.WizardRepositoriesImpl;

namespace WizardAPI.Repositories.StudentRepositories;

public class StudentRepository(WizardContext context) : WizardRepositoryImpl<Student>(context)
{
    
}