using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Models.InsuranceListViewModels;
using Pishkhan_LifeInsurance.Models.WageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pishkhan_LifeInsurance.Services
{
    public interface ITblLifeInsuranceRepository : IRepository<TblLifeInsurance>
    {
        IQueryable<TblLifeInsurance> GetInsuranceList(bool Filter, int Skip, int Take, FilterItemsViewModel FilterItems);

        Task<int> GetTotalCount();

        Task<bool> CheckAny_InsuranceNumber_InsuranceSerial(int number, int serial);

        Task<List<SmallLifeInsuranceViewModel>> GetAllInsuranceForCompareToExcelFile(string agentID);

       
    }


}
