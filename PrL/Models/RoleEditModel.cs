using System.ComponentModel.DataAnnotations;

namespace PrL.Models
{
    public class RoleEditModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}