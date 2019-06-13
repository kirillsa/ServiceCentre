using BLL.DTO;
using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICoreServices : IDisposable
    {
        void CreateApplication(ApplicationDTO applicationDTO);
        IEnumerable<ApplicationDTO> GetAllApplications();
        ApplicationDTO GetApplication(int id);
        void EditApplication(ApplicationDTO applicationDTO);
        void DeleteApplication(int id);

        void CreateUser(UserDTO userDTO);
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUser(int id);
        void EditUser(UserDTO userDTO);

        IEnumerable<StatusDTO> GetAllStatuses();
        StatusDTO GetStatus(int id);
    }
}
