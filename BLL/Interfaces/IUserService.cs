using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.Identity;
using Microsoft.AspNet.Identity;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        OperationDetails Create(UserDTO userDto);
        ClaimsIdentity Authenticate(UserDTO userDto, string authenticationType);
        void SetInitialData(UserDTO adminDto, List<string> roles);

        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUser(string id);
        void EditUser(UserDTO userDTO);
        UserDTO Find(string login, string password);
        UserDTO Find(UserLoginInfo userLoginInfo);
    }
}
