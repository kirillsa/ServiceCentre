using DAL.DBContext.Models;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Application> Applications { get; }
        IRepository<StatusOfApplication> Statuses { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
