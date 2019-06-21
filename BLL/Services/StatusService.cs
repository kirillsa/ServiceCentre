using System;
using System.Collections.Generic;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.DBContext.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class StatusService : IRepositoryBll<StatusDTO>
    {
        private IUnitOfWork _dataBase;

        public StatusService(IUnitOfWork db)
        {
            _dataBase = db;
        }

        public void Create(StatusDTO item)
        {
            if (item == null)
            {
                throw new ValidationException("Invalid input status", "");
            }
            try
            {
                var newStatus = new StatusOfApplication()
                {
                    Name = item.Name
                };
                _dataBase.Statuses.Create(newStatus);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while creating new status", "");
            }
        }

        public IEnumerable<StatusDTO> GetAll()
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

        public StatusDTO Get(string id)
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

        public void Edit(StatusDTO item)
        {
            if (item == null)
            {
                throw new ValidationException("Invalid input status", "");
            }
            try
            {
                var statusToEdit = new StatusOfApplication()
                {
                    Name = item.Name
                };
                _dataBase.Statuses.Create(statusToEdit);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while editing status", "");
            }
        }

        public void Delete(string id)
        {
            try
            {
                _dataBase.Statuses.Delete(id);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while deleting status", "");
            }
        }
    }
}
