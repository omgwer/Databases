using Entity;
using Entity.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Service;

public class CourseModuleStatusRepository : ICourseModuleStatusRepository
{
    private readonly DbSet<CourseModuleStatus> _dbSet;

    public CourseModuleStatusRepository(CourseDbContext dbContext)
    {
        _dbSet = dbContext.Set<CourseModuleStatus>();
    }

    public List<CourseModuleStatus> GetList()
    {
        throw new NotImplementedException();
    }

    public CourseModuleStatus? Get(string enrollmentId, string moduleId)
    {
        return _dbSet.FirstOrDefault(c => c.EnrollmentId == enrollmentId && c.ModuleId == moduleId);
    }

    public void Add(CourseModuleStatus courseModuleStatus)
    {
        _dbSet.Add(courseModuleStatus);
    }

    public void Delete(string enrollmentId, string moduleId)
    {
        var courseStatus = Get(enrollmentId, moduleId);
        if (courseStatus != null)
        {
            _dbSet.Remove(courseStatus);
        }
    }

    public void Delete(CourseModuleStatus courseModuleStatus)
    {
        _dbSet.Remove(courseModuleStatus);
    }

    public void Update(CourseModuleStatus course)
    {
        _dbSet.Update(course);
    }

    public List<CourseModuleStatus> GetListByEnrollmentId(string enrollmentId)
    {
        return _dbSet.Where(e => e.EnrollmentId == enrollmentId).ToList();
    }
}