using DAL.Interfaces;
using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.DBContext.Models;

namespace BLL
{
    public class CoreServices : ICoreServices
    {
        private IUnitOfWork _dataBase;

        public CoreServices(IUnitOfWork uow)
        {
            _dataBase = uow;
        }

        public void CreateApplication(ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                throw new ValidationException("Invalid input Application", "");
            }
            var owner = _dataBase.Users.Read(applicationDTO.UserOwnerId);
            if (owner == null)
            {
                throw new ValidationException("Invalid userOwner ID", "ApplicationDTO.UserOwnerId");
            }
            try
            {
                var newApp = new Application()
                {
                    ApplicationName = applicationDTO.ApplicationName,
                    UserOwner = owner,
                    StatusId = 1,
                    DateOfChangeStatus = DateTime.Now
                };
                _dataBase.Applications.Create(newApp);
                _dataBase.Save();
            }
            catch(Exception)
            {
                throw new ValidationException("Error while Adding new Application","");
            }
        }

        public IEnumerable<ApplicationDTO> GetAllApplications()
        {
            var list = new List<ApplicationDTO>();
            foreach (var item in _dataBase.Applications.ReadAll())
            {
                var newApp = new ApplicationDTO()
                {
                    Id = item.Id,
                    DateOfChangeStatus = item.DateOfChangeStatus,
                    ApplicationName = item.ApplicationName,
                    DateOfCreateApplication = item.DateOfCreateApplication,
                    ExecutorId = item.ExecutorId,
                    StatusId = item.StatusId,
                    UserOwnerId = item.UserOwnerId
                };
                list.Add(newApp);
            }
            return list;
        }
        
        public ApplicationDTO GetApplication(int id)
        {
            var appToGet = _dataBase.Applications.Read(id);
            if (appToGet == null)
            {
                throw new ValidationException($"Incorrect Id={id}", "id");
            }
            var appToSend = new ApplicationDTO()
            {
                Id = appToGet.Id,
                DateOfChangeStatus = appToGet.DateOfChangeStatus,
                ApplicationName = appToGet.ApplicationName,
                DateOfCreateApplication = appToGet.DateOfCreateApplication,
                ExecutorId = appToGet.ExecutorId,
                StatusId = appToGet.StatusId,
                UserOwnerId = appToGet.UserOwnerId
            };
            return appToSend;
        }

        public void EditApplication(ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                throw new ValidationException("Invalid input Application", "");
            }
            try
            {
                var applicationToChange = _dataBase.Applications.Read(applicationDTO.Id);
                applicationToChange.ApplicationName = applicationDTO.ApplicationName;
                applicationToChange.StatusId = 1;
                applicationToChange.DateOfChangeStatus = DateTime.Now;
                _dataBase.Applications.Update(applicationToChange);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while Updating an Application", "");
            }
        }

        public void DeleteApplication(int id)
        {
            try
            {
                _dataBase.Applications.Delete(id);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while deleting application", "");
            }
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

        public IEnumerable<StatusDTO> GetAllStatuses()
        {
            var list = new List<StatusDTO>();
            foreach (var item in _dataBase.Statuses.ReadAll())
            {
                var newStatus = new StatusDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(newStatus);
            }
            return list;
        }

        public StatusDTO GetStatus(int id)
        {
            var statusToGet = _dataBase.Statuses.Read(id);
            if (statusToGet == null)
            {
                throw new ValidationException("Invalid id of status", "");
            }
            var statusToSend = new StatusDTO()
            {
                Name = statusToGet.Name
            };
            return statusToSend;
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}
