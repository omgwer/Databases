using Entity;
using Entity.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Service;

public class CourseEnrollmentRepository : ICourseEnrollmentRepository
{
    private readonly DbSet<CourseEnrollment> _dbSet;
    
    public CourseEnrollmentRepository(CourseDbContext dbContext)
    {
        _dbSet = dbContext.Set<CourseEnrollment>();
    }

    public List<CourseEnrollment> GetList()
    {
        throw new NotImplementedException();
    }

    public CourseEnrollment? Get(string id)
    {
        return _dbSet.FirstOrDefault(c => c.EnrollmentId == id);
    }

    public void Add(CourseEnrollment courseStatus)
    {
        _dbSet.Add(courseStatus);
    }

    public void Delete(string id)
    {
        var courseStatus = Get(id);
        if (courseStatus != null)
        {
            _dbSet.Remove(courseStatus);
        }
    }

    public void Update(CourseEnrollment course)
    {
        _dbSet.Update(course);
    }
}