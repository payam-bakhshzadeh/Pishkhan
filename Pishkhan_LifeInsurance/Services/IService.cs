using Pishkhan_LifeInsurance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Services
{
    public interface IService : IDisposable
    {
        Task<int> Complete();

        TblLifeInsuranceRepository TblLifeInsurance { get; set; }
        ITblPishkhanRepository TblPishkhan { get; set; }
        ITblSupervisiorRepository TblSupervisior { get; set; }
        ITblSettingRepository TblSetting { get; set; }
        ITblKarmozdRepository TblKarmozd { get; set; }
  
    }
}
