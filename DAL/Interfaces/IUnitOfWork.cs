using DAL.DBContext.Models;
using DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Application> Applications { get; }
        IRepository<StatusOfApplication> Statuses { get; }
        IRepository<ApplicationUserProfile> UsersProfiles { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
