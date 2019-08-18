using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                return _database.Users;
            }
        }

        public OperationDetails Create(UserDTO userDto)
        {
            ApplicationUser user = AppUserManager.FindByEmail(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = userDto.Email,
                    UserName = userDto.Email
                };
                var result = AppUserManager.Create(user, userDto.Password);
                if (result.Errors.Count() > 0)
                { 
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }
                AppUserManager.AddToRoles(user.Id, userDto.Roles);
                ApplicationUserProfile clientProfile = new ApplicationUserProfile
                                                            {
                                                                Id = user.Id,
                                                                Name = userDto.Name
                                                            };
                _database.UsersProfiles.Create(clientProfile);
                _database.Save();
                return new OperationDetails(true, "Registration complete", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login is already present", "Email");
            }
        }

        public ClaimsIdentity Authenticate(UserDTO userDto, string authenticationType)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = AppUserManager.Find(userDto.Email, userDto.Password);
            if (user != null)
            {
                claim = AppUserManager.CreateIdentity(user, authenticationType);
            }
            return claim;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var list = new List<UserDTO>();
            foreach (var user in _database.Users.Users)
            {
                var userRolesList = AppUserManager.GetRoles(user.Id);
                string roles = "";
                if (userRolesList != null)
                {
                    foreach (var role in userRolesList)
                    {
                        roles += role + ',';
                    }
                    roles = roles.TrimEnd(',');
                }
                var newUser = new UserDTO()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    UserName = user.UserName,
                    Roles = roles
                };
                list.Add(newUser);
            }
            return list;
        }

        public UserDTO Get(string id)
        {
            var user = _database.Users.FindById(id);
            if (user == null)
            {
                throw new ValidationException("Invalid user id", "");
            }
            var userRolesList = AppUserManager.GetRoles(user.Id);
            string roles = "";
            if (userRolesList != null)
            {
                foreach (var role in userRolesList)
                {
                    roles += role + ',';
                }
                roles = roles.TrimEnd(',');
            }
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                UserName = user.UserName,
                Roles = roles
            };
        }

        public void Edit(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ValidationException("Invalid input user", "");
            }
            try
            {
                var appUserToChange = _database.Users.FindById(userDTO.Id);
                var appUserProfileToChange = _database.UsersProfiles.Read(userDTO.Id);
                appUserToChange.Email = userDTO.Email;
                appUserToChange.UserName = userDTO.UserName;
                appUserProfileToChange.Name = userDTO.Name;
                _database.Users.Update(appUserToChange);
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
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.PasswordHash,
                UserName = user.UserName
            };
        }
    }
}
