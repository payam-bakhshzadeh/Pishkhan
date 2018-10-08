using Pishkhan_LifeInsurance.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Repositories;
using Pishkhan_LifeInsurance.Models.WageViewModels;

namespace Pishkhan_LifeInsurance.Services
{
    public interface ITblKarmozdRepository : IRepository<TblKarmozd>
    {
        Task<bool> FindDoubleInsuranceById(int id);

        IQueryable<TblKarmozd> GetListOfWageGroupByAgentID();

        IQueryable<TblKarmozd> GetListOfWageSortByMonth_Year_AgentID(string AgentID, int Year, int Month);

        Task<List<TblKarmozd>> InsuranceReportFirstPrice(int Year , int Month);
        Task<List<TblKarmozd>> InsuranceReportDoublePrice(int Year , int Month);
        Task<List<TblLifeInsurance>> InsuranceReportNoPricePrice(int Year , int Month);

        Task<List<TblKarmozd>> AgentReport(string AgentID, int Year, int Month);

        Task<List<TblKarmozd>> PishkhanReportFirstPrice(int PishkhanID, int Year, int Month);
        Task<List<TblKarmozd>> PishkhanReportDoublePrice(int PishkhanID, int Year, int Month);
        Task<List<TblLifeInsurance>> PishkhanReportNoPricePrice(int PishkhanID, int Year, int Month);

        Task<List<TblKarmozd>> SupervisiorReportFirstPrice(int SupervisiorID, int Year, int Month);
        Task<List<TblKarmozd>> SupervisiorReportDoublePrice(int SupervisiorID, int Year, int Month);
        Task<List<TblLifeInsurance>> SupervisiorReportNoPricePrice(int SupervisiorID, int Year, int Month);

        Task<bool> CheckIfBeforSubmitThisKarmozd(string AgentID, int Month, int Year);




    }
}
