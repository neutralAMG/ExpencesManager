
using Expences.Aplication.Core;
using Expences.Aplication.Dto.Users;
using Expences.Aplication.Models;


namespace Expences.Aplication.Contracts
{
    public interface IUsersService : IService<UsersGetModel, UsersSaveDto, UsersUpdateDto>
    {
        ServiceResult<UsersGetModel> LogIn(string name, string pass);
        ServiceResult<UsersGetModel> UpdateCredentials(UsersUpdateCredentialDto user);
        ServiceResult<UsersGetModel> Validate(UsersBaseDto usersBaseDto);
    }
}
