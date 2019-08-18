using DAL.Interfaces;
using System;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Services;

namespace BLL
{
    public class CoreServices : IMainService
    {
        private IUnitOfWork _dataBase;
        private IUserService _userService;
        private IRepositoryBll<RoleDTO> _roleService;
        private IRepositoryBll<ApplicationDTO> _applicationService;
        private IRepositoryBll<StatusDTO> _statusService;
        private bool _disposed = false;

        public CoreServices(IUnitOfWork uow)
        {
            _dataBase = uow;
        }

        public IUserService UserServices
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService(_dataBase);
                }
                return _userService;
            }
        }

        public IRepositoryBll<RoleDTO> RoleServices
        {
            get
            {
                if (_roleService == null)
                {
                    _roleService = new RoleService(_dataBase);
                }
                return _roleService;
            }
        }

        public IRepositoryBll<ApplicationDTO> ApplicationServices
        {
            get
            {
                if (_applicationService == null)
                {
                    _applicationService = new ApplicationService(_dataBase);
                }
                return _applicationService;
            }
        }

        public IRepositoryBll<StatusDTO> StatusServices
        {
            get
            {
                if (_statusService == null)
                {
                    _statusService = new StatusService(_dataBase);
                }
                return _statusService;
            }
        }

        public void Save()
        {
            _dataBase.Save();
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
