using System.ComponentModel.DataAnnotations;

namespace PrL.Models
{
    public class ApplicationEditModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ApplicationName { get; set; }
        [Required]
        public string StatusId { get; set; }
        [Required]
        public string ExecutorId { get; set; }
    }
}