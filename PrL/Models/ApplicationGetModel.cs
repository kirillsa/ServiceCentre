using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrL.Models
{
    public class ApplicationGetModel
    {
        public string Id { get; set; }

        public DateTime DateOfCreationApplication { get; set; }

        public string ApplicationName { get; set; }

        public string UserOwnerId { get; set; }

        public string UserOwnerName { get; set; }

        public string StatusId { get; set; }

        public string StatusName { get; set; }

        public string ExecutorId { get; set; }

        public string ExecutorName { get; set; }

        public DateTime DateOfChangeStatus { get; set; }
    }
}