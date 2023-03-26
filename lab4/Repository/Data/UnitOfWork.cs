using Entity;

namespace Repository.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CourseDbContext _dbContext;


    public UnitOfWork(CourseDbContext courseDbContext)
    {
        _dbContext = courseDbContext;
    }

    public void Commit()
    {
        _dbContext.SaveChanges();

        //  _dbContext.Database.BeginTransaction();
        //   _dbContext.Database.RollbackTransaction();
    }
}