namespace Lab2.Model.Infrastructure.Data;

public class UnitOfWork
{
    private readonly StopOnTheRoadDbContext _dbContext;


    public UnitOfWork(StopOnTheRoadDbContext todoDbContext)
    {
        _dbContext = todoDbContext;
    }

    public void Commit()
    {
        _dbContext.SaveChanges();
    }
}