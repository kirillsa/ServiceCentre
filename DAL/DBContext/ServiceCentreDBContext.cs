using DAL.DBContext.Models;
using DAL.DBContext.Models.Config;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAL.DBContext
{
    public class ServiceCentreDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationUserProfile> UsersProfiles { get; set; }
        public DbSet<StatusOfApplication> Statuses { get; set; }

        static ServiceCentreDBContext()
        {
            Database.SetInitializer<ServiceCentreDBContext>(new ServiceCentreDBContextInitializer());
        }

        public ServiceCentreDBContext() : base("ServiceCentreDB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add<Application>(new ApplicationConfig());
            modelBuilder.Configurations.Add<StatusOfApplication>(new StatusOfApplicationConfig());
            modelBuilder.Configurations.Add<ApplicationUserProfile>(new ApplicationUserProfileConfig());
        }
    }
}
