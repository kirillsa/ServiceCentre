using System.ComponentModel.DataAnnotations;

namespace PrL.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}