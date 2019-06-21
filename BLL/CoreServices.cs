using DAL.Interfaces;
using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.DBContext.Models;
using System.Threading.Tasks;
using BLL.Services;

namespace BLL
{
    public class CoreServices : IMainService
    {
        public IUnitOfWork DataBase { get; private set; }

        private IUserService _userService { get; }
        private IRepositoryBll<RoleDTO> _roleService { get; }
        private IRepositoryBll<ApplicationDTO> _applicationService { get; }
        private IRepositoryBll<StatusDTO> _statusService { get; }
        private bool _disposed = false;

        //public CoreServices()
        //{
        //    DataBase = new DAL.UOW();
        //    _userService = new UserService(DataBase);
        //    _roleService = new RoleService(DataBase);
        //    _applicationService = new ApplicationService(DataBase);
        //    _statusService = new StatusService(DataBase);
        //}

        public CoreServices(IUnitOfWork uow)
        {
            DataBase = uow;
            _userService = new UserService(DataBase);
            _roleService = new RoleService(DataBase);
            _applicationService = new ApplicationService(DataBase);
            _statusService = new StatusService(DataBase);
        }

        public IUserService UserServices
        {
            get { return _userService; }
        }

        public IRepositoryBll<RoleDTO> RoleServices
        {
            get { return _roleService; }
        }

        public IRepositoryBll<ApplicationDTO> ApplicationServices
        {
            get { return _applicationService; }
        }

        public IRepositoryBll<StatusDTO> StatusServices
        {
            get { return _statusService; }
        }

        public void Save()
        {
            DataBase.Save();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    DataBase.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
