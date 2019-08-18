using System.ComponentModel.DataAnnotations;

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