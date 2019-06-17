using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class ApplicationsUserProfileRepository : IRepository<ApplicationUserProfile>
    {
        ServiceCentreDBContext _db;

        public ApplicationsUserProfileRepository(ServiceCentreDBContext context)
        {
            _db = context;
        }

        public void Create(ApplicationUserProfile item)
        {
            _db.UsersProfiles.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _db.UsersProfiles.Find(id);
            if (entity != null)
            {
                _db.UsersProfiles.Remove(entity);
            }
        }

        public ApplicationUserProfile Read(int id)
        {
            return _db.UsersProfiles.Find(id);
        }

        public IEnumerable<ApplicationUserProfile> ReadAll()
        {
            return _db.UsersProfiles;
        }

        public void Update(ApplicationUserProfile item)
        {
            _db.UsersProfiles.AddOrUpdate(item);
        }
    }
}