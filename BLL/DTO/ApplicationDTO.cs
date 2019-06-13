using System;

namespace BLL.DTO
{
    public class ApplicationDTO
    {
        public int Id { get; set; }

        public DateTime DateOfCreateApplication { get; set; }

        public string ApplicationName { get; set; }

        public int UserOwnerId { get; set; }

        public int StatusId { get; set; }

        public int? ExecutorId { get; set; }

        public DateTime DateOfChangeStatus { get; set; }
    }
}
