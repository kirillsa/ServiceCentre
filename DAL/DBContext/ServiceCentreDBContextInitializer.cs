using System.Collections.Generic;
using System.Data.Entity;
using DAL.DBContext.Models;

namespace DAL.DBContext
{
    public class ServiceCentreDBContextInitializer : DropCreateDatabaseIfModelChanges<ServiceCentreDBContext>
    {
        protected override void Seed(ServiceCentreDBContext db)
        {
            StatusOfApplication status1 = new StatusOfApplication() { Name = "New" };
            StatusOfApplication status2 = new StatusOfApplication() { Name = "In Progress" };
            StatusOfApplication status3 = new StatusOfApplication() { Name = "Completed" };
            db.Statuses.AddRange(new List<StatusOfApplication>() { status1, status2, status3 });

            /*User user1 = new User() { Login = "user1", Name = "user1Name" };
            User user2 = new User() { Login = "user2", Name = "user2Name" };
            User user3 = new User() { Login = "user3", Name = "user3Name" };
            User user4 = new User() { Login = "user4", Name = "user4Name" };
            db.Users.AddRange(new List<User>() { user1, user1, user3 });

            Application application1 = new Application() { ApplicationName = "app1", Status = status1, UserOwner = user3 };
            Application application2 = new Application() { ApplicationName = "app2", Status = status1, UserOwner = user3 };
            Application application3 = new Application() { ApplicationName = "app3", Status = status1, UserOwner = user4 };
            db.Applications.AddRange(new List<Application>() { application1, application2, application3});
            */
            db.SaveChanges();
            base.Seed(db);
        }
    }
}