
using Expences.Domain.Entities;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Core;
using Expences.Infraestrocture.Interfaces;
using Expences.Infraestrocture.Logger;
using Expences.Infraestrocture.Logger.Loggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Expences.Infraestrocture.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly LoggerAdapter logger;

        public DbAppContext Context { get; set; }
        public CategoryRepository(DbAppContext dbAppContext) : base(dbAppContext)
        {
            Context = dbAppContext;
            this.logger = new LoggerAdapter(new RepositoryLogger<CategoryRepository>());
        }
        public override void Delete(int id)
        {
            var DeleteCategory = Get(id);
            try
            {
                if (Exits(cd => cd.Id == DeleteCategory.Id)) return;
                Context.Category.Remove(DeleteCategory);
                Context.SaveChanges();
            }catch (Exception ex){
                logger.LogError("Error deleting the category" + ex);
                throw;
            }
        }

        public override List<Category> Get()
        {
            
            return [..Context.Category.Include(cd =>  cd.Expences)];
            
        }

        public override Category Get(int id)
        {
            return Context.Category.Include(cd => cd.Expences).FirstOrDefault(cd => cd.Id == id)!;
        }

        public override void Save(Category category)
        {
           Context.Add(category);
           Context.SaveChanges();
        }

        public override void Update(Category category)
        {

            var UpdatedCategory = Get(category.Id);
            try
            {
                if (Exits(cd => cd.Id == UpdatedCategory.Id)) return;

                UpdatedCategory.Description = category.Description;
                Context.Category.Update(UpdatedCategory);
                Context.SaveChanges();
            }
            catch(Exception ex){
                logger.LogError("Error updating the category" + ex);
                throw;
            }

        }
    }
}
