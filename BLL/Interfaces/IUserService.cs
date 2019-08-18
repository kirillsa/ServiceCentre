using System.Collections.Generic;
using System.Security.Claims;
using BLL.DTO;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        OperationDetails Create(UserDTO userDto);
        ClaimsIdentity Authenticate(UserDTO userDto, string authenticationType);
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(string id);
        UserDTO Find(string login, string password);
        void Edit(UserDTO userDTO);
    }
}