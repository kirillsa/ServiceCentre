using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DBContext.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ApplicationUserProfile UserProfile { get; set; }
    }
}
