using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;

namespace DAL
{
    public class UOW : IUnitOfWork
    {
        private ServiceCentreDBContext _db;

        private IRepository<Application> _applicationRepository;
        private IRepository<StatusOfApplication> _statusRepository;
        private IRepository<User> _userRepository;
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

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_db);
                }
                return _userRepository;
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