
using Expences.Aplication.Contracts;
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Users;
using Expences.Aplication.Models;
using Expences.Domain.Entities;
using Expences.Infraestrocture.Interfaces;
using Expences.Infraestrocture.Utils.PasswordHasher;
using Microsoft.IdentityModel.Tokens;

namespace Expences.Aplication.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IPasswordHasher passwordHasher;

        public UsersService(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
        {
            this.usersRepository = usersRepository;
            this.passwordHasher = passwordHasher;
        }


        public ServiceResult<UsersGetModel> Get(int id)
        {
            var result = new ServiceResult<UsersGetModel>();
            try
            {
                var user = usersRepository.Get(id);
                  
                if (user is null)
                {
                    result.Message = "User not find in db";
                    result.IsSuccess = false;
                    return result;
                }
                result.Data = new UsersGetModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    LimiteGasto = user.LimiteDeGasto,
                    UserName = user.UserName,
                    Expences = user.Expences.Select(cd =>
                    {
                        // returns multiple objects of ExpencesGetModel 
                        return new ExpencesGetModel
                        {
                            Id = cd.Id,
                            Name = cd.Name,
                            Amount = cd.Amount,
                            Description = cd.Description,
                            DateIntroduce = cd.DateIntroduce,
                            CategoryId = cd.CategoryId,
                            UserId = cd.UserId,
                            Category = new CategoryGetModel
                            {
                                Id = cd.CategoryId,
                                Description = cd.Category.Description,
                                Name = cd.Category.Name,
                            }

                        };

                    }
                    ).ToList()
                };


                result.Message = "User get was succesful";

            }
            catch (Exception ex)
            {
                result.Message = "Error geting the User";
                result.IsSuccess = false;
                throw;
            }

            return result;
        }

        public ServiceResult<List<UsersGetModel>> GetAll()
        {
            var result = new ServiceResult<List<UsersGetModel>>();
            try
            {
                result.Data = usersRepository.Get().Select(cd =>
                {
                    return new UsersGetModel
                    {

                        Id = cd.Id,
                        Name = cd.Name,
                        LimiteGasto = cd.LimiteDeGasto,
                        UserName = cd.UserName,
                        Expences = cd.Expences.Select(x =>
                           new ExpencesGetModel
                           {
                               Id = x.Id,
                               Name = x.Name,
                               Amount = x.Amount,
                               Description = x.Description,
                               DateIntroduce = x.DateIntroduce,
                               CategoryId = x.CategoryId,
                               UserId = x.UserId,
                               Category = new CategoryGetModel
                               {
                                   Id = x.CategoryId,
                                   Description = x.Category.Description,
                                   Name = x.Category.Name,
                               }

                           }).ToList()


                    };
                }).ToList();

                if (result.Data is null)
                {
                    result.Message = "Error geting the Users";
                    result.IsSuccess = false;
                    return result;
                }

                result.Message = "Users get was succesful";
            }
            catch (Exception ex)
            {
                result.Message = "Error geting the Users exeption";
                result.IsSuccess = false;
            }
            return result;
        }

        public ServiceResult<UsersGetModel> LogIn(string name, string pass)
        {
            var result = new ServiceResult<UsersGetModel>();
            try
            {
                
                var user = usersRepository.LogIn(name);
                
                if (user is null)
                {
                    result.Message = "User not find in db";
                    result.IsSuccess = false;
                    return result;
                }             

                var IsValidPassword = passwordHasher.Verification(user.UsuarioPassword, pass);

                if (!IsValidPassword)
                {
                    result.Message = "Incorrect user password introduced";
                    result.IsSuccess = false;
                    return result;
                }

                result.Data = new UsersGetModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    LimiteGasto = user.LimiteDeGasto,
                    UserName = user.UserName,
                    Expences = user.Expences.Select(cd =>
                    {
                        // returns multiple objects of ExpencesGetModel 
                        return new ExpencesGetModel
                        {
                            Id = cd.Id,
                            Name = cd.Name,
                            Amount = cd.Amount,
                            Description = cd.Description,
                            DateIntroduce = cd.DateIntroduce,
                            CategoryId = cd.CategoryId,
                            UserId = cd.UserId,
                            //Category = new CategoryGetModel
                            //{
                            //    Id = cd.CategoryId,
                            //    Description = cd.Category.Description,
                            //    Name = cd.Category.Name,
                            //}

                        };

                    }
                    ).ToList()
                };
  

                result.Message = "User get was succesful";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error geting the User";
                return result;
            }

            return result;
        }

        public ServiceResult<UsersGetModel> Save(UsersSaveDto user)
        {
            var result = new ServiceResult<UsersGetModel>();
            var HashPassword = passwordHasher.HashPassword(user.Password);
            try
            {
                var isInValid =  Validate(user);

                if (!isInValid.IsSuccess)
                {
                    result.IsSuccess = isInValid.IsSuccess;
                    result.Message = "Error registering the user, " + isInValid.Message;
                    return result;
                }

                usersRepository.Save(new Users
                {
                    Name = user.Name,
                    LimiteDeGasto = user.LimiteGasto,
                    UsuarioPassword = HashPassword,
                    UserName = user.UserName,
                    DateCreated = user.DateCreated,
                });
                result.Message = "User register was a succes";

            }
            catch (Exception ex)
            {
                result.Message = "Error registering the user";
                result.IsSuccess = false;
                return result;
            }
            return result;
        }

        public ServiceResult<UsersGetModel> Update(UsersUpdateDto user)
        {
            var result = new ServiceResult<UsersGetModel>();
            try
            {
                var isInValid = Validate(user);
                if (!isInValid.IsSuccess)
                {
                    result.Message = "Error updating the user, " + isInValid.Message; 
                    result.IsSuccess = false;
                    return result;
                }

                usersRepository.Update(new Users
                {
                    Id = user.Id,
                    LimiteDeGasto = user.LimiteGasto,
                    Name = user.Name,
                });
                result.Message = "User updated with succes";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error updating the user";
                return result;
            }
            return result;
        }

        public ServiceResult<UsersGetModel> UpdateCredentials(UsersUpdateCredentialDto user)
        {
            var result = new ServiceResult<UsersGetModel>();
            try
            {
                
                if (!result.IsSuccess)
                {
                    result.Message = "Error updating the user";
                    result.IsSuccess = false;
                    return result;
                }
                usersRepository.UpdateCredentials(new Users
                {
                    Id = user.Id,   
                    UserName = user.UserName,
                    UsuarioPassword = user.Password,
                });
                result.Message = "User credentials updated with succes";

            }
            catch
            {
                result.IsSuccess = false;
                result.Message = "Error updating the user credentials";
                return result;
            }
            return result;
        }
        public ServiceResult<UsersGetModel> Delete(int id)
        {
            var result = new ServiceResult<UsersGetModel>();
            try
            {
                if(!result.IsSuccess)
                {
                    result.Message = "Error deleting the user";
                    result.IsSuccess = false;
                    return result;
                }

                usersRepository.Delete(id);

                result.Message = "Succes deleting the user";

            }catch (Exception ex) {
                result.IsSuccess = false;
                result.Message = "Error deleting the user";
                return result;
            }

            return result;
        }
        public ServiceResult<UsersGetModel> Validate(UsersBaseDto usersBaseDto)
        {
            var result = new ServiceResult< UsersGetModel>();
            if (usersBaseDto.Name.IsNullOrEmpty())
            {
                result.Message = "Name can not be Empty";
                result.IsSuccess = false;
            }
            if (usersBaseDto.LimiteGasto == 0 || usersBaseDto.LimiteGasto < 0)
            {
                result.Message = "Limite de pago cant be less or 0";
                result.IsSuccess = false;
            }
            return result;
        }
    }
}
