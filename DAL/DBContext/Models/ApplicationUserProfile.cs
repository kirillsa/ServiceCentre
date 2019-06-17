using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DBContext.Models
{
    public class ApplicationUserProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public ApplicationUserProfile()
        {
            Applications = new List<Application> ();
        }
    }
}