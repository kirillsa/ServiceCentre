using DAL.DBContext.Models;
using DAL.DBContext.Models.Config;
using System.Data.Entity;

namespace DAL.DBContext
{
    public class ServiceCentreDBContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<StatusOfApplication> Statuses { get; set; }

        static ServiceCentreDBContext()
        {
            Database.SetInitializer<ServiceCentreDBContext>(new ServiceCentreDBContextInitializer());
        }

        public ServiceCentreDBContext() : base("ServiceCentreDB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Application>(new ApplicationConfig());
        }
    }
}
