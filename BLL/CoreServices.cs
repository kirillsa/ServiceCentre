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
        private IUnitOfWork _dataBase;

        private IUserService _userService { get; }
        private IRepositoryBll<RoleDTO> _roleService { get; }
        private IRepositoryBll<ApplicationDTO> _applicationService { get; }
        private IRepositoryBll<StatusDTO> _statusService { get; }
        private bool _disposed = false;

        public CoreServices(IUnitOfWork uow)
        {
            _dataBase = uow;
            _userService = new UserService(_dataBase);
            _roleService = new RoleService(_dataBase);
            _applicationService = new ApplicationService(_dataBase);
            _statusService = new StatusService(_dataBase);
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

        public async Task SaveAsync()
        {
            await _dataBase.SaveAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dataBase.Dispose();
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
