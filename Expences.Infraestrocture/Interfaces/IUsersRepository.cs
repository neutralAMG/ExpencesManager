using Expences.Domain.Entities;
using Expences.Domain.Repository;

namespace Expences.Infraestrocture.Interfaces
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Users LogIn(string name);
        void UpdateCredentials(Users user);
    }
}
