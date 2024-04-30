
using Expences.Aplication.Contracts;
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Expences;
using Expences.Aplication.Models;
using Expences.Infraestrocture.Interfaces;
using Microsoft.IdentityModel.Tokens;
namespace Expences.Aplication.Services
{
    public class ExpencesService : IExpencesService
    {
        private readonly IExpencesRepository expencesRepository;

        public ExpencesService(IExpencesRepository expencesRepository)
        {
            this.expencesRepository = expencesRepository;
        }


        public ServiceResult<List<ExpencesGetModel>> FilterByCategory(int userId, int id)
        {
            var result = new ServiceResult<List<ExpencesGetModel>>();
            try
            {
                //Problem to fix, this needs to just get the current user ones
                result.Data = expencesRepository.FilterByCategory(userId, id).Select(expe =>
                new ExpencesGetModel
                {
                    Name = expe.Name,
                    Amount = expe.Amount,
                    Description = expe.Description,
                    Id = expe.Id,
                    UserId = expe.UserId,
                    CategoryId = expe.CategoryId,
                    DateIntroduce = expe.DateIntroduce,
                    Category = new CategoryGetModel
                    {
                        Id = expe.Category.Id,
                        Name = expe.Category.Name,
                        Description = expe.Category.Description,
                    }
                }
                ).ToList();

                if (result.Data is null)
                {
                    result.Message = "Error geting the expences";
                    result.IsSuccess = false;
                    return result;
                }
                result.Message = "Succes geting the expence";
            }
            catch (Exception ex)
            {
                result.Message = "Error geting the expences";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<ExpencesGetModel> Get(int id)
        {
            var result = new ServiceResult<ExpencesGetModel>();
            try
            {
                var expences = expencesRepository.Get(id);

                if (expences is null)
                {
                    result.Message = "Error geting the expences";
                    result.IsSuccess = false;
                    return result;
                }
                result.Data = new ExpencesGetModel
                {
                    Name = expences.Name,
                    Amount = expences.Amount,
                    Description = expences.Description,
                    Id = expences.Id,
                    UserId = expences.UserId,
                    CategoryId = expences.CategoryId,
                    DateIntroduce = expences.DateIntroduce,
                    Category = new CategoryGetModel
                    {
                        Id = expences.Category.Id,
                        Name = expences.Category.Name,
                        Description = expences.Category.Description,
                    }
                };
                result.Message = "Succes geting the expence";


            }
            catch (Exception ex)
            {
                result.Message = "Error geting the expences";
                result.IsSuccess = false;
                return result;
            }

            return result;
        }

        public ServiceResult<List<ExpencesGetModel>> GetAll()
        {
            var result = new ServiceResult<List<ExpencesGetModel>>();
            try
            {
                result.Data = expencesRepository.Get().Select(expe =>
                new ExpencesGetModel
                {
                    Name = expe.Name,
                    Amount = expe.Amount,
                    Description = expe.Description,
                    Id = expe.Id,
                    UserId = expe.UserId,
                    CategoryId = expe.CategoryId,
                    DateIntroduce = expe.DateIntroduce,
                    Category = new CategoryGetModel
                    {
                        Id = expe.Category.Id,
                        Name = expe.Category.Name,
                        Description = expe.Category.Description,
                    }
                }
                ).ToList();

                if (result.Data is null)
                {
                    result.Message = "Error geting the expences";
                    result.IsSuccess = false;
                    return result;
                }
                result.Message = "Succes geting the expence";
            }
            catch (Exception ex)
            {
                result.Message = "Error geting the expences";
                result.IsSuccess = false;
                return result;
            }

            return result;
        }

        public ServiceResult<List<ExpencesGetModel>> GetByUserId(int userId)
        {
            var result = new ServiceResult<List<ExpencesGetModel>>();
            try
            {

                result.Data = expencesRepository.GetByUserId(userId).Select(expe =>
                new ExpencesGetModel
                {
                    Name = expe.Name,
                    Amount = expe.Amount,
                    Description = expe.Description,
                    Id = expe.Id,
                    UserId = expe.UserId,
                    CategoryId = expe.CategoryId,
                    DateIntroduce = expe.DateIntroduce,
                    Category = new CategoryGetModel
                    {
                        Id = expe.Category.Id,
                        Name = expe.Category.Name,
                        Description = expe.Category.Description,
                    }
                }
                  ).ToList();

                if (result.Data is null)
                {
                    result.Message = "Error geting the expences";
                    result.IsSuccess = false;
                    return result;
                }

                result.Message = "Succes geting the expence";

            }
            catch (Exception ex)
            {
                result.Message = "Error geting the expences";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<ExpencesGetModel> Save(ExpencesSaveDto expence)
        {
            var result = new ServiceResult<ExpencesGetModel>();
            try
            {
                var isInValid = Validate(expence);
                if (!isInValid.IsSuccess)
                {
                    result.Message = "Error saving the expence, " + isInValid.Message;
                    result.IsSuccess = isInValid.IsSuccess;
                    return result;
                }

                expencesRepository.Save(new Domain.Entities.Expences
                {
                    Name = expence.Name,
                    Description = expence.Description,
                    CategoryId = expence.CategoryId,
                    UserId = expence.UserId,
                    DateIntroduce = expence.DateIntroduce,
                    Amount = expence.Amount,

                });
                result.Message = "Succes saving the expence";
            }
            catch (Exception ex)
            {
                result.Message = "Error saving the expence";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<ExpencesGetModel> Update(ExpencesUpdateDto expence)
        {
            var result = new ServiceResult<ExpencesGetModel>();
            try
            {
                var isInValid = Validate(expence);
                if (!isInValid.IsSuccess)
                {
                    result.Message = "Error updating the expence, " + isInValid.Message;
                    result.IsSuccess = isInValid.IsSuccess;
                    return result;
                }

                expencesRepository.Update(new Domain.Entities.Expences
                {
                    Id = expence.Id,
                    Name = expence.Name,
                    Amount = expence.Amount,
                    Description = expence.Description,
                });

                result.Message = "Succes updating the expence";
            }
            catch (Exception ex)
            {
                result.Message = "Error updating the expence";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }
        public ServiceResult<ExpencesGetModel> Delete(int id)
        {
            var result = new ServiceResult<ExpencesGetModel>();
            try
            {
                if (!result.IsSuccess)
                {
                    result.Message = "Error deleting the expence";
                    result.IsSuccess = false;
                    return result;
                }
                expencesRepository.Delete(id);
                result.Message = "Succes deleting the expence";
            }
            catch (Exception ex)
            {
                result.Message = "Error deleting the expence";
                result.IsSuccess = false;
                return result;
            }

            return result;
        }
        public ServiceResult<ExpencesGetModel> Validate(ExpencesBaseDto expencesBaseDto)
        {
            var result = new ServiceResult<ExpencesGetModel>();
            if (expencesBaseDto.Name.IsNullOrEmpty())
            {
                result.Message = "Name can not be Empty";
                result.IsSuccess = false;
            }
            if (expencesBaseDto.Description.IsNullOrEmpty())
            {
                result.Message = "The description can not be Empty";
                result.IsSuccess = false;
            }
            if (expencesBaseDto.Description.Length > 50)
            {
                result.Message = "The description can not have less than 50 caracters";
                result.IsSuccess = false;
            }

            if (expencesBaseDto.Amount == 0 || expencesBaseDto.Amount < 0)
            {
                result.Message = "Amount cant be less or 0";
                result.IsSuccess = false;
            }

            return result;
        }
    }
}
