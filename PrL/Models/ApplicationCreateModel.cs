using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrL.Models
{
    public class ApplicationCreateModel
    {
        [Required]
        public string ApplicationName { get; set; }
        [Required]
        public string UserOwnerId { get; set; }
    }
}