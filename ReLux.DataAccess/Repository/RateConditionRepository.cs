using ReLux.DataAccess.Data;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReLux.DataAccess.Repository
{
    public class RateConditionRepository : Repository<RateCondition>, IRateConditionRepository
    {
        private readonly ApplicationDbContext _db;
        public RateConditionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(RateCondition rateCondition)
        {
            var objFromDb = _db.RateCondition.FirstOrDefault(u => u.Id == rateCondition.Id);
            objFromDb.Stars = rateCondition.Stars;
            objFromDb.Description = rateCondition.Description;
        }
    }
}
