using System;

namespace BLL.DTO
{
    public class ApplicationDTO
    {
        public string Id { get; set; }

        public DateTime DateOfCreateApplication { get; set; }

        public string ApplicationName { get; set; }

        public string UserOwnerId { get; set; }

        public string StatusId { get; set; }

        public string ExecutorId { get; set; }

        public DateTime DateOfChangeStatus { get; set; }
    }
}
