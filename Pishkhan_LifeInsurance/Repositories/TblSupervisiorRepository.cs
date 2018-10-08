using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Data;

namespace Pishkhan_LifeInsurance.Repositories
{
    public class TblSupervisiorRepository : Repository<TblSupervisior>, ITblSupervisiorRepository
    {
        public TblSupervisiorRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
