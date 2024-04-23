using Expences.Domain.Entities;
using Expences.Domain.Repository;
namespace Expences.Infraestrocture.Interfaces
{
    public interface IExpencesRepository : IBaseRepository<Expences.Domain.Entities.Expences>
    {
        List<Expences.Domain.Entities.Expences> FilterByCategory(Category category);
    }
}
