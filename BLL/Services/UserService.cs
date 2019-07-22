using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DBContext.Models;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _database { get; set; }


        public UserService(IUnitOfWork uow)
        {
            _database = uow;
        }

        private ApplicationUserManager AppUserManager
        {
            get
            {
                return _database.UserManager;
            }
        }

        public OperationDetails Create(UserDTO userDto)
        {
            ApplicationUser user = AppUserManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = AppUserManager.Create(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                AppUserManager.AddToRoles(user.Id, userDto.Role.ToString());
                // создаем профиль клиента
                ApplicationUserProfile clientProfile = new ApplicationUserProfile { Id = user.Id, Name = userDto.Name };
                _database.UsersProfiles.Create(clientProfile);
                _database.Save();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public ClaimsIdentity Authenticate(UserDTO userDto, string authenticationType)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = AppUserManager.Find(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            {
                claim = AppUserManager.CreateIdentity(user, authenticationType);
            }
            return claim;
        }

        // начальная инициализация бд
        public void SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = _database.RoleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    _database.RoleManager.Create(role);
                }
            }
            Create(adminDto);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var list = new List<UserDTO>();
            var collection = _database.UserManager.Users;
            foreach (var user in collection)
            {
                var newUser = new UserDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    UserName = user.UserName
                };
                list.Add(newUser);
            }
            return list;
        }

        public UserDTO GetUser(string id)
        {
            var user = _database.UserManager.FindById(id);
            UserDTO userToGet = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                UserName = user.UserName
            };
            return userToGet;
        }

        public void EditUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ValidationException("Invalid input user", "");
            }
            try
            {
                var appUserToChange = _database.UserManager.FindById(userDTO.Id);
                var appUserProfileToChange = _database.UsersProfiles.Read(userDTO.Id);
                appUserToChange.Email = userDTO.Email;
                appUserToChange.UserName = userDTO.UserName;
                appUserProfileToChange.Name = userDTO.Name;
                _database.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while editing user", "");
            }
        }

        public UserDTO Find(string login, string password)
        {
            var user = AppUserManager.Find(login, password);
            UserDTO userToGet = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                UserName = user.UserName
            };
            return userToGet;
        }

        public UserDTO Find(UserLoginInfo userLoginInfo)
        {
            var user = AppUserManager.Find(userLoginInfo);
            UserDTO userToGet = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                UserName = user.UserName
            };
            return userToGet;
        }
    }
}
