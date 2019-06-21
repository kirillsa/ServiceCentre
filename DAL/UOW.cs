using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace DAL
{
    public class UOW : IUnitOfWork
    {
        private ServiceCentreDBContext _db;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IRepository<Application> _applicationRepository;
        private IRepository<StatusOfApplication> _statusRepository;
        private IRepository<ApplicationUserProfile> _userRepository;
        private bool _disposed = false;

        public UOW()
        {
            _db = new ServiceCentreDBContext();
        }

        public IRepository<Application> Applications
        {
            get
            {
                if (_applicationRepository == null)
                {
                    _applicationRepository = new ApplicationRepository(_db);
                }
                return _applicationRepository;
            }
        }

        public IRepository<StatusOfApplication> Statuses
        {
            get
            {
                if (_statusRepository == null)
                {
                    _statusRepository = new StatusOfApplicationRepository(_db);
                }
                return _statusRepository;
            }
        }

        public IRepository<ApplicationUserProfile> UsersProfiles
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new ApplicationsUserProfileRepository(_db);
                }
                return _userRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                }
                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));
                }
                return _roleManager;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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