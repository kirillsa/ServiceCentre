using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using DAL.Interfaces;
using DAL.DBContext.Models;
using BLL.Infrastructure;

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
            try
            {
                var newRole = new ApplicationRole()
                {
                    Name = item.Name
                };
                _dataBase.RoleManager.CreateAsync(newRole);
                _dataBase.SaveAsync();
            }
            catch (Exception)
            {
                throw new ValidationException("Error while creating a Role", "");
            }
        }

        public IEnumerable<RoleDTO> GetAll()
        {
            var list = new List<RoleDTO>();
            foreach (var item in _dataBase.RoleManager.Roles)
            {
                var newRole = new RoleDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                };
            }
            return list;
        }

        public RoleDTO Get(int id)
        {
            ApplicationRole roleToGet = _dataBase.RoleManager.Roles.FirstOrDefault(x => x.Id == id.ToString());
            if (roleToGet == null)
            {
                throw new ValidationException("Invalid role id", "");
            }
            return new RoleDTO()
            {
                Name = roleToGet.Name
            };
        }

        public void Delete(int id)
        {
            var roleToDelete = _dataBase.RoleManager.Roles.FirstOrDefault(x => x.Id == id.ToString());
            if (roleToDelete == null)
            {
                throw new ValidationException("Invalid input Role ID", "");
            }
            try
            {
                _dataBase.RoleManager.DeleteAsync(roleToDelete);
                _dataBase.SaveAsync();
            }
            catch
            {
                throw new ValidationException("Error while deleting Role", "");
            }
        }

        public void Edit(RoleDTO item)
        {
            if (item == null)
            {
                throw new ValidationException("Invalid input Role", "");
            }
            try
            {
                _dataBase.RoleManager.UpdateAsync(new ApplicationRole() { Name = item.Name });
                _dataBase.SaveAsync();
            }
            catch(Exception)
            {
                throw new ValidationException("Error while editing Role", "");
            }
        }
    }
}
