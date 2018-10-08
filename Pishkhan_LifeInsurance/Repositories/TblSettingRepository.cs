using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Data;
using Microsoft.EntityFrameworkCore;

namespace Pishkhan_LifeInsurance.Repositories
{
    public class TblSettingRepository : Repository<TblSetting>, ITblSettingRepository
    {
        private readonly ApplicationDbContext _db;
        public TblSettingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<TblSetting> GetFirst()
        {
            try
            {
                return await  _db.TblSetting.FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
            
        }
    }
}
