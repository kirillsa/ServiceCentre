using DAL.DBContext;
using DAL.DBContext.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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

        public IEnumerable<StatusOfApplication> Find(Func<StatusOfApplication, Boolean> predicate)
        {
            return _db.Statuses.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            var entity = _db.Statuses.Find(id);
            if (entity != null)
            {
                _db.Statuses.Remove(entity);
            }
        }

        public StatusOfApplication Read(string id)
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