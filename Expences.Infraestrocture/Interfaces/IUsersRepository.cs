using Expences.Domain.Entities;
using Expences.Domain.Repository;

namespace Expences.Infraestrocture.Interfaces
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Users GetByPassAndUname(string name, string pass);
        void UpdateCredentials(Users user);
    }
}
