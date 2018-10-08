using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pishkhan_LifeInsurance.Data;
using Pishkhan_LifeInsurance.Models.InsuranceListViewModels;
using Microsoft.EntityFrameworkCore;
using Pishkhan_LifeInsurance.Extensions;
using Pishkhan_LifeInsurance.Models.WageViewModels;

namespace Pishkhan_LifeInsurance.Repositories
{
    public class TblLifeInsuranceRepository : Repository<TblLifeInsurance>, ITblLifeInsuranceRepository
    {
        private readonly ApplicationDbContext _db;
        public TblLifeInsuranceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<TblLifeInsurance> GetInsuranceList(bool Filter, int Skip, int Take, FilterItemsViewModel FilterItems)
        {
            //User Filter The Tabel
            if (Filter)
            {

                var result = GetFilterList(FilterItems);
                return result;
            }

            //User Get All Table
            else
            {

                var result = _db.TblLifeInsurance.OrderByDescending(o => o.InsuranceNumber_Number).Skip(Skip).Take(Take)
                    .Include(i => i.TblUser)
                    .Include(i => i.TblPishkhan)
                    .Include(i => i.TblSupervisior)
                    .AsQueryable();

                return result;
            }
        }

        public async Task<int> GetTotalCount()
        {
            return await _db.TblLifeInsurance.CountAsync();
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        public IQueryable<TblLifeInsurance> GetFilterList(FilterItemsViewModel filter)
        {
            //کاربر بر اساس نمایندگی جستجو میکند
            if (filter.AgentID != null && filter.AgentID!= 0.ToString())
            {

                //بررسی اینکه آیا بر اساس تاریخ شروع بیمه نامه هم فیلتر اعمال کرده یا نه

                //کاربر بر اساس تاریخ هم فیلتر کرده
                if (filter.FromDate_Start != null || filter.ToDate_Start != null)
                {

                    if (filter.FromDate_Start != null && filter.ToDate_Start == null)
                    {
                        try
                        {
                            var date = filter.FromDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.UserID == filter.AgentID && a.Date_Start_Miladi >= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch {

                            throw new Exception();
                        }


                    }

                    else if (filter.FromDate_Start == null && filter.ToDate_Start != null)
                    {
                        try
                        {
                            var date = filter.ToDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.UserID == filter.AgentID && a.Date_Start_Miladi <= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }
                    }

                    else {

                        try
                        {
                            var fromDate = filter.FromDate_Start.DateToMiladi();
                            var toDate = filter.ToDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.UserID == filter.AgentID && a.Date_Start_Miladi >= fromDate && a.Date_Start_Miladi <= toDate).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }
                    }

                }

                //کاربر تاریخی وارد نکرده
                else
                {
                    var result = _db.TblLifeInsurance.Where(a => a.UserID == filter.AgentID).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }

            }

            //کاربر بر اساس زیرگروه جستجو میکند
            else if (filter.SupervisiorID != 0 && filter.SupervisiorID != null)
            {

                //بررسی اینکه آیا بر اساس تاریخ شروع بیمه نامه هم فیلتر اعمال کرده یا نه

                //کاربر بر اساس تاریخ هم فیلتر کرده
                if (filter.FromDate_Start != null || filter.ToDate_Start != null)
                {

                    if (filter.FromDate_Start != null && filter.ToDate_Start == null)
                    {
                        try
                        {
                            var date = filter.FromDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.SupervisorID == filter.SupervisiorID && a.Date_Start_Miladi >= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }


                    }

                    else if (filter.FromDate_Start == null && filter.ToDate_Start != null)
                    {
                        try
                        {
                            var date = filter.ToDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.SupervisorID == filter.SupervisiorID && a.Date_Start_Miladi <= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }
                    }

                    else
                    {

                        try
                        {
                            var fromDate = filter.FromDate_Start.DateToMiladi();
                            var toDate = filter.ToDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.SupervisorID == filter.SupervisiorID && a.Date_Start_Miladi >= fromDate && a.Date_Start_Miladi <= toDate).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }
                    }

                }

                //کاربر تاریخی وارد نکرده
                else
                {
                    var result = _db.TblLifeInsurance.Where(a => a.SupervisorID == filter.SupervisiorID).OrderByDescending(o => o.InsuranceNumber_Number)
                    .Include(i => i.TblUser)
                    .Include(i => i.TblPishkhan)
                    .Include(i => i.TblSupervisior)
                    .AsQueryable();

                    return result;
                }
            }

            //کاربر بر اساس دفاتر پیشخوان جستجو میکند
            else if (filter.PishkhanID != 0 && filter.PishkhanID != null)
            {

                //بررسی اینکه آیا بر اساس تاریخ شروع بیمه نامه هم فیلتر اعمال کرده یا نه

                //کاربر بر اساس تاریخ هم فیلتر کرده
                if (filter.FromDate_Start != null || filter.ToDate_Start != null)
                {

                    if (filter.FromDate_Start != null && filter.ToDate_Start == null)
                    {
                        try
                        {
                            var date = filter.FromDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.PishkhanID == filter.PishkhanID && a.Date_Start_Miladi >= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }


                    }

                    else if (filter.FromDate_Start == null && filter.ToDate_Start != null)
                    {
                        try
                        {
                            var date = filter.ToDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.PishkhanID == filter.PishkhanID && a.Date_Start_Miladi <= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }
                    }

                    else
                    {

                        try
                        {
                            var fromDate = filter.FromDate_Start.DateToMiladi();
                            var toDate = filter.ToDate_Start.DateToMiladi();

                            var result = _db.TblLifeInsurance.Where(a => a.PishkhanID == filter.PishkhanID && a.Date_Start_Miladi >= fromDate && a.Date_Start_Miladi <= toDate).OrderByDescending(o => o.InsuranceNumber_Number)
                                        .Include(i => i.TblUser)
                                        .Include(i => i.TblPishkhan)
                                        .Include(i => i.TblSupervisior)
                                        .AsQueryable();

                            return result;


                        }

                        catch
                        {

                            throw new Exception();
                        }
                    }

                }

                //کاربر تاریخی وارد نکرده
                else
                {
                    var result = _db.TblLifeInsurance.Where(a => a.PishkhanID == filter.PishkhanID).OrderByDescending(o => o.InsuranceNumber_Number)
                    .Include(i => i.TblUser)
                    .Include(i => i.TblPishkhan)
                    .Include(i => i.TblSupervisior)
                    .AsQueryable();

                    return result;
                }
            }

            //کاربر بر اساس شماره بیمه نامه جستجو میکند
            else if (filter.InsuranceNumber != null)
            {
                var result = _db.TblLifeInsurance.Where(a => a.InsuranceNumber_Number == filter.InsuranceNumber).OrderByDescending(o => o.InsuranceNumber_Number)
                        .Include(i => i.TblUser)
                        .Include(i => i.TblPishkhan)
                        .Include(i => i.TblSupervisior)
                        .AsQueryable();

                return result;
            }

            //کاربر بر اساس شماره سریال جستجو میکند
            else if (filter.InsuranceSerial != null)
            {
                var result = _db.TblLifeInsurance.Where(a => a.Serial == filter.InsuranceSerial).OrderByDescending(o => o.InsuranceNumber_Number)
                            .Include(i => i.TblUser)
                            .Include(i => i.TblPishkhan)
                            .Include(i => i.TblSupervisior)
                            .AsQueryable();

                return result;
            }

            //کاربر بر اساس تاریخ صدور جستجو میکند
            else if (filter.FromDate_Soodoor != null || filter.ToDate_Soodoor != null)
            {
                if (filter.FromDate_Soodoor != null && filter.ToDate_Soodoor == null)
                {

                    var date = filter.FromDate_Soodoor.DateToMiladi();

                    var result = _db.TblLifeInsurance.Where(a => a.Date_Soodoor_Miladi >= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else if (filter.FromDate_Soodoor == null && filter.ToDate_Soodoor != null)
                {

                    var date = filter.ToDate_Soodoor.DateToMiladi();

                    var result = _db.TblLifeInsurance.Where(a => a.Date_Soodoor_Miladi <= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else
                {
                    var datefrom = filter.FromDate_Soodoor.DateToMiladi();
                    var dateto = filter.ToDate_Soodoor.DateToMiladi();

                    var result = _db.TblLifeInsurance.Where(a => a.Date_Soodoor_Miladi >= datefrom && a.Date_Soodoor_Miladi <= dateto).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
            }

            //کاربر بر اساس تاریخ شروع جستجو میکند
            else if (filter.FromDate_Start != null || filter.ToDate_Start != null)
            {
                if (filter.FromDate_Start != null && filter.ToDate_Start == null)
                {

                    var date = filter.FromDate_Start.DateToMiladi();

                    var result = _db.TblLifeInsurance.Where(a => a.Date_Start_Miladi >= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else if (filter.FromDate_Start == null && filter.ToDate_Start != null)
                {

                    var date = filter.ToDate_Start.DateToMiladi();

                    var result = _db.TblLifeInsurance.Where(a => a.Date_Start_Miladi <= date).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else
                {
                    var datefrom = filter.FromDate_Start.DateToMiladi();
                    var dateto = filter.ToDate_Start.DateToMiladi();

                    var result = _db.TblLifeInsurance.Where(a => a.Date_Start_Miladi >= datefrom && a.Date_Start_Miladi <= dateto).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
            }

            //کاربر بر اساس نام بیمه گذار و بیمه شده جستجو میکند
            else if (filter.BimegozarName != null || filter.BimeshodeName != null)
            {
                if (filter.BimegozarName != null && filter.BimeshodeName == null)
                {

                    var result = _db.TblLifeInsurance.Where(a => a.Bimegozar_Name.Contains(filter.BimegozarName)).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else if (filter.BimegozarName == null && filter.BimeshodeName != null)
                {


                    var result = _db.TblLifeInsurance.Where(a => a.Bimeshode_Name.Contains(filter.BimeshodeName)).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else
                {


                    var result = _db.TblLifeInsurance.Where(a => a.Bimegozar_Name.Contains(filter.BimegozarName) && filter.BimeshodeName.Contains(filter.BimeshodeName)).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
            }

            //کاربر بر اساس شماره موبایل بیمه گذار یا بیمه شده جستجو میکند
            else if (filter.BimegozarPhone != null || filter.BimeshodePhone != null)
            {
                if (filter.BimegozarPhone != null && filter.BimeshodePhone == null)
                {


                    var result = _db.TblLifeInsurance.Where(a => a.Bimegozar_Phone == filter.BimegozarPhone).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else if (filter.BimegozarPhone == null && filter.BimeshodePhone != null)
                {

                    var result = _db.TblLifeInsurance.Where(a => a.Bimeshode_Phone == filter.BimeshodePhone).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
                else
                {

                    var result = _db.TblLifeInsurance.Where(a => a.Bimegozar_Phone == filter.BimegozarPhone && a.Bimeshode_Phone == filter.BimeshodePhone).OrderByDescending(o => o.InsuranceNumber_Number)
                                .Include(i => i.TblUser)
                                .Include(i => i.TblPishkhan)
                                .Include(i => i.TblSupervisior)
                                .AsQueryable();

                    return result;
                }
            }

            else { return null; }
        }


        //چک میکنیم که آیا قبلا بیمه نامه ای با این شماره یا سریال در دیتابیس ذخیره شده یا نه
        public async Task<bool> CheckAny_InsuranceNumber_InsuranceSerial(int number, int serial)
        {
            return await _db.TblLifeInsurance.AnyAsync(a => a.InsuranceNumber_Number == number || a.Serial == serial);
        }

        public async Task<List<SmallLifeInsuranceViewModel>> GetAllInsuranceForCompareToExcelFile(string agentID)
        {
            var aa = await _db.TblLifeInsurance.Where(r => r.UserID == agentID).ToListAsync();
            return await _db.TblLifeInsurance.Where(b => b.UserID == agentID).Select(a => new SmallLifeInsuranceViewModel { ID = a.ID, InsuranceNumber = a.InsuranceNumber_Number, AgentID = a.UserID }).OrderByDescending(c => c.InsuranceNumber).ToListAsync();
        }
    }
}
