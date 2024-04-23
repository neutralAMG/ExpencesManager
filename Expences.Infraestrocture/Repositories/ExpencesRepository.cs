using Expences.Domain.Entities;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Core;
using Expences.Infraestrocture.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Expences.Infraestrocture.Repositories
{
    public class ExpencesRepository : BaseRepository<Domain.Entities.Expences>, IExpencesRepository
    {
        public DbAppContext Context { get; set; }
        public ExpencesRepository(DbAppContext dbAppContext) : base(dbAppContext)
        {
            Context = dbAppContext;
        }

        //TODO: Make it so i dont have to inherite the delete and update methods
        //TODO: see if is even necesary to have a filter Expences by category

        public override void Delete(Domain.Entities.Expences expences)
        {
            var ExpenceDeleted = Get(expences.Id);
            try
            {
                if (Exits(cd => cd.Id == expences.Id)) return;

                Context.Expences.Remove(ExpenceDeleted);
                Context.SaveChanges();

            }
            catch
            {
                throw;
            }
        }



        public override List<Domain.Entities.Expences> Get()
        {
            return [.. Context.Expences.Include(cd => cd.Category)];
        }
        public override Domain.Entities.Expences Get(int id)
        {
            return Context.Expences.Include(cd => cd.Category).FirstOrDefault(cd => cd.Id == id)!;
        }

        public override void Save(Domain.Entities.Expences expences)
        {
           Context.Expences.Add(expences);
            Context.SaveChanges();
        }

        public override void Update(Domain.Entities.Expences expences)
        {
            var expenceUpdated = Get(expences.Id);
            try
            {
                if (Exits(cd => cd.Id == expenceUpdated.Id)) return;

                Context.Expences.Update(expenceUpdated);
                Context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<Domain.Entities.Expences> FilterByCategory(Category category)
        {
            return [.. Context.Expences.Where(cd => cd.CategoryId == category.Id)];
        }
    }
}
