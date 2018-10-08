using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pishkhan_LifeInsurance.Services;
using Pishkhan_LifeInsurance.Data.DataBase;
using Pishkhan_LifeInsurance.Extensions;

namespace Pishkhan_LifeInsurance.Controllers
{
    public class DateController : Controller
    {
        private readonly IService _service;

        public DateController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {


            int success = 0;
            int error = 0;
            int needToChange = 0;

            //Get Info From Database

            var lstInsurance = await _service.TblLifeInsurance.GetAll();

            var a = lstInsurance.First().Date_Start_Miladi;

            foreach (var item in lstInsurance)
            {
                if (item.Date_Start_Miladi == a)
                {
                    try
                    {
                        needToChange = needToChange + 1;
                        item.Date_Start_Miladi = item.Date_Start_Shamsi.DateToMiladi();
                        _service.TblLifeInsurance.Update(item);
                        success = success + 1;

                    }
                    catch {

                        error = error + 1;

                    }

                }

                else if (item.Date_Start_Miladi == null)
                {
                    try
                    {
                        needToChange = needToChange + 1;
                        item.Date_Start_Miladi = item.Date_Start_Shamsi.DateToMiladi();
                        _service.TblLifeInsurance.Update(item);
                        success = success + 1;

                    }
                    catch { error = error + 1; }
                }



            }

            ViewBag.totalItemCount = lstInsurance.Count;
            ViewBag.NeedToChange = needToChange;
            ViewBag.Success = success;
            ViewBag.Error = error;

            var result = await _service.Complete();

            if (result > 0)
            {
                ViewBag.pm = "OK";
                return View(result);

            }

            else
            {

                ViewBag.pm = "No";
                return View(0);
            }




        }





    }
}