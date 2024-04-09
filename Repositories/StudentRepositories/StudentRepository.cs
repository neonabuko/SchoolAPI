using SchoolAPI.Data;
using SchoolAPI.Entities;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;

namespace SchoolAPI.Repositories.StudentRepositories;

public class StudentRepository(SchoolContext context) : SchoolRepositoryImpl<Student>(context)
{
    
}