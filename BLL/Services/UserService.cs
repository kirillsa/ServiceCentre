using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DBContext.Models;
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

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await _database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ApplicationUserProfile clientProfile = new ApplicationUserProfile { Id = user.Id, Name = userDto.Name };
                _database.UsersProfiles.Create(clientProfile);
                await _database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            { 
                claim = await _database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await _database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var list = new List<UserDTO>();
            foreach (var user in _database.UsersProfiles.ReadAll())
            {
                var newUser = new UserDTO()
                {
                    Id = user.Id,
                    Email = user.ApplicationUser.Email,
                    Password = user.ApplicationUser.PasswordHash,
                    UserName = user.ApplicationUser.UserName
                };
                list.Add(newUser);
            }
            return list;
        }

        public UserDTO GetUser(int id)
        {
            var user = _database.UserManager.FindById(id.ToString());
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
                var userToChange = _database.UserManager.FindById(userDTO.Id);
                userToChange.Email = userDTO.Email;
                userToChange.UserName = userDTO.UserName;
            }
            catch (Exception)
            {
                throw new ValidationException("Error while editing user", "");
            }
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
