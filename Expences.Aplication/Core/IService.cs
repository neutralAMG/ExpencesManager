
namespace Expences.Aplication.Core
{
    public interface IService<TModel, TSave, TUpdate> 
        where TModel : class
        where TSave : class
        where TUpdate : class
    {
        ServiceResult<List<TModel>> GetAll();
        ServiceResult<TModel> Get(int id);
        ServiceResult<TModel> Save(TSave entity);
        ServiceResult<TModel> Update(TUpdate entity);
        ServiceResult<TModel> Delete(int id);
        ServiceResult<TModel> Validate(BaseDto baseDto);
    }
}
