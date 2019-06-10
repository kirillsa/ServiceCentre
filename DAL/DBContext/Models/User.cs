using System.Collections.Generic;

namespace DAL.DBContext.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public User()
        {
            Applications = new List<Application> ();
        }
    }
}