
namespace Expences.Domain.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        List<TEntity> Get();
        bool Exits(Func<TEntity, bool> predicate);
        TEntity Get(int id);
        void Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
