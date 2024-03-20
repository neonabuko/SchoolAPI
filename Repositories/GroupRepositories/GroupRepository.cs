using WizardAPI.Data;
using WizardAPI.Entities;
using WizardAPI.Repositories.WizardRepositoriesImpl;

namespace WizardAPI.Repositories.GroupRepositories;

public class GroupRepository(WizardContext context) : WizardRepositoryImpl<InteractiveGroup>(context)
{
    
}