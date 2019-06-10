using System.Data.Entity.ModelConfiguration;

namespace DAL.DBContext.Models.Config
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Login).IsRequired().HasMaxLength(30);
        }
    }
}
