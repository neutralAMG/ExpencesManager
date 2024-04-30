
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Category;
using Expences.Aplication.Dto.Users;
using Expences.Aplication.Models;

namespace Expences.Aplication.Contracts
{
    public interface ICategoryService : IService<CategoryGetModel, CategorySaveDto, CategoryUpdateDto>
    {
        ServiceResult<CategoryGetModel> Validate(CategoryBaseDto categoryBaseDto);
    }
}
