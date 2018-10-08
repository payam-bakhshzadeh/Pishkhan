using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pishkhan_LifeInsurance.Models.ReportViewModels;
using Pishkhan_LifeInsurance.Services;
using Microsoft.AspNetCore.Identity;
using Pishkhan_LifeInsurance.Models;

namespace Pishkhan_LifeInsurance.Controllers
{
    public class ReportController : Controller
    {
        private readonly IService _service;
        private readonly ITblUserRepository _user;

        public ReportController(IService service, ITblUserRepository user)
        {
            _service = service;
            _user = user;

        }

        /// <summary>
        /// گزارش کارمزد بیمه نامه های صادره
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult InsuranceReport()
        {

            var model = new InsuranceReportGetViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> InsuranceReport(int ddlYear, int ddlMonth)
        {

            var model = new InsuranceReportPostViewModel();

            var _double = _service.TblKarmozd.InsuranceReportDoublePrice(ddlYear, ddlMonth);
            var _firstprice = _service.TblKarmozd.InsuranceReportFirstPrice(ddlYear, ddlMonth);
            var _noprice = _service.TblKarmozd.InsuranceReportNoPricePrice(ddlYear, ddlMonth);

            var Double = await _double;
            var FirstPrice = await _firstprice;
            var NoPrice = await _noprice;


            //ثبت بیمه نامه هایی که کارمزد آنها برای بار دوم واریز شده
            foreach (var item in Double)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                vm.PaymentType = item.TblLifeInsurance.PaymentType;
                vm.Price = item.TblLifeInsurance.Payment_Price;
                vm.Serial = item.TblLifeInsurance.Serial;
                vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                vm.Status = item.TblLifeInsurance.Insurance_Status;
                vm.Wage = item.Price;

                model.DoublePrice.Add(vm);
            }


            //ثبت بیمه نامه هایی که کارمزد آنها برای اولین بار واریز شده
            foreach (var item in FirstPrice)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                vm.PaymentType = item.TblLifeInsurance.PaymentType;
                vm.Price = item.TblLifeInsurance.Payment_Price;
                vm.Serial = item.TblLifeInsurance.Serial;
                vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                vm.Status = item.TblLifeInsurance.Insurance_Status;
                vm.Wage = item.Price;

                model.FirstTimePrice.Add(vm);
            }

            //ثبت بیمه نامه هایی که کارمزد آنها واریز نشده
            foreach (var item in NoPrice)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.Bimegozar_Name;
                vm.BimeshodeName = item.Bimeshode_Name;
                vm.InsuranceNumber = item.InsuranceNumber_Code + "/" + item.InsuranceNumber_CenterCode + "/" + item.InsuranceNumber_GarardadNumber + "/" + item.InsuranceNumber_Year + "/" + item.InsuranceNumber_Number;
                vm.PaymentType = item.PaymentType;
                vm.Price = item.Payment_Price;
                vm.Serial = item.Serial;
                vm.SoodoorDate = item.Date_Soodoor_Shamsi;
                vm.Status = item.Insurance_Status;
                vm.Wage = 0;

                model.NoPrice.Add(vm);
            }


            ViewBag.Year = ddlYear;
            ViewBag.Month = ddlMonth;


            return View("InsuranceReportPrint", model);
        }

        //-----------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------

        /// <summary>
        /// گزارش کارمزد نماینده
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> AgentReport()
        {
            var model = new AgentReportGetViewModel();

            var lstUser = await _user.GetAgentListAsync();

            foreach (var item in lstUser)
            {
                var vm = new smallAgent();
                vm.ID = item.Id;
                vm.Name = item.Name;

                model.Agent.Add(vm);
            }


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AgentReport(string ddlAgent, int ddlYear, int ddlMonth, int? From_InsuranceNumber, int? To_InsuranceNumber)
        {
            bool showError = false;
            //اگر شماره بیمه نامه وارد شده تا فیلتر اعمال شود باید "شماره بیمه نامه از " کوچکتر از "شماره بیمه نامه تا" باشد
            if (From_InsuranceNumber != null || To_InsuranceNumber != null)
            {
                if (From_InsuranceNumber > To_InsuranceNumber)
                {
                    ViewBag.pm = "شماره بیمه نامه برای فیلتر وارد شده باید صحیح وارد شود.از باید کوچکتر از تا باشد";
                    showError = true;
                }

                else if (From_InsuranceNumber < 0 || To_InsuranceNumber < 0)
                {
                    ViewBag.pm = "شماره بیمه نامه نمی تواند عدد منفی باشد";
                    showError = true;
                }

                else if (From_InsuranceNumber == null && To_InsuranceNumber != null)
                {
                    ViewBag.pm = "شماره بیمه نامه برای -از- حتما باید وارد شده باشد";
                    showError = true;
                }

                //اگر ارور وجود داشت این ویو را برگردان و پیام را نشان بده در غیر این صورت ادامه کد ها را برو
                if (showError)
                {
                    var newmodel = new AgentReportGetViewModel();

                    var lstUser = await _user.GetAgentListAsync();

                    foreach (var item in lstUser)
                    {
                        var vm = new smallAgent();
                        vm.ID = item.Id;
                        vm.Name = item.Name;

                        newmodel.Agent.Add(vm);
                    }


                    return View(newmodel);
                }

            }
            //کد های بالا فقط جهت اعتبار سنجی شماره بیمه نامه های وارد شده است و نه چیز دیگر. کد های اصلی در ادامه نوشته شده است
            //------------------------------------------------------------------------------------

            var model = new List<ReportTableViewModel>();



            var lst = await _service.TblKarmozd.AgentReport(ddlAgent, ddlYear, ddlMonth);

            foreach (var item in lst)
            {
                //اگر شماره بیمه نامه -از- و یا -تا- وارد شده باشد یعنی میخواهیم اطلاعات را فیلتر کنیم پس این کد ها اجرا خواهند شد
                if (From_InsuranceNumber != null && To_InsuranceNumber != null)
                {
                    //چون هر دو وارد شده پس یک بازه مد نظر است
                    if (item.TblLifeInsurance.InsuranceNumber_Number >= From_InsuranceNumber && item.TblLifeInsurance.InsuranceNumber_Number <= To_InsuranceNumber)
                    {
                        var vm = new ReportTableViewModel();

                        vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                        vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                        vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                        vm.PaymentType = item.TblLifeInsurance.PaymentType;
                        vm.Price = item.TblLifeInsurance.Payment_Price;
                        vm.Serial = item.TblLifeInsurance.Serial;
                        vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                        vm.Status = item.TblLifeInsurance.Insurance_Status;
                        vm.Wage = item.Price;

                        model.Add(vm);
                    }

                }
                else if (From_InsuranceNumber != null && To_InsuranceNumber == null)
                {
                    //یعنی از یک شکاره بیمه نامه به بعد را فیلتر کن
                    if (item.TblLifeInsurance.InsuranceNumber_Number >= From_InsuranceNumber)
                    {
                        var vm = new ReportTableViewModel();

                        vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                        vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                        vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                        vm.PaymentType = item.TblLifeInsurance.PaymentType;
                        vm.Price = item.TblLifeInsurance.Payment_Price;
                        vm.Serial = item.TblLifeInsurance.Serial;
                        vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                        vm.Status = item.TblLifeInsurance.Insurance_Status;
                        vm.Wage = item.Price;

                        model.Add(vm);
                    }
                }
                else
                {
                    //اگر شماره بیمه نامه یرای فیلتر وارد نشده باشد یعنی تمامی اطلاعات را برای من نشان بده
                    var vm = new ReportTableViewModel();

                    vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                    vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                    vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                    vm.PaymentType = item.TblLifeInsurance.PaymentType;
                    vm.Price = item.TblLifeInsurance.Payment_Price;
                    vm.Serial = item.TblLifeInsurance.Serial;
                    vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                    vm.Status = item.TblLifeInsurance.Insurance_Status;
                    vm.Wage = item.Price;

                    model.Add(vm);
                }

                //------------------------------------------------------------------

            }

            try
            {
                ViewBag.AgentName = lst.FirstOrDefault().TblUser.Name;
            }
            catch (Exception)
            { }

            ViewBag.Year = ddlYear;
            ViewBag.Month = ddlMonth;

            return View("AgentReportPrint", model);
        }



        //-----------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------

        /// <summary>
        /// گزارش دفاتر پیشحوان
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> PishkhanReport()
        {
            var model = new PishkhanReportGetViewModel();

            var lstPishkhan = await _service.TblPishkhan.GetAll();
            model.Setting = await _service.TblSetting.GetFirst();

            foreach (var item in lstPishkhan)
            {
                var vm = new smallPishkhan();
                vm.ID = item.ID;
                vm.Name = item.Name;

                model.Pishkhan.Add(vm);
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PishkhanReport(int ddlPishkhan, int ddlYear, int ddlMonth, PishkhanReportGetViewModel pishkhan)
        {
            var model = new PishkhanReportPostViewModel();
            var _firstWage = _service.TblKarmozd.PishkhanReportFirstPrice(ddlPishkhan, ddlYear, ddlMonth);
            var _doubleWage = _service.TblKarmozd.PishkhanReportDoublePrice(ddlPishkhan, ddlYear, ddlMonth);
            var _noWage = _service.TblKarmozd.PishkhanReportNoPricePrice(ddlPishkhan, ddlYear, ddlMonth);

            var _pishkhanName = _service.TblPishkhan.GetByID(ddlPishkhan);

            var firstWage = await _firstWage;
            var doubleWage = await _doubleWage;
            var noWage = await _noWage;


            var pishkhanName = await _pishkhanName;


            //ثبت بیمه نامه هایی که کارمزد آنها برای اولین بار کارمزد واریز شده
            foreach (var item in firstWage)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                vm.PaymentType = item.TblLifeInsurance.PaymentType;
                vm.Price = item.TblLifeInsurance.Payment_Price;
                vm.Serial = item.TblLifeInsurance.Serial;
                vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                vm.Status = item.TblLifeInsurance.Insurance_Status;
                vm.Wage = item.Price;

                model.FirstWage.Add(vm);
            }

            // ثبت بیمه نامه هایی که کارمزد آنها برای دومین بار واریز شده
            foreach (var item in doubleWage)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                vm.PaymentType = item.TblLifeInsurance.PaymentType;
                vm.Price = item.TblLifeInsurance.Payment_Price;
                vm.Serial = item.TblLifeInsurance.Serial;
                vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                vm.Status = item.TblLifeInsurance.Insurance_Status;
                vm.Wage = item.Price;

                model.DoubleWage.Add(vm);
            }

            // ثبت بیمه نامه هایی که برای آنها کارمزد واریز نشده
            foreach (var item in noWage)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.Bimegozar_Name;
                vm.BimeshodeName = item.Bimeshode_Name;
                vm.InsuranceNumber = item.InsuranceNumber_Code + "/" + item.InsuranceNumber_CenterCode + "/" + item.InsuranceNumber_GarardadNumber + "/" + item.InsuranceNumber_Year + "/" + item.InsuranceNumber_Number;
                vm.PaymentType = item.PaymentType;
                vm.Price = item.Payment_Price;
                vm.Serial = item.Serial;
                vm.SoodoorDate = item.Date_Soodoor_Shamsi;
                vm.Status = item.Insurance_Status;
                vm.Wage = 0;

                model.NoPriceWage.Add(vm);
            }



            model.FirstWagePercent = pishkhan.Setting.Pishkhan_Percent;
            model.DoubleWagePercent = pishkhan.Setting.Pishkhan_Percent_OldInsurance;


            ViewBag.Year = ddlYear;
            ViewBag.Month = ddlMonth;
            ViewBag.PishkhanName = pishkhanName.Name;

            return View("PishkhanReportPrint", model);
        }


        //-----------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------

        /// <summary>
        /// گزارش نمایندگان فروش
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> SubgroupReport()
        {
            var model = new SubgroupReportGetViewModel();

            var lstSubgroup = await _service.TblSupervisior.GetAll();
            model.Setting = await _service.TblSetting.GetFirst();

            foreach (var item in lstSubgroup)
            {
                var vm = new smallSubgroup();
                vm.ID = item.ID;
                vm.Name = item.Name;

                model.Subgroup.Add(vm);
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubgroupReport(int ddlSupervisior, int ddlYear, int ddlMonth, int SubgroupPercent, int SubgroupOldInsurancePercent, int SubgroupOutOfOfficePercent)
        {

            var model = new SubgroupReportPostViewModel();
            var _firstWage = _service.TblKarmozd.SupervisiorReportFirstPrice(ddlSupervisior, ddlYear, ddlMonth);
            var _doubleWage = _service.TblKarmozd.SupervisiorReportDoublePrice(ddlSupervisior, ddlYear, ddlMonth);
            var _noWage = _service.TblKarmozd.SupervisiorReportNoPricePrice(ddlSupervisior, ddlYear, ddlMonth);


            var _subgroupName = _service.TblSupervisior.GetByID(ddlSupervisior);

            var firstWage = await _firstWage;
            var doubleWage = await _doubleWage;
            var noWage = await _noWage;


            var subgroupName = await _subgroupName;


            //ثبت بیمه نامه هایی که کارمزد آنها برای اولین بار کارمزد واریز شده
            foreach (var item in firstWage)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                vm.PaymentType = item.TblLifeInsurance.PaymentType;
                vm.Price = item.TblLifeInsurance.Payment_Price;
                vm.Serial = item.TblLifeInsurance.Serial;
                vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                vm.Status = item.TblLifeInsurance.Insurance_Status;
                vm.Wage = item.Price;

                model.FirstWage.Add(vm);
            }

            // ثبت بیمه نامه هایی که کارمزد آنها برای دومین بار واریز شده
            foreach (var item in doubleWage)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.TblLifeInsurance.Bimegozar_Name;
                vm.BimeshodeName = item.TblLifeInsurance.Bimeshode_Name;
                vm.InsuranceNumber = item.TblLifeInsurance.InsuranceNumber_Code + "/" + item.TblLifeInsurance.InsuranceNumber_CenterCode + "/" + item.TblLifeInsurance.InsuranceNumber_GarardadNumber + "/" + item.TblLifeInsurance.InsuranceNumber_Year + "/" + item.TblLifeInsurance.InsuranceNumber_Number;
                vm.PaymentType = item.TblLifeInsurance.PaymentType;
                vm.Price = item.TblLifeInsurance.Payment_Price;
                vm.Serial = item.TblLifeInsurance.Serial;
                vm.SoodoorDate = item.TblLifeInsurance.Date_Soodoor_Shamsi;
                vm.Status = item.TblLifeInsurance.Insurance_Status;
                vm.Wage = item.Price;

                model.DoubleWage.Add(vm);
            }

            // ثبت بیمه نامه هایی که برای آنها کارمزد واریز نشده
            foreach (var item in noWage)
            {
                var vm = new ReportTableViewModel();

                vm.BimegozarName = item.Bimegozar_Name;
                vm.BimeshodeName = item.Bimeshode_Name;
                vm.InsuranceNumber = item.InsuranceNumber_Code + "/" + item.InsuranceNumber_CenterCode + "/" + item.InsuranceNumber_GarardadNumber + "/" + item.InsuranceNumber_Year + "/" + item.InsuranceNumber_Number;
                vm.PaymentType = item.PaymentType;
                vm.Price = item.Payment_Price;
                vm.Serial = item.Serial;
                vm.SoodoorDate = item.Date_Soodoor_Shamsi;
                vm.Status = item.Insurance_Status;
                vm.Wage = 0;

                model.NoPriceWage.Add(vm);
            }



            model.FirstWagePercent = SubgroupPercent;
            model.DoubleWagePercent = SubgroupOldInsurancePercent;


            ViewBag.Year = ddlYear;
            ViewBag.Month = ddlMonth;
            ViewBag.SubgroupName = subgroupName.Name;

            return View("SubgroupReportPrint", model);

        }
    }
}