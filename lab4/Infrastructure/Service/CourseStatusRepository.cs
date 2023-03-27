using Entity;
using Entity.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Service;

public class CourseStatusRepository : ICourseStatusRepository
{
    private readonly DbSet<CourseStatus> _dbSet;
    
    public CourseStatusRepository(CourseDbContext dbContext)
    {
        _dbSet = dbContext.Set<CourseStatus>();
    }
    
    public List<CourseStatus> GetCourseStatusList()
    {
        throw new NotImplementedException();
    }

    public CourseStatus? GetCourseStatus(string id)
    {
        return _dbSet.FirstOrDefault(c => c.EnrollmentId == id);
    }

    public void Add(CourseStatus courseStatus)
    {
        _dbSet.Add(courseStatus);
    }

    public void DeleteCourseStatus(string id)
    {
        var courseStatus = GetCourseStatus(id);
        if (courseStatus != null)
        {
            _dbSet.Remove(courseStatus);
        }
    }

    public void UpdateCourseStatus(CourseStatus courseStatus)
    {
        _dbSet.Update(courseStatus);
    }
}