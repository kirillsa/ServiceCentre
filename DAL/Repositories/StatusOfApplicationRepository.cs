using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DAL.Identity
{
    public class StatusOfApplicationRepository : IRepository<StatusOfApplication>
    {
        private ServiceCentreDBContext _db;

        public StatusOfApplicationRepository(ServiceCentreDBContext context)
        {
            _db = context;
        }

        public void Create(StatusOfApplication item)
        {
            _db.Statuses.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _db.Statuses.Find(id);
            if (entity != null)
            {
                _db.Statuses.Remove(entity);
            }
        }

        public StatusOfApplication Read(int id)
        {
            return _db.Statuses.Find(id);
        }

        public IEnumerable<StatusOfApplication> ReadAll()
        {
            return _db.Statuses;
        }

        public void Update(StatusOfApplication item)
        {
            _db.Statuses.AddOrUpdate(item);
        }
    }
}