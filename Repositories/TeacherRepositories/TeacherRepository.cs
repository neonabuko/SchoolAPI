using SchoolAPI.Data;
using SchoolAPI.Entities;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;

namespace SchoolAPI.Repositories.TeacherRepositories;

public class TeacherRepository(SchoolContext context) : SchoolRepositoryImpl<Teacher>(context)
{
}