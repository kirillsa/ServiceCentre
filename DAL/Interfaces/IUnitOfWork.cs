using DAL.DBContext.Models;
using DAL.Identity;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager Users { get; }
        ApplicationRoleManager Roles { get; }
        IRepository<Application> Applications { get; }
        IRepository<StatusOfApplication> Statuses { get; }
        IRepository<ApplicationUserProfile> UsersProfiles { get; }
        void Save();
    }
}
