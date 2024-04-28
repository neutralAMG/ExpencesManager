using Expences.Domain.Entities;
using Expences.Infraestrocture.Context;
using Expences.Infraestrocture.Core;
using Expences.Infraestrocture.Interfaces;
using Expences.Infraestrocture.Logger;
using Expences.Infraestrocture.Logger.Loggers;
using Microsoft.EntityFrameworkCore;

namespace Expences.Infraestrocture.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
      //  private readonly LoggerAdapter logger;
      //LoggerAdapter logger
        public DbAppContext Context { get; set; }
        public UsersRepository(DbAppContext dbAppContext) : base(dbAppContext) 
        {
            Context = dbAppContext;
          //  this.logger = new LoggerAdapter(new RepositoryLogger<UsersRepository>());
        }
        public override void Delete(int id)
        {
            var UserDeleted  = Get(id);
            try
            {
                if (Exits(cd => cd.Id != UserDeleted.Id)) return;

                Context.Users.Remove(UserDeleted);
                Context.SaveChanges();

            }
            catch (Exception ex)
            {
               // logger.LogError("Error deleting the user" + ex);
                throw;
            }

        }


        public override List<Users> Get()
        {
            //Fix the navigation properties
            return  [.. Context.Users.Include(cd => cd.Expences)];
        }

        public override Users Get(int id)
        {
            var User = Context.Users.Include(cd => cd.Expences).FirstOrDefault(cd => cd.Id == id);

            if (User == null) return null;

            return User;
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
                if (Exits(cd => cd.Id != UserUpdated.Id)) return;

                UserUpdated.LimiteDeGasto = user.LimiteDeGasto;
                UserUpdated.Name = user.Name;
                Context.Users.Update(UserUpdated);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
               // logger.LogError("Error updating the credantials of the user" + ex);
                throw;
            }
        }

        public Users GetByPassAndUname(string name, string pass)
        {

            return Context.Users.Include(cd => cd.Expences).FirstOrDefault(cd => cd.UserName == name && cd.UsuarioPassword == pass);
           
        }

        public void UpdateCredentials(Users user)
        {
            var UserUpdated = Get(user.Id);
            try
            {
                if (Exits(cd => cd.Id != UserUpdated.Id)) return;

                UserUpdated.UserName = user.UserName;
                UserUpdated.UsuarioPassword = user.UsuarioPassword;
                Context.Users.Update(UserUpdated);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
               // logger.LogError("Error updating the credantials of the user" + ex);
                throw;
            }


        }
    }
}
