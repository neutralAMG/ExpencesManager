using Expences.Domain.Entities;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Core;
using Expences.Infraestrocture.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Expences.Infraestrocture.Repositories
{
    public class ExpencesRepository : BaseRepository<Domain.Entities.Expences>, IExpencesRepository
    {
      //  private readonly LoggerAdapter logger;

        public DbAppContext Context { get; set; }
        
        //LoggerAdapter logger
        public ExpencesRepository(DbAppContext dbAppContext) : base(dbAppContext)
        {
            Context = dbAppContext;
         //   this.logger = new LoggerAdapter(new RepositoryLogger<ExpencesRepository>());
        }

     
        //TODO: see if is even necesary to have a filter Expences by category

        public override void Delete(int id)
        {
            var ExpenceDeleted = Get(id);
            try
            {
                if (Exits(cd => cd.Id != ExpenceDeleted.Id)) return;

                Context.Expences.Remove(ExpenceDeleted);
                Context.SaveChanges();

            }
            catch (Exception ex) 
            {
              //  logger.LogError("Error deleting the new expence" + ex);
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
                if (Exits(cd => cd.Id != expenceUpdated.Id)) return;
                expenceUpdated.Description = expences.Description;
                expenceUpdated.Amount = expences.Amount;
                Context.Expences.Update(expenceUpdated);
                Context.SaveChanges();
            }
             catch (Exception ex)
            {
               // logger.LogError("Error Saving the new expence" + ex);
                throw;
            }
        }

        public List<Domain.Entities.Expences> FilterByCategory(int userId, int id)
        {
            var UserExpences = GetByUserId(userId);
            return [.. UserExpences.Where(cd => cd.CategoryId == id)];
        }

        public List<Domain.Entities.Expences> GetByUserId(int userId)
        {
            return Context.Expences.Include(cd => cd.Category).Where(cd => cd.UserId == userId).ToList();
        }
    }
}
