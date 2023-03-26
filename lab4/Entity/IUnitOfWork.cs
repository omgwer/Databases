namespace Entity;

public interface IUnitOfWork
{
    void Commit();
    void Rollback();
}