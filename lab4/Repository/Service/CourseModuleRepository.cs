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

    public CourseModule? GetCourseModule(string moduleId)
    {
        return _dbSet.FirstOrDefault(c => c.ModuleId == moduleId);
    }

    public List<CourseModule> getCourseModulesListByCourseId(string courseId)
    {
        return _dbSet.Where(x => x.CourseId == courseId).ToList();
    }

    public void AddCourseModule(CourseModule courseModule)
    {
        _dbSet.Add(courseModule);
    }

    public void DeleteCourseModule(string id)
    {
        var courseStatus = GetCourseModule(id);
        if (courseStatus != null)
        {
            _dbSet.Remove(courseStatus);
        }
    }

    public void DeleteCourseModule(CourseModule courseModule)
    {
        _dbSet.Remove(courseModule);
    }

    public void UpdateCourseModule(Course course)
    {
        throw new NotImplementedException();
    }
}