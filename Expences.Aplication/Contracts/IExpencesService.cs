
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Expences;
using Expences.Aplication.Models;
using Expences.Domain.Entities;

namespace Expences.Aplication.Contracts
{
    public interface IExpencesService : IService<ExpencesGetModel, ExpencesSaveDto, ExpencesUpdateDto>
    {
       ServiceResult<List<ExpencesGetModel>> FilterByCategory(Category category);
        ServiceResult<List<ExpencesGetModel>> GetByUserId(int userId);
    }
}
