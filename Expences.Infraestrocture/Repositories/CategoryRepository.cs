﻿
using Expences.Domain.Entities;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Core;
using Expences.Infraestrocture.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Expences.Infraestrocture.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public DbAppContext Context { get; set; }
        public CategoryRepository(DbAppContext dbAppContext) : base(dbAppContext)
        {
            Context = dbAppContext;
        }
        public override void Delete(Category category)
        {
            var DeleteCategory = Get(category.Id);
            try
            {
                if (Exits(cd => cd.Id == DeleteCategory.Id)) return;
                Context.Category.Remove(DeleteCategory);
                Context.SaveChanges();
            }catch{
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
            catch
            {

                throw;
            }
            
        }
    }
}
