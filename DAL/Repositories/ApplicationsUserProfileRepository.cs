using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL.Identity
{
    public class ApplicationsUserProfileRepository : IRepository<ApplicationUserProfile>
    {
        ServiceCentreDBContext _db;

        public ApplicationsUserProfileRepository(ServiceCentreDBContext context)
        {
            _db = context;
        }

        public IEnumerable<ApplicationUserProfile> Find(Func<ApplicationUserProfile, Boolean> predicate)
        {
            return _db.UsersProfiles.Where(predicate).ToList();
        }

        public void Create(ApplicationUserProfile item)
        {
            _db.UsersProfiles.Add(item);
        }

        public ApplicationUserProfile Read(string id)
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

        public void Delete(string id)
        {
            var entity = _db.UsersProfiles.Find(id);
            if (entity != null)
            {
                _db.UsersProfiles.Remove(entity);
            }
        }
    }
}