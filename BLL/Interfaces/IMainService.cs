using System;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IMainService : IDisposable
    {
        IUserService UserServices { get; }
        IRepositoryBll<RoleDTO> RoleServices { get; }
        IRepositoryBll<StatusDTO> StatusServices { get; }
        IRepositoryBll<ApplicationDTO> ApplicationServices { get; }
        void Save();
    }
}
