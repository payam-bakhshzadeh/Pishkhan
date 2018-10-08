using Pishkhan_LifeInsurance.Data;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Repositories
{
    public class Service : IService
    {
        private readonly ApplicationDbContext _db;
        public TblLifeInsuranceRepository TblLifeInsurance { get; set; }
        public ITblPishkhanRepository TblPishkhan { get; set; }
        public ITblSupervisiorRepository TblSupervisior { get; set; }
        public ITblSettingRepository TblSetting { get; set; }
        public ITblKarmozdRepository TblKarmozd { get; set ; }

        public Service(ApplicationDbContext db)
        {
            _db = db;

            TblLifeInsurance = new TblLifeInsuranceRepository(_db);
            TblPishkhan = new TblPishkhanRepository(_db);
            TblSupervisior = new TblSupervisiorRepository(_db);
            TblKarmozd = new TblKarmozdRepository(_db);
            TblSetting = new TblSettingRepository(_db);

        }

        public async Task<int> Complete()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch 
            {

                return 0;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
