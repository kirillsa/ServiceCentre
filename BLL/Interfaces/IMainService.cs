using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IMainService : IDisposable
    {
        IRepositoryBll<StatusDTO> StatusServices { get; }
        IRepositoryBll<ApplicationDTO> ApplicationServices { get; }
        IUserService UserServices { get; }
        IRepositoryBll<RoleDTO> RoleServices { get; }
        Task SaveAsync();
    }
}
