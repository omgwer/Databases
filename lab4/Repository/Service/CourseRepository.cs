using Entity;
using Entity.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository.Service;

public class CourseRepository : ICourseRepository
{
    private readonly DbSet<Course> _dbSet;
    
    public CourseRepository(CourseDbContext dbContext)
    {
        _dbSet = dbContext.Set<Course>();
    }
    
    public List<Course> GetCoursesList()
    {
        throw new NotImplementedException();
    }
    
    public Course? GetCourse(string id)
    {
       return _dbSet.FirstOrDefault(c => c.CourseId == id);
    }
    
    public void AddCourse(Course course)
    {
        _dbSet.Add(course);
    }
    
    public void DeleteCourse(string id)
    {
        _dbSet.Remove(GetCourse(id)!);
    }
    
    public void Update(Course course)
    {
        throw new NotImplementedException();
    }
}