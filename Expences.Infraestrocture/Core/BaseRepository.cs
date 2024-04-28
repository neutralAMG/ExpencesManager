using Expences.Domain.Repository;
using Expences.Infraestrocture.Context;
using Microsoft.EntityFrameworkCore;


namespace Expences.Infraestrocture.Core
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private  DbAppContext Context { get; set; }
        private DbSet<TEntity> Entity { get; set; }
        public BaseRepository(DbAppContext dbAppContext)
        {
            Context = dbAppContext;
            Entity =  Context.Set<TEntity>();
        }
        public virtual void Delete(int id)
        {
            Entity.Remove(Get(id));
            Context.SaveChanges();
        }

        public virtual bool Exits(Func<TEntity, bool> predicate)
        {
            return Entity.Any(predicate);
        }

        public virtual List<TEntity> Get()
        {
            return [..Entity];
        }

        public virtual TEntity Get(int id)
        {
            return Entity.Find(id);
        }

        public virtual void Save(TEntity entity)
        {
            Entity.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            Entity.Update(entity);
            Context.SaveChanges();
        }
    }
}
