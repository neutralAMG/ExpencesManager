using Expences.Domain.Entities;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Core;
using Expences.Infraestrocture.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expences.Infraestrocture.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public DbAppContext Context { get; set; }
        public UsersRepository(DbAppContext dbAppContext) : base(dbAppContext) 
        {
            Context = dbAppContext; 
        }
        public override void Delete(Users user)
        {
            var UserDeleted  = Get(user.Id);
            try
            {
                if (Exits(cd => cd.Id == UserDeleted.Id)) return;

                Context.Users.Remove(UserDeleted);
                Context.SaveChanges();

            }catch{

                throw;
            }

        }


        public override List<Users> Get()
        {
            return Context.Users.Include(cd => cd.Expences).ToList();
        }

        public override Users Get(int id)
        {
            return Context.Users.Include(cd => cd.Expences).FirstOrDefault(cd => cd.Id == id);
        }

        public override void Save(Users user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public override void Update(Users user)
        {
            var UserUpdated = Get(user.Id);
            try
            {
                if (Exits(cd => cd.Id == UserUpdated.Id)) return;

                UserUpdated.LimiteGasto = user.LimiteGasto;
                UserUpdated.UserName = user.UserName;
                UserUpdated.Name = user.Name;
                UserUpdated.Password = user.Password;
                Context.Users.Update(UserUpdated);
                Context.SaveChanges();
            }
            catch {

                throw;
            }
            
        }
    }
}
