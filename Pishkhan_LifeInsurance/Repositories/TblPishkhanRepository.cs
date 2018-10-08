using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Data;

namespace Pishkhan_LifeInsurance.Repositories
{
    public class TblPishkhanRepository : Repository<TblPishkhan>, ITblPishkhanRepository
    {
        public TblPishkhanRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
