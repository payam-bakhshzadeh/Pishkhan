using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pishkhan_LifeInsurance.Models.WageViewModels;
using Pishkhan_LifeInsurance.Services;
using OfficeOpenXml;
using System.Text;
using Pishkhan_LifeInsurance.Data.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Pishkhan_LifeInsurance.Controllers
{
    public class FinancialController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        private readonly IService _service;
        private readonly ITblUserRepository _user;

        public FinancialController(IHostingEnvironment environment, IService service, ITblUserRepository user)
        {
            _hostingEnvironment = environment;
            _service = service;
            _user = user;
        }


        [HttpGet]
        public IActionResult UploadExcel()
        {
            var model = new List<string>();
            model = GetUploadsFileList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IList<IFormFile> ExcelFiles)
        {
            var model = GetUploadsFileList();
            try
            {
                string path = "";
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                foreach (var file in ExcelFiles)
                {
                    if (file.Length <= 0)
                    {

                        ModelState.Clear();
                        ModelState.AddModelError("File", "File is empty!");
                        return View();
                    }


                    if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx") || file.FileName.EndsWith(".xlsm"))
                    {
                        var filepath = Path.Combine(uploads, file.FileName);
                        path = filepath;

                        using (var filestream = new FileStream(filepath, FileMode.Create))
                        {

                            await file.CopyToAsync(filestream);

                            model = GetUploadsFileList();
                        }
                    }

                    else
                    {

                        ModelState.Clear();
                        ModelState.AddModelError("File", "File format is not supported!");

                    }


                }
            }
            catch (Exception)
            {

                ModelState.Clear();
                ModelState.AddModelError("File", "Error to upload file. Please try again");
            }


            return View(model);
        }

        public JsonResult DeleteExcelFile(string name)
        {
            try
            {
                string webroot = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                FileInfo file = new FileInfo(Path.Combine(webroot, name));
                if (file.Exists)
                {
                    file.Delete();
                }

                return Json(new { pm = "ok" });
            }
            catch (Exception)
            {

                return Json(new { pm = "error" });
            }
        }

        //------------------------------------------------------------------------------------------------------------------

        //ثبت کارمزد ها
        [HttpGet]
        public async Task<IActionResult> ImportWage()
        {
            var model = await FillImportWageModel();



            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ImportWage(string ddlAgent, int ddlYear, int ddlMonth, string ExcelFiles)
        {
            //بررسی اینکه برای این نماینده برای این ماه و این سال کارمزد واریز شده یا نه
            var checkIfDouble = _service.TblKarmozd.CheckIfBeforSubmitThisKarmozd(ddlAgent, ddlMonth, ddlYear);

            if (await checkIfDouble)
            {
                return Json(new { pm = "برای این نماینده قبلا کارمزد محاسبه گردیده" });
            }

            else
            {





                //اگر هنگام خواند فایل اکسل در یک سطر به خطا خورد این خطا در اس بی ذخیره شده و تعداد دفعات خطا در ارور کانت ذخیره خواهد شد که به کاربر نشان داده شود
                StringBuilder sbExcel = new StringBuilder();
                StringBuilder sbCompare = new StringBuilder();
                sbExcel.Clear();
                sbCompare.Clear();
                int errorCountExcel = 0;
                int errorCountCompare = 0;
                int successCountSaveDatabase = 0;

                //ستون A باید کارمزد ها باشد
                //ستون B باید شماره بیمه نامه ها باشد
                List<ExcelViewModel> lstExcel = new List<ExcelViewModel>();

                if (ExcelFiles != null)
                {
                    string root = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    string filename = ExcelFiles;
                    FileInfo file = new FileInfo(Path.Combine(root, filename));



                    try
                    {
                        using (ExcelPackage package = new ExcelPackage(file))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                            int rowCount = worksheet.Dimension.Rows;
                            int colCount = worksheet.Dimension.Columns;
                            int startRow = worksheet.Dimension.Start.Row;
                            int startColumn = worksheet.Dimension.Start.Column;

                            bool bHeaderRow = true;

                            for (int row = startRow; row < startRow + rowCount; row++)
                            {

                                try
                                {
                                    if (bHeaderRow)
                                    {
                                        ExcelViewModel vm = new ExcelViewModel();
                                        vm.Price = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                                        vm.InsuranceNumber = worksheet.Cells[row, 2].Value.ToString();
                                        lstExcel.Add(vm);

                                    }
                                    else
                                    {
                                        ExcelViewModel vm = new ExcelViewModel();
                                        vm.Price = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                                        vm.InsuranceNumber = worksheet.Cells[row, 2].Value.ToString();
                                        lstExcel.Add(vm);
                                    }
                                }
                                catch (Exception)
                                {

                                    sbExcel.AppendLine(worksheet.Cells[row, 2].Value.ToString() + "----- " + worksheet.Cells[row, 1].Value.ToString());
                                    errorCountExcel += 1;
                                }


                            }

                            //فایل اکسل روی رم بارگذاری شد. حالا اطلاعات را از دیتا بیس خوانده و مقایسه کرده و کارمزد هر بیمه نامه را درجدول کارمزدها ثبت میکنیم
                            var DatabaseInsuranceList = await _service.TblLifeInsurance.GetAllInsuranceForCompareToExcelFile(ddlAgent);

                            List<TblKarmozd> lstTblKarmozd = new List<TblKarmozd>();
                            foreach (var item in lstExcel)
                            {
                                try
                                {
                                    var index = item.InsuranceNumber.LastIndexOf("/") + 1;
                                    var excelinsurancenumber = int.Parse(item.InsuranceNumber.Substring(index).Trim());

                                    var result = DatabaseInsuranceList.Find(a => a.InsuranceNumber == excelinsurancenumber);


                                    //بیمه نامه پیدا شده
                                    if (result != null)
                                    {
                                        TblKarmozd vm = new TblKarmozd();


                                        //فهمیدن اینکه این بیمه نامه تکراری است یا برای اولین بار کارمزد واریز گردیده
                                        vm.isDouble = await _service.TblKarmozd.FindDoubleInsuranceById(result.ID);
                                        //این تغییرات را جدیدا اعمال کردم 
                                        //اگر بیمه گذار 5 قسط را همزمان واریز کرده باشد سیستم باید قسط اول را به عنوان کارمزد بار اوال محاسبه کرده و مابقی اقساط را به عنوان اقساط مجدد محاسبه و کارمزد آنها را کمتر حساب کند
                                        if (vm.isDouble == false)
                                        {
                                            vm.isDouble = lstTblKarmozd.Any(z => z.TblLifeInsuranceID == result.ID);
                                        }
                                        ////////////////////////////////////////////////////////////////
                                        vm.Month = ddlMonth;
                                        vm.Price = item.Price;
                                        vm.TblLifeInsuranceID = result.ID;
                                        vm.Year = ddlYear;
                                        vm.AgentID = result.AgentID;

                                        lstTblKarmozd.Add(vm);
                                    }
                                }
                                catch (Exception)
                                {

                                    sbCompare.AppendLine(item.InsuranceNumber);
                                    errorCountCompare += 1;
                                }

                            }


                            //ذخیره سازی در دیتابیس
                            if (lstTblKarmozd.Count > 0)
                            {
                                var _save = _service.TblKarmozd.AddRange(lstTblKarmozd);
                                var _compelete = _service.Complete();

                                var save = await _save;
                                var compelet = await _compelete;

                                //اطلاعات با موفقیت ذخیره شد
                                if (save && compelet > 0)
                                {
                                    successCountSaveDatabase = compelet;
                                }

                                //خطا در ذخیره اطلاعات در دیتا بیس
                                else
                                {
                                    return Json(new { pm = "خطا در دخیرخ اطلاعات در دیتابیس.لطفا با مدیر تماس بگیرید" });
                                }
                            }

                            else
                            {
                                return Json(new { pm = "آیتمی برای ذخیره کردن پیدا نشد" });
                            }








                        }
                    }
                    catch (Exception)
                    {

                        return Json(new { pm = "خطا در خواند فایل اکسل. اطفا با مدیر تماس بگیرید" });
                    }
                }

                else
                {
                    return Json(new { pm = "لطفا یک فایل اکسل را انتخاب نمایید" });
                }


                var totalExcelRow = lstExcel.Count;

                //return View(model);
                return Json(new { errorExcelText = sbExcel.ToString(), errorCountExcel = errorCountExcel, totalExcelRow = totalExcelRow, errorCompareText = sbCompare.ToString(), errorCountCompare = errorCountCompare, successCountSaveDatabase = successCountSaveDatabase });
            }
        }






        //---------------------------------------------------------------------------------------------------------------------
        public List<string> GetUploadsFileList()
        {
            List<string> lst = new List<string>();
            try
            {
                string wwwroot = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var lstFileName = Directory.GetFiles(wwwroot);

                foreach (var item in lstFileName)
                {
                    var index = item.LastIndexOf("\\");
                    index = index + 1;
                    var a = item.Substring(index);
                    lst.Add(a);
                }
            }
            catch (Exception) { }

            return lst;
        }



        public async Task<ImportWageViewModel> FillImportWageModel()
        {
            var model = new ImportWageViewModel();

            var _agent = _user.GetAgentListAsync();

            for (int i = 1396; i < 1420; i++)
            {
                model.Year.Add(i);
            }

            for (int i = 1; i < 13; i++)
            {
                model.Month.Add(i);
            }

            var agent = await _agent;

            foreach (var item in agent)
            {
                model.Users.Add(new Users { ID = item.Id, Name = item.Name });
            }

            model.ExcelFileList = GetUploadsFileList();

            return model;
        }


        //----------------------------------------------------------------------------------------------------------
        //حذف کارمزدها
        [HttpGet]
        public async Task<IActionResult> DeleteWage()
        {
            var _result = _service.TblKarmozd.GetListOfWageGroupByAgentID();
            //var result = await _result.GroupBy(a => a.Month).ToListAsync();
            var result = await _result.GroupBy(a => a.AgentID).ToListAsync();



            var model = new List<DeleteWageViewModel>();

            foreach (var item in result)
            {
                var groupByMonth = item.GroupBy(a => a.Month);

                foreach (var items in groupByMonth)
                {
                    var vm = new DeleteWageViewModel();
                    vm.AgentName = items.First().TblUser.Name;
                    vm.Month = items.First().Month;
                    vm.Year = items.First().Year;
                    vm.AgentID = items.First().AgentID;

                    model.Add(vm);
                }


                
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWage(string id)
        {
            var items = id.Split(",");
            var lst = await _service.TblKarmozd.GetListOfWageSortByMonth_Year_AgentID(items[0], int.Parse(items[1]), int.Parse(items[2])).ToListAsync();

            var delete = _service.TblKarmozd.DeleteRange(lst);
            var _compelete = _service.Complete();
            var compelete = await _compelete;

            if (delete && compelete > 0)
            {
                return Json(new { pm = compelete+" مورد حذف گردید" });
            }

            else
            {
                return Json(new { pm = false });
            }

        }
    }
}