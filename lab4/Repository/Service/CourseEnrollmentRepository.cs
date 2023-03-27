﻿using Entity;
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

    public CourseEnrollment? GetByCourseId(string id)
    {
        return _dbSet.FirstOrDefault(c => c.CourseId == id);
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
        var courseEnrollment = Get(id);
        if (courseEnrollment != null)
        {
            _dbSet.Remove(courseEnrollment);
        }
        else
        {
            throw new ArgumentException($"course with id = '{id}' is not found");
        }
    }

    public void Update(CourseEnrollment course)
    {
        _dbSet.Update(course);
    }
}