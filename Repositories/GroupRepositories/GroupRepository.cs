using SchoolAPI.Data;
using SchoolAPI.Entities;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;

namespace SchoolAPI.Repositories.GroupRepositories;

public class GroupRepository(SchoolContext context) : SchoolRepositoryImpl<Group>(context)
{
    
}