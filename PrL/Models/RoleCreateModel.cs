using System.ComponentModel.DataAnnotations;

namespace PrL.Models
{
    public class RoleCreateModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}