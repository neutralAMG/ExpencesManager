
using Expences.Aplication.Contracts;
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Category;
using Expences.Aplication.Dto.Expences;
using Expences.Aplication.Dto.Users;
using Expences.Aplication.Models;
using Expences.Domain.Entities;
using Expences.Infraestrocture.Interfaces;
using Microsoft.IdentityModel.Tokens;

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
                if (category is null)
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

                if (result.Data is null)
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

                var isValid = Validate(category);

                if (!isValid.IsSuccess)
                {
                    result.Message = "Error saving the category, " + isValid.Message;
                    result.IsSuccess = isValid.IsSuccess;
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
                var isValid = Validate(category);

                if (!isValid.IsSuccess)
                {
                    result.Message = "Error updating the category, " + isValid.Message;
                    result.IsSuccess = isValid.IsSuccess;
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

        public ServiceResult<CategoryGetModel> Validate(CategoryBaseDto categoryBaseDto)
        {
            var result = new ServiceResult<CategoryGetModel>();

            if (categoryBaseDto.Name.IsNullOrEmpty())
            {
                result.Message = "Name can not be Empty";
                result.IsSuccess = false;
            }

            if (categoryBaseDto.Description.IsNullOrEmpty())
            {
                result.Message = "Description can not be Empty";
                result.IsSuccess = false;
            }

            if (categoryBaseDto.Description.Length > 50)
            {
                result.Message = "The description can not have less than 50 caracters";
                result.IsSuccess = false;
            }

            return result;
        }
    }
}
