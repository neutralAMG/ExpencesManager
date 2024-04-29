
using Expences.Aplication.Contracts;
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Category;
using Expences.Aplication.Models;
using Expences.Domain.Entities;
using Expences.Infraestrocture.Interfaces;

namespace Expences.Aplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ServiceResult<CategoryGetModel> Get(int id)
        {
            var result = new ServiceResult<CategoryGetModel>();
            try
            {
                var category = categoryRepository.Get(id);
                if (category == null)
                {
                    result.Message = "Category not found in db";
                    result.IsSuccess = false;
                    return result;
                }

                result.Data = new CategoryGetModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Expences = category.Expences.Select(expe => new ExpencesGetModel
                    {
                        Name = expe.Name,
                        Description = expe.Description,
                        Amount = expe.Amount,
                        Id = expe.Id,
                        UserId = expe.UserId                     
                    }).ToList()
                };

                result.Message = "Category get was a success";
            }
            catch (Exception ex)
            {
                result.Message = "Error geting the category";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<List<CategoryGetModel>> GetAll()
        {
            var result = new ServiceResult<List<CategoryGetModel>>();
            try
            {
                result.Data = categoryRepository.Get().Select(ct => new CategoryGetModel
                {
                    Id = ct.Id,
                    Name = ct.Name,
                    Description = ct.Description,
                }).ToList();

                if (!result.IsSuccess)
                {
                    result.Message = "Error geting the categories";
                    result.IsSuccess = false;
                    return result;
                }
                result.Message = "Categories get was a success";
            }
            catch (Exception ex)
            {
                result.Message = "Error geting the categories";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<CategoryGetModel> Save(CategorySaveDto category)
        {
            var result = new ServiceResult<CategoryGetModel>();
            try
            {

                if (!result.IsSuccess)
                {
                    result.Message = "Error saving the category";
                    result.IsSuccess = false;
                    return result;
                }

                categoryRepository.Save(new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,

                });
                result.Message = "category was saved succesfully";
            }
            catch (Exception ex)
            {
                result.Message = "Error saving the category";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<CategoryGetModel> Update(CategoryUpdateDto category)
        {
            var result = new ServiceResult<CategoryGetModel>();
            try
            {
                if (!result.IsSuccess)
                {
                    result.Message = "Error updating the category";
                    result.IsSuccess = false;
                    return result;
                }
                categoryRepository.Update(new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                });
                 
            }
            catch (Exception ex)
            {
                result.Message = "Error updating the category";
                result.IsSuccess = false;
                return result;
            }
           return result;
        }
        public ServiceResult<CategoryGetModel> Delete(int id)
        {
            var result = new ServiceResult<CategoryGetModel>();
            try
            {
                if (!result.IsSuccess)
                {
                    result.Message = "Error deleting the category";
                    result.IsSuccess = false;
                    return result;
                }

                categoryRepository.Delete(id);

            }
            catch (Exception ex)
            {
                result.Message = "Error deleting the category";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<CategoryGetModel> Validate(BaseDto baseDto)
        {
            throw new NotImplementedException();
        }
    }
}
