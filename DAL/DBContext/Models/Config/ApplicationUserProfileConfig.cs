using System.Data.Entity.ModelConfiguration;

namespace DAL.DBContext.Models.Config
{
    public class ApplicationUserProfileConfig : EntityTypeConfiguration<ApplicationUserProfile>
    {
        public ApplicationUserProfileConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(20);
        }
    }
}
