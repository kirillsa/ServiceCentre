using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.DTO;
using DAL.Interfaces;
using DAL.DBContext.Models;
using BLL.Infrastructure;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class RoleService : IRepositoryBll<RoleDTO>
    {
        private IUnitOfWork _dataBase;

        public RoleService(IUnitOfWork db)
        {
            _dataBase = db;
        }

        public void Create(RoleDTO item)
        {
            if (item == null)
            {
                throw new ValidationException("Invalid input Role", "");
            }
            var newRole = new ApplicationRole()
            {
                Name = item.Name
            };
            try
            {
                _dataBase.Roles.Create(newRole);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while creating a Role", "");
            }
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            var list = new List<RoleDTO>();
            foreach (var item in _dataBase.Roles.Roles)
            {
                var newRole = new RoleDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(newRole);
            }
            return list;
        }

        public RoleDTO Get(string id)
        {
            ApplicationRole roleToGet = _dataBase.Roles.Roles.FirstOrDefault(x => x.Id == id);
            if (roleToGet == null)
            {
                throw new ValidationException("Invalid role id", "");
            }
            return new RoleDTO()
            {
                Id = roleToGet.Id,
                Name = roleToGet.Name
            };
        }

        public IEnumerable<RoleDTO> Find(Func<RoleDTO, bool> predicate)
        {
            var list = new List<RoleDTO>();
            foreach (var item in _dataBase.Roles.Roles)
            {
                var newRole = new RoleDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(newRole);
            }
            return list.Where(predicate).ToList();
        }

        public void Edit(RoleDTO item)
        {
            if (item == null)
            {
                throw new ValidationException("Invalid input Role", "");
            }
            try
            {
                var roleToEdit = _dataBase.Roles.FindById(item.Id);
                roleToEdit.Name = item.Name;
                _dataBase.Roles.Update(roleToEdit);
                _dataBase.Save();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while editing Role", "");
            }
        }

        public void Delete(string id)
        {
            var roleToDelete = _dataBase.Roles.Roles.FirstOrDefault(x => x.Id == id);
            if (roleToDelete == null)
            {
                throw new ValidationException("Invalid input Role ID", "");
            }
            try
            {
                _dataBase.Roles.Delete(roleToDelete);
                _dataBase.Save();
            }
            catch
            {
                throw new ValidationException("Error while deleting Role", "");
            }
        }
    }
}
