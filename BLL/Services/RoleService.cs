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
            throw new NotImplementedException();
        }

        public RoleDTO Get(int id)
        {
            var roleToGet = _dataBase.RoleManager.FindByIdAsync(id.ToString());
            if (roleToGet == null)
            {
                throw new ValidationException("Invalid role id", "");
            }
            return new RoleDTO()
            {
                Name = roleToGet.ToString()
            };
        }

        public void Delete(int id)
        {
            try
            {
                //var roleToDelete = _dataBase.RoleManager.FindByIdAsync(id.ToString());
                //_dataBase.RoleManager.DeleteAsync(roleToDelete);
                _dataBase.SaveAsync();
            }
            catch
            {
                throw new ValidationException("Error while deleting Role", "");
            }
        }

        public void Edit(RoleDTO item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dataBase.Dispose();
        }
    }
}
