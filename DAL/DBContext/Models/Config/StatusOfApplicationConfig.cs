using System.Data.Entity.ModelConfiguration;

namespace DAL.DBContext.Models.Config
{
    public class StatusOfApplicationConfig : EntityTypeConfiguration<StatusOfApplication>
    {
        public StatusOfApplicationConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(15);
        }
    }
}
