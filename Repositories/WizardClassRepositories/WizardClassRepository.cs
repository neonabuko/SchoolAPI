using WizardAPI.Data;
using WizardAPI.Entities;
using WizardAPI.Repositories.WizardRepositoriesImpl;

namespace WizardAPI.Repositories.WizardClassRepositories;

public class WizardClassRepository(WizardContext context) : WizardRepositoryImpl<WizardClass>(context)
{
    
}