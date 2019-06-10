using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Repositories
{
    public class ApplicationRepository : IRepository<Application>
    {
        private ServiceCentreDBContext _db;

        public ApplicationRepository(ServiceCentreDBContext context)
        {
            _db = context;
        }

        public void Create(Application item)
        {
            _db.Applications.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _db.Applications.Find(id);
            if (entity != null)
            {
                _db.Applications.AddOrUpdate(entity);
            }
        }

        public Application Read(int id)
        {
            return _db.Applications.Find(id);
        }

        public IEnumerable<Application> ReadAll()
        {
            return _db.Applications;
        }

        public void Update(Application item)
        {
            _db.Applications.AddOrUpdate(item);
        }
    }
}