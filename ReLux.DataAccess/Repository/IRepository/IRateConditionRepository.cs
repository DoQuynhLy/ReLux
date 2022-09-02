using ReLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReLux.DataAccess.Repository.IRepository
{
    public interface IRateConditionRepository : IRepository<RateCondition>
    {
        void Update(RateCondition obj);
        void Save();
    }
}
