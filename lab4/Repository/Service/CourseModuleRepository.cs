using Entity;
using Entity.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Service;

public class CourseModuleRepository : ICourseModuleRepository
{
    private readonly DbSet<CourseModule> _dbSet;
    
    public CourseModuleRepository(CourseDbContext dbContext)
    {
        _dbSet = dbContext.Set<CourseModule>();
    }
    
    public List<CourseModule> GetCoursesModulesList()
    {
        throw new NotImplementedException();
    }

    public CourseModule? GetCourseModule(string id)
    {
        return _dbSet.FirstOrDefault(c => c.ModuleId == id);
    }

    public void AddCourseModule(CourseModule courseModule)
    {
        _dbSet.Add(courseModule);
    }

    public void DeleteCourseModule(string id)
    {
        throw new NotImplementedException();
    }

    public void UpdateCourseModule(Course course)
    {
        throw new NotImplementedException();
    }
}