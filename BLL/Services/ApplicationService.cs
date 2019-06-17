using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using DAL.Interfaces;
using BLL.Infrastructure;
using DAL.DBContext.Models;

namespace BLL.Services
{
    public class ApplicationService : IRepositoryBll<ApplicationDTO>
    {
        private IUnitOfWork _dataBase;

        public ApplicationService(IUnitOfWork db)
        {
            _dataBase = db;
        }

        public void Create(ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                throw new ValidationException("Invalid input Application", "");
            }
            var owner = _dataBase.UsersProfiles.Read(applicationDTO.UserOwnerId);
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
                _dataBase.SaveAsync();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while Adding new Application", "");
            }
        }

        public IEnumerable<ApplicationDTO> GetAll()
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

        public ApplicationDTO Get(int id)
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

        public void Edit(ApplicationDTO applicationDTO)
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
                _dataBase.SaveAsync();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while Updating an Application", "");
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dataBase.Applications.Delete(id);
                _dataBase.SaveAsync();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while deleting application", "");
            }
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}
