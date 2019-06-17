using DAL.Interfaces;
using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.DBContext.Models;

namespace BLL
{
    public class CoreServices : IMainService
    {
        private IUnitOfWork _dataBase;

        public CoreServices(IUnitOfWork uow)
        {
            _dataBase = uow;
        }

        

        public void CreateUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ValidationException("Invalid input User", "");
            }
            try
            {
                var newUser = new User()
                {
                    Login = userDTO.Login,
                    Name = userDTO.Name
                };
                _dataBase.Users.Create(newUser);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while adding new User", ""); }
        }
        
        public void EditUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                throw new ValidationException("Invalid input User", "");
            }
            var userToChange = _dataBase.Users.Read(userDTO.Id);
            if (userToChange == null)
            {
                throw new ValidationException($"Wrong user id = {userDTO.Id}", "");
            }
            try
            {
                userToChange.Name = userDTO.Name;
                _dataBase.Users.Update(userToChange);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while updating user", "");
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var list = new List<UserDTO>();
            foreach (var item in _dataBase.Users.ReadAll())
            {
                var newUser = new UserDTO()
                {
                    Id = item.Id,
                    Login = item.Login,
                    Name = item.Name
                };
                list.Add(newUser);
            }
            return list;
        }
        
        public UserDTO GetUser(int id)
        {
            var userToGet = _dataBase.Users.Read(id);
            if (userToGet == null)
            {
                throw new ValidationException($"Incorrect Id={id}", "id");
            }
            var userToSend = new UserDTO()
            {
                Login = userToGet.Login,
                Name = userToGet.Name
            };
            return userToSend;
        }
        
        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}
