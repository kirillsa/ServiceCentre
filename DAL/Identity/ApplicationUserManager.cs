using DAL.DBContext.Models;
using Microsoft.AspNet.Identity;

namespace DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        { }
    }
}
