using Microsoft.EntityFrameworkCore;
using Pishkhan_LifeInsurance.Data;
using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Models.WageViewModels;
using Pishkhan_LifeInsurance.Extensions;

namespace Pishkhan_LifeInsurance.Repositories
{
    public class TblKarmozdRepository : Repository<TblKarmozd>, ITblKarmozdRepository
    {
        private readonly ApplicationDbContext _db;
        public TblKarmozdRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<TblKarmozd>> AgentReport(string AgentID, int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.AgentID == AgentID && a.Year == Year && a.Month == Month)
                .Include(c => c.TblLifeInsurance)
                .Include(d => d.TblUser)
                .OrderByDescending(b => b.TblLifeInsurance.InsuranceNumber_Number)
                .ToListAsync();

            return model;
        }

        public async Task<bool> CheckIfBeforSubmitThisKarmozd(string AgentID, int Month, int Year)
        {
            var result = await _db.TblKarmozd.AnyAsync(a => a.AgentID == AgentID && a.Month == Month && a.Year == Year);

            return result;
        }

        public async Task<bool> FindDoubleInsuranceById(int id)
        {
            var _count = _db.TblKarmozd.Where(a => a.TblLifeInsuranceID == id).ToListAsync();

            var count = await _count;

            if (count.Count != 0)
            {
                return true;
            }

            else { return false; }


            //var result = await _db.TblKarmozd.AnyAsync(a => a.TblLifeInsuranceID == id);
            //return await _db.TblKarmozd.AnyAsync(a => a.TblLifeInsuranceID == id);
        }

        public IQueryable<TblKarmozd> GetListOfWageGroupByAgentID()
        {
            return _db.TblKarmozd
                .OrderByDescending(a => a.Month)
                .ThenByDescending(b => b.Year)
                .Include(i => i.TblUser)
                .AsQueryable();
        }

        public IQueryable<TblKarmozd> GetListOfWageSortByMonth_Year_AgentID(string AgentID, int Year, int Month)
        {
            return _db.TblKarmozd.Where(a => a.AgentID == AgentID && a.Year == Year && a.Month == Month).AsQueryable();
        }

        public async Task<List<TblKarmozd>> InsuranceReportDoublePrice(int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.Year == Year && a.Month == Month && a.isDouble == true)
                .Include(b => b.TblLifeInsurance)
                .OrderByDescending(c => c.TblLifeInsurance.InsuranceNumber_Number)
                .ThenByDescending(d => d.TblLifeInsurance.Date_Soodoor_Miladi)
                .ToListAsync();

            return model;
        }

        public async Task<List<TblKarmozd>> InsuranceReportFirstPrice(int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.Year == Year && a.Month == Month && a.isDouble == false)
                 .Include(b => b.TblLifeInsurance)
                 .OrderByDescending(c => c.TblLifeInsurance.InsuranceNumber_Number)
                 .ThenByDescending(d => d.TblLifeInsurance.Date_Soodoor_Miladi)
                 .ToListAsync();

            return model;
        }

        public async Task<List<TblLifeInsurance>> InsuranceReportNoPricePrice(int Year, int Month)
        {
            var startDateShamsi = Year + "/" + Month + "/" + "1";
            var startDateMiladi = startDateShamsi.DateToMiladi();

            var endDateMiladi = startDateMiladi.AddMonths(1);

            var lst = await _db.TblLifeInsurance.Where(a => a.Date_Soodoor_Miladi >= startDateMiladi && a.Date_Soodoor_Miladi < endDateMiladi).ToListAsync();

            var model = new List<TblLifeInsurance>();

            foreach (var item in lst)
            {
                if (!_db.TblKarmozd.Any(a => a.TblLifeInsuranceID == item.ID))
                {
                    model.Add(item);
                }
            }

            return model;
        }

        public async Task<List<TblKarmozd>> PishkhanReportDoublePrice(int PishkhanID, int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.TblLifeInsurance.PishkhanID == PishkhanID && a.Year == Year && a.Month == Month && a.isDouble == true)
                .OrderByDescending(b => b.TblLifeInsurance.InsuranceNumber_Number)
                .Include(c => c.TblLifeInsurance)
                .ToListAsync();

            return model;
        }

        public async Task<List<TblKarmozd>> PishkhanReportFirstPrice(int PishkhanID, int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.TblLifeInsurance.PishkhanID == PishkhanID && a.Year == Year && a.Month == Month && a.isDouble == false)
                .OrderByDescending(b => b.TblLifeInsurance.InsuranceNumber_Number)
                .Include(c => c.TblLifeInsurance)
                .ToListAsync();

            return model;
        }

        public async Task<List<TblLifeInsurance>> PishkhanReportNoPricePrice(int PishkhanID, int Year, int Month)
        {
            var startDateShamsi = Year + "/" + Month + "/" + "1";
            var startDateMiladi = startDateShamsi.DateToMiladi();

            var endDateMiladi = startDateMiladi.AddMonths(1);

            var lst = await _db.TblLifeInsurance.Where(a => a.Date_Soodoor_Miladi >= startDateMiladi && a.Date_Soodoor_Miladi < endDateMiladi && a.PishkhanID == PishkhanID).ToListAsync();

            var model = new List<TblLifeInsurance>();

            foreach (var item in lst)
            {
                if (!_db.TblKarmozd.Any(a => a.TblLifeInsuranceID == item.ID))
                {
                    model.Add(item);
                }
            }

            return model;
        }

        public async Task<List<TblKarmozd>> SupervisiorReportDoublePrice(int SupervisiorID, int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.TblLifeInsurance.SupervisorID == SupervisiorID && a.Year == Year && a.Month == Month && a.isDouble == true)
                        .OrderByDescending(b => b.TblLifeInsurance.InsuranceNumber_Number)
                        .Include(c => c.TblLifeInsurance)
                        .ToListAsync();

            return model;
        }

        public async Task<List<TblKarmozd>> SupervisiorReportFirstPrice(int SupervisiorID, int Year, int Month)
        {
            var model = await _db.TblKarmozd.Where(a => a.TblLifeInsurance.SupervisorID == SupervisiorID && a.Year == Year && a.Month == Month && a.isDouble == false)
                        .OrderByDescending(b => b.TblLifeInsurance.InsuranceNumber_Number)
                        .Include(c => c.TblLifeInsurance)
                        .ToListAsync();

            return model;
        }

        public async Task<List<TblLifeInsurance>> SupervisiorReportNoPricePrice(int SupervisiorID, int Year, int Month)
        {
            var startDateShamsi = Year + "/" + Month + "/" + "1";
            var startDateMiladi = startDateShamsi.DateToMiladi();

            var endDateMiladi = startDateMiladi.AddMonths(1);

            var lst = await _db.TblLifeInsurance.Where(a => a.Date_Soodoor_Miladi >= startDateMiladi && a.Date_Soodoor_Miladi < endDateMiladi && a.SupervisorID == SupervisiorID).ToListAsync();

            var model = new List<TblLifeInsurance>();

            foreach (var item in lst)
            {
                if (!_db.TblKarmozd.Any(a => a.TblLifeInsuranceID == item.ID))
                {
                    model.Add(item);
                }
            }

            return model;
        }


    }
}
