namespace ASP.NETCoreWebApplication1.Model.Infrastructure.EntityConfiguration;

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