using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pishkhan_LifeInsurance.Models.LifeInsuranceViewModels;
using Pishkhan_LifeInsurance.Views.LifeInsurance;
using Pishkhan_LifeInsurance.Services;
using Pishkhan_LifeInsurance.Models.AgentViewModels;
using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Data.Enums;
using Pishkhan_LifeInsurance.Extensions;
using Pishkhan_LifeInsurance.Models.ManageViewModels;
using Pishkhan_LifeInsurance.Models.InsuranceListViewModels;

namespace Pishkhan_LifeInsurance.Controllers
{
    [Authorize]
    public class LifeInsuranceController : Controller
    {
        private readonly IService _service;
        private readonly ITblUserRepository _user;

        public LifeInsuranceController(IService service, ITblUserRepository user)
        {
            _service = service;
            _user = user;
        }


        [HttpGet]
        public async Task<IActionResult> Add(int id = 0)
        {
            AddViewModels model = new AddViewModels();
            model = await FillAddModel();

            //کاربر قصد اضافه کردن رکورد جدید را دارد
            if (id == 0)
            {
                try {
                    var _setting = _service.TblSetting.GetFirst();
                    var setting = await _setting;
                    model.LifeInsuranceVM.InsuranceNumber_CenterCode = setting.InsuranceNumber_CenterCode;
                    model.LifeInsuranceVM.InsuranceNumber_Code = setting.InsuranceNumber_Code;
                    model.LifeInsuranceVM.InsuranceNumber_GarardadNumber = setting.InsuranceNumber_GarardadNumber;
                    model.LifeInsuranceVM.InsuranceNumber_Year = setting.InsuranceNumber_Year;
                }
                catch { }


               
            }

            //کاربر قصد آپدیت کردن اطلاعات را دارد
            else if (id != 0)
            {
                var _insurance = _service.TblLifeInsurance.GetByID(id);
                var insuramce = await _insurance;

                model.LifeInsuranceVM.Bimegozar_Name = insuramce.Bimegozar_Name;
                model.LifeInsuranceVM.Bimegozar_Phone = insuramce.Bimegozar_Phone;
                model.LifeInsuranceVM.Bimeshode_Name = insuramce.Bimeshode_Name;
                model.LifeInsuranceVM.Bimeshode_Phone = insuramce.Bimeshode_Phone;
                model.LifeInsuranceVM.Date_Soodoor_Shamsi = insuramce.Date_Soodoor_Shamsi;
                model.LifeInsuranceVM.Date_Start_Shamsi = insuramce.Date_Start_Shamsi;
                model.LifeInsuranceVM.Date_Soodoor_Miladi = insuramce.Date_Soodoor_Miladi;
                model.LifeInsuranceVM.Date_Start_Miladi = insuramce.Date_Start_Miladi;
                model.LifeInsuranceVM.InsuranceNumber_CenterCode = insuramce.InsuranceNumber_CenterCode;
                model.LifeInsuranceVM.InsuranceNumber_Code = insuramce.InsuranceNumber_Code;
                model.LifeInsuranceVM.InsuranceNumber_GarardadNumber = insuramce.InsuranceNumber_GarardadNumber;
                model.LifeInsuranceVM.InsuranceNumber_Number = insuramce.InsuranceNumber_Number;
                model.LifeInsuranceVM.InsuranceNumber_Year = insuramce.InsuranceNumber_Year;
                model.LifeInsuranceVM.Insurance_Status = insuramce.Insurance_Status;
                model.LifeInsuranceVM.PaymentType = insuramce.PaymentType;
                model.LifeInsuranceVM.Payment_Price = insuramce.Payment_Price;
                model.LifeInsuranceVM.PishkhanID = insuramce.PishkhanID;
                model.LifeInsuranceVM.SepordeAvaliye = insuramce.SepordeAvaliye;
                model.LifeInsuranceVM.Serial = insuramce.Serial;
                model.LifeInsuranceVM.SupervisorID = insuramce.SupervisorID;
                model.LifeInsuranceVM.UserID = insuramce.UserID;
                model.LifeInsuranceVM.ID = insuramce.ID;
                

            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddViewModels model, string ddlAgent, int ddlInsuranceStatus, int ddlPaymentType, int ddlPishkhan = 0, int ddlSupervisior = 0, int ID = 0)
        {

            if (model.LifeInsuranceVM.Payment_Price != 0)
            {

                try
                {
                    TblLifeInsurance obj = new TblLifeInsurance();

                    //Fill LifeInsurance Table Info
                    obj.ID = ID;
                    obj.Bimegozar_Name = model.LifeInsuranceVM.Bimegozar_Name;
                    obj.Bimegozar_Phone = model.LifeInsuranceVM.Bimegozar_Phone;
                    obj.Bimeshode_Name = model.LifeInsuranceVM.Bimeshode_Name;
                    obj.Bimeshode_Phone = model.LifeInsuranceVM.Bimeshode_Phone;
                    obj.Date_Soodoor_Shamsi = model.LifeInsuranceVM.Date_Soodoor_Shamsi;
                    obj.Date_Start_Shamsi = model.LifeInsuranceVM.Date_Start_Shamsi;
                    obj.InsuranceNumber_CenterCode = model.LifeInsuranceVM.InsuranceNumber_CenterCode;
                    obj.InsuranceNumber_Code = model.LifeInsuranceVM.InsuranceNumber_Code;
                    obj.InsuranceNumber_GarardadNumber = model.LifeInsuranceVM.InsuranceNumber_GarardadNumber;
                    obj.InsuranceNumber_Number = model.LifeInsuranceVM.InsuranceNumber_Number;
                    obj.InsuranceNumber_Year = model.LifeInsuranceVM.InsuranceNumber_Year;
                    obj.Insurance_Status = Convert.ToBoolean(ddlInsuranceStatus);
                    obj.PaymentType = ddlPaymentType;
                    obj.Payment_Price = model.LifeInsuranceVM.Payment_Price;
                    

                    //کد دفتر پیشخوان
                    if (ddlPishkhan != 0)
                        obj.PishkhanID = ddlPishkhan;

                    obj.SepordeAvaliye = model.LifeInsuranceVM.SepordeAvaliye;
                    obj.Serial = model.LifeInsuranceVM.Serial;

                    //کد زیر گروه
                    if (ddlSupervisior != 0)
                        obj.SupervisorID = ddlSupervisior;

                    obj.UserID = ddlAgent;

                    ModelState.Clear();
                    //تاریخ های میلادی
                    obj.Date_Soodoor_Miladi =  model.LifeInsuranceVM.Date_Soodoor_Miladi;
                    obj.Date_Start_Miladi = model.LifeInsuranceVM.Date_Start_Miladi;

                    if (obj.Date_Soodoor_Miladi == new DateTime() || obj.Date_Start_Miladi == new DateTime())
                        ModelState.AddModelError(string.Empty, "تاریخ های میلادی بدرستی وارد نشدند");


                    //تاریخ ثبت بیمه نامه
                    obj.TarikhSabtBimename = DateTime.Now;

                    //ذخیره سازی در دیتابیس
                    //یعنی یک رکورد جدید می خواهیم ثبت کنیم
                    if (ID == 0)
                    {
                        //چک کردن اینکه آیا بیمه نامه ای قبلا با این شماره یا سریال در سیستم ثیت شده یا نه
                        var isInsuranceDouble = await _service.TblLifeInsurance.CheckAny_InsuranceNumber_InsuranceSerial(model.LifeInsuranceVM.InsuranceNumber_Number, model.LifeInsuranceVM.Serial);

                        if (!isInsuranceDouble)
                        {

                            var _add = _service.TblLifeInsurance.Add(obj);
                            var _complete = _service.Complete();

                            var add = await _add;
                            var complete = await _complete;

                            //ذخیره سازی بدرستی انجام شده
                            if (add && complete > 0)
                            {

                                ViewBag.pm = "ok";
                                AddViewModels modell = new AddViewModels();
                                modell = await FillAddModel();
                                return View(modell);
                            }

                            //خطا در ذخیره سازی اطلاعات در دیتا بیس
                            else
                            {
                                ViewBag.pm = "error";
                                return View(model);
                            }
                        }
                        else {

                            AddViewModels newModel = new AddViewModels();
                            newModel = await FillAddModel();
                            model.InsuranceStatus = newModel.InsuranceStatus;
                            model.PaymentType = newModel.PaymentType;
                            model.PishkhanVm = newModel.PishkhanVm;
                            model.SupervisiorVm = newModel.SupervisiorVm;
                            model.AgentVM = newModel.AgentVM;

                            ModelState.Clear();
                            ModelState.AddModelError(string.Empty, "شماره بیمه نامه یا شماره سریال بیمه نامه تکراری است");
                            return View(model);
                        }

                    }

                    //می خواهیم آپدیت انجام دهیم
                    else
                    {
                        var update = _service.TblLifeInsurance.Update(obj);
                        var _compelete = _service.Complete();
                        var compelete = await _compelete;

                        if (update && compelete > 0)
                        {
                            return RedirectToAction("List");
                        }
                        else
                        {
                            ViewBag.pm = "error";
                            return View(model);
                        }
                    }




                }
                catch (Exception)
                {
                    AddViewModels newModel = new AddViewModels();
                    newModel = await FillAddModel();
                    model.InsuranceStatus = newModel.InsuranceStatus;
                    model.PaymentType = newModel.PaymentType;
                    model.PishkhanVm = newModel.PishkhanVm;
                    model.SupervisiorVm = newModel.SupervisiorVm;
                    model.AgentVM = newModel.AgentVM;

                    ModelState.AddModelError(string.Empty, "لطفا اطلاعات را به درستی وارد نمایید");
                    return View(model);
                }
            }
            else
            {
                AddViewModels newModel = new AddViewModels();
                newModel = await FillAddModel();
                model.InsuranceStatus = newModel.InsuranceStatus;
                model.PaymentType = newModel.PaymentType;
                model.PishkhanVm = newModel.PishkhanVm;
                model.SupervisiorVm = newModel.SupervisiorVm;
                model.AgentVM = newModel.AgentVM;


                ModelState.AddModelError(string.Empty, "وارد کردن حق بیمه ماهانه اجباری است");
                return View(model);
            }



        }


        //متدی برای پر کردت ویو مدال که هر بار لازم بود صداش کنیم
        private async Task<AddViewModels> FillAddModel()
        {

            AddViewModels model = new AddViewModels();



            try
            {
                //Fill the model
                var _agent = _user.GetAgentListAsync();
                var _pishkhan = _service.TblPishkhan.GetAll();
                var _supervisior = _service.TblSupervisior.GetAll();
                var _setting = _service.TblSetting.GetFirst();

                var agent = await _agent;
                var pishkhan = await _pishkhan;
                var supervisior = await _supervisior;

                //Fill Agent
                foreach (var item in agent)
                {
                    AgentListViewModels vm = new AgentListViewModels();
                    vm.AgentID = item.AgentID;
                    vm.Email = item.Email;
                    vm.ID = item.Id;
                    vm.Name = item.Name;

                    model.AgentVM.Add(vm);
                }


                //Fill Pishkhan
                foreach (var item in pishkhan)
                {
                    TblPishkhan vm = new TblPishkhan();
                    vm.ID = item.ID;
                    vm.Name = item.Name;
                    vm.Pishkhan_Code = item.Pishkhan_Code;

                    model.PishkhanVm.Add(vm);
                }

                //Fill Supervisior
                foreach (var item in supervisior)
                {
                    TblSupervisior vm = new TblSupervisior();
                    vm.ID = item.ID;
                    vm.Name = item.Name;

                    model.SupervisiorVm.Add(vm);
                }


                //Fill PaymentType
                var paymentType = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().Select(p => p.ToString()).ToList();
                model.PaymentType = paymentType;

                //Insurance Status
                var insuranceStatus = Enum.GetValues(typeof(InsuranceStatus)).Cast<InsuranceStatus>().Select(p => p.ToString()).ToList();
                model.InsuranceStatus = insuranceStatus;


                //Fill Insurance Number
                var setting = await _setting;
                model.LifeInsuranceVM.InsuranceNumber_Code = setting.InsuranceNumber_Code;
                model.LifeInsuranceVM.InsuranceNumber_CenterCode = setting.InsuranceNumber_CenterCode;
                model.LifeInsuranceVM.InsuranceNumber_GarardadNumber = setting.InsuranceNumber_GarardadNumber;
                model.LifeInsuranceVM.InsuranceNumber_Year = setting.InsuranceNumber_Year;


            }
            catch { }

            return model;
        }

        //این متد برای جدا کردن سه رقم اعشار اعداد مورد استفاده قرار میگیرد
        [HttpPost]
        public JsonResult ConvertToMony(int price)
        {

            var myprice = price.MoneyToPrice();

            return Json(new { mony = myprice });
        }



        //---------------------------------------------------------------------------------------------------------------
        //Lisy Of Insurance Methods

        [HttpGet]
        public async Task<IActionResult> List()
        {

            ListInsuranceViewModel model = new ListInsuranceViewModel();

            //Get Agent , pishkhan , supervisior List
            var _agentList = _user.GetAgentListAsync();
            var _pishkhanList = _service.TblPishkhan.GetAll();
            var _supervisiorList = _service.TblSupervisior.GetAll();
            var _countTotalRow = _service.TblLifeInsurance.GetTotalCount();

            var agentList = await _agentList;
            var pishkhanList = await _pishkhanList;
            var supervisiorList = await _supervisiorList;
            var countTotalRow = await _countTotalRow;



            //Fill DropdownList ViewModel
            foreach (var item in agentList)
            {
                AgentListViewModel vm = new AgentListViewModel();
                vm.ID = item.Id;
                vm.Name = item.Name;
                model.AgentListVM.Add(vm);
            }

            foreach (var item in pishkhanList)
            {
                PishkhanListViewModel vm = new PishkhanListViewModel();
                vm.ID = item.ID;
                vm.Name = item.Name;
                model.PishkhanListVM.Add(vm);
            }

            foreach (var item in supervisiorList)
            {
                SupervisiorListViewModel vm = new SupervisiorListViewModel();
                vm.ID = item.ID;
                vm.Name = item.Name;
                model.SupervisiorListVM.Add(vm);
            }



            //Fill Tabel Items
            model.Take = 20;
            model.Skip = 0;

            var _take = _service.TblSetting.GetFirst();
            var take = await _take;

            if (take != null)
            {
                model.Take = take.CountTakeItem;
            }


            //Get Info From Database
            var result = (_service.TblLifeInsurance.GetInsuranceList(false, model.Skip, model.Take, model.FilterItemsVM)).ToList();

            //Fill Count TotalRow
            model.CountTotalRow = countTotalRow;
            model.CountRow = result.Count;


            foreach (var item in result)
            {
                TabelItemsViewModel vm = new TabelItemsViewModel();
                vm.Agent = item.TblUser.Name;
                vm.BimegozarName = item.Bimegozar_Name;
                vm.BimegozarPhone = item.Bimegozar_Phone;
                vm.BimeshodeName = item.Bimeshode_Name;
                vm.BimeshodePhon = item.Bimeshode_Phone;
                vm.DateSoodoor = item.Date_Soodoor_Shamsi;
                vm.DateStart = item.Date_Soodoor_Shamsi;
                vm.ID = item.ID;
                vm.InsuranceNumber = item.InsuranceNumber_Code + "/" + item.InsuranceNumber_CenterCode + "/" + item.InsuranceNumber_GarardadNumber + "/" + item.InsuranceNumber_Year + "/" + item.InsuranceNumber_Number;
                vm.PaymentType = item.PaymentType;
                vm.Price = item.Payment_Price;
                vm.Status = item.Insurance_Status;
                try
                {
                    vm.Supervisior = item.TblSupervisior.Name;
                }
                catch { }
                try
                {
                    vm.Pishkhan = item.TblPishkhan.Name;
                }
                catch { }

                try {

                    if (item.TarikhSabtBimename.Year < 2000)
                    {

                        vm.TarikhSabtBimename = "";
                    }

                    else {

                        vm.TarikhSabtBimename = item.TarikhSabtBimename.DateToShamsi();
                    }

                    
                }
                catch {

                    vm.TarikhSabtBimename = "";
                }

                model.TabelItemsVM.Add(vm);
            }


            return View(model);
        }

        [HttpPost]
        public JsonResult GetNextPageInfo(int Take, int Skip, int Row)
        {
            Skip = Skip + Take;

            //Get Info From Database
            var result = (_service.TblLifeInsurance.GetInsuranceList(false, Skip, Take, null)).ToList();
            var countRow = result.Count;

            string html = "";

            foreach (var item in result)
            {
                Row = Row + 1;
                string InsuramceNumber = item.InsuranceNumber_Code + "/" + item.InsuranceNumber_CenterCode + "/" + item.InsuranceNumber_GarardadNumber + "/" + item.InsuranceNumber_Year + "/" + item.InsuranceNumber_Number;
                string Agent = item.TblUser.Name;
                string Supervisior = "";
                string Pishkhan = "";
                if (item.TblSupervisior != null) { Supervisior = item.TblSupervisior.Name; }
                if (item.TblPishkhan != null) { Pishkhan = item.TblPishkhan.Name; }


                string tarikhsabt = "";
                if (item.TarikhSabtBimename.Year < 2000)
                {
                    tarikhsabt = "";
                }

                else {
                    tarikhsabt = item.TarikhSabtBimename.DateToShamsi();
                }


                html += "<tr><td>" + Row + "</td><td>" + InsuramceNumber + "</td><td>" + Agent + "</td><td>" + Supervisior + "</td><td>" + Pishkhan + "</td><td>" + item.Bimegozar_Name + "</td><td>" + item.Bimeshode_Name + "</td><td>" + item.Date_Soodoor_Shamsi + "</td><td>" + item.Date_Start_Shamsi + "</td><td>" + item.PaymentType + " ماهه</td><td>" + item.Payment_Price.MoneyToPrice() + "</td><td>" + item.Insurance_Status + "</td><td>"+tarikhsabt+"</td><td><span>" +
                    "<a href = '/LifeInsurance/Add/" + item.ID + "' style = 'margin:10px;' title = 'Update'><i class='glyphicon glyphicon-upload text-success'></i></a></span><span>" +
                    "<a onclick='deleteRow(" + item.ID + ")' href = '' style='margin:10px;' title='Delete'><i class='glyphicon glyphicon-remove text-danger'></i></a></span></td></tr>";
            }

            return Json(new { html = html, countRow = countRow });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteLifeInsurance(int id)
        {
            bool pm = false;

            try
            {
                var _obj = _service.TblLifeInsurance.GetByID(id);
                var obj = await _obj;

                var result = _service.TblLifeInsurance.Delete(obj);
                await _service.Complete();
                pm = true;
            }
            catch (Exception) { }

            return Json(new { pm = pm });
        }

        //-----------------------------------------------------------------------------------------------------------------
        //Filter
        [HttpPost]
        public async Task<IActionResult> Filter(ListInsuranceViewModel md, string ddlAgent, int? ddlSupervisior, int? ddlPishkhan)
        {
            ListInsuranceViewModel model = new ListInsuranceViewModel();

            FilterItemsViewModel filter = new FilterItemsViewModel();
            filter = md.FilterItemsVM;
            filter.AgentID = ddlAgent;
            filter.PishkhanID = ddlPishkhan;
            filter.SupervisiorID = ddlSupervisior;

            //کاربر هیچ اطلاعاتی برای فیلتر کردن وارد ننموده
            if (filter.AgentID == "0" && filter.SupervisiorID == 0 && filter.PishkhanID == 0 && filter.InsuranceNumber == null && filter.BimegozarName == null && filter.BimegozarPhone == null && filter.BimeshodeName == null && filter.BimeshodePhone == null && filter.FromDate_Soodoor == null && filter.FromDate_Start == null && filter.InsuranceSerial == null && filter.ToDate_Soodoor == null && filter.ToDate_Start == null)
            {

                ViewBag.pm = "لطفا یک آیتم برای فیلتر انتخاب نمایید";
            }

            //ارسال اطلاعات به مادل برای جستجو در دیتابیس
            else
            {

                ViewBag.pm = "";
                var result = _service.TblLifeInsurance.GetInsuranceList(true, 0, 0, filter);

                foreach (var item in result)
                {
                    TabelItemsViewModel vm = new TabelItemsViewModel();
                    vm.Agent = item.TblUser.Name;
                    vm.BimegozarName = item.Bimegozar_Name;
                    vm.BimegozarPhone = item.Bimegozar_Phone;
                    vm.BimeshodeName = item.Bimeshode_Name;
                    vm.BimeshodePhon = item.Bimeshode_Phone;
                    vm.DateSoodoor = item.Date_Soodoor_Shamsi;
                    vm.DateStart = item.Date_Soodoor_Shamsi;
                    vm.ID = item.ID;
                    vm.InsuranceNumber = item.InsuranceNumber_Code + "/" + item.InsuranceNumber_CenterCode + "/" + item.InsuranceNumber_GarardadNumber + "/" + item.InsuranceNumber_Year + "/" + item.InsuranceNumber_Number;
                    vm.PaymentType = item.PaymentType;
                    vm.Price = item.Payment_Price;
                    vm.Status = item.Insurance_Status;
                    try
                    {
                        vm.Supervisior = item.TblSupervisior.Name;
                    }
                    catch { }
                    try
                    {
                        vm.Pishkhan = item.TblPishkhan.Name;
                    }
                    catch { }

                    model.TabelItemsVM.Add(vm);
                }


            }


            //-----------------------------------------------------------------------------------------------------------
            //return items


            //Get Agent , pishkhan , supervisior List
            var _agentList = _user.GetAgentListAsync();
            var _pishkhanList = _service.TblPishkhan.GetAll();
            var _supervisiorList = _service.TblSupervisior.GetAll();
            var _countTotalRow = _service.TblLifeInsurance.GetTotalCount();

            var agentList = await _agentList;
            var pishkhanList = await _pishkhanList;
            var supervisiorList = await _supervisiorList;
            var countTotalRow = await _countTotalRow;



            //Fill DropdownList ViewModel
            foreach (var item in agentList)
            {
                AgentListViewModel vm = new AgentListViewModel();
                vm.ID = item.Id;
                vm.Name = item.Name;
                model.AgentListVM.Add(vm);
            }

            foreach (var item in pishkhanList)
            {
                PishkhanListViewModel vm = new PishkhanListViewModel();
                vm.ID = item.ID;
                vm.Name = item.Name;
                model.PishkhanListVM.Add(vm);
            }

            foreach (var item in supervisiorList)
            {
                SupervisiorListViewModel vm = new SupervisiorListViewModel();
                vm.ID = item.ID;
                vm.Name = item.Name;
                model.SupervisiorListVM.Add(vm);
            }



            //Fill Tabel Items
            model.Take = 0;
            model.Skip = 0;
            return View("List", model);
        }



        //-------------------------------------------------------------------------------------------------------------------
        //Convert Date To Miladi
        public JsonResult ConvertDateToMiladi(string date) {

            if (!string.IsNullOrEmpty(date))
            {



                try
                {

                    var MiladiDate = date.DateToMiladi();
                    return Json(new { miladiDate = MiladiDate });
                }
                catch
                {

                    return Json(new { miladiDate = "" });
                }
            }

            else {

                return Json(new { miladiDate = "" });
            }
            
        }

    }
}