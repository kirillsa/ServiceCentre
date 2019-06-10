using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        ServiceCentreDBContext _db;

        public UserRepository(ServiceCentreDBContext context)
        {
            _db = context;
        }

        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _db.Users.Find(id);
            if (entity != null)
            {
                _db.Users.Remove(entity);
            }
        }

        public User Read(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<User> ReadAll()
        {
            return _db.Users;
        }

        public void Update(User item)
        {
            _db.Users.AddOrUpdate(item);
        }
    }
}