using System;

namespace DAL.DBContext.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public DateTime DateOfCreateApplication { get; set; }

        public int UserOwnerId { get; set; }
        public virtual User UserOwner { get; set; }

        public int StatusId { get; set; }
        public virtual StatusOfApplication Status { get; set; }

        public int? ExecutorId { get; set; }
        public virtual User Executor { get; set; }

        public DateTime DateOfChangeStatus { get; set; }

        public Application()
        {
            DateOfCreateApplication = DateTime.Now;
        }
    }
}

