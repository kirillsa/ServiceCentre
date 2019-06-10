using System.Data.Entity.ModelConfiguration;

namespace DAL.DBContext.Models.Config
{
    class ApplicationConfig : EntityTypeConfiguration<Application>
    {
        public ApplicationConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.ApplicationName).IsRequired().HasMaxLength(200);
            Property(x => x.DateOfCreateApplication).IsRequired();
            HasRequired(x => x.UserOwner).WithMany(x => x.Applications).WillCascadeOnDelete(false);
            HasRequired(x => x.Status);
        }
    }
}
