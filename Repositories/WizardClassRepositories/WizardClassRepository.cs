using SchoolAPI.Data;
using SchoolAPI.Entities;
using SchoolAPI.Repositories.SchoolRepositoriesImpl;

namespace SchoolAPI.Repositories.LessonRepositories;

public class LessonRepository(SchoolContext context) : SchoolRepositoryImpl<Lesson>(context)
{
    
}