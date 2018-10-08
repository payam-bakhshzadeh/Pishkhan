using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pishkhan_LifeInsurance.Services;
using Pishkhan_LifeInsurance.Data.DataBase;

namespace Pishkhan_LifeInsurance.Controllers
{
    public class SettingController : Controller
    {
        private readonly IService _service;

        public SettingController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _model = _service.TblSetting.GetFirst();
            var model = await _model;

            //There is not any record in database
            if (model == null)
            {
                model = new TblSetting();
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(TblSetting model)
        {
            if (ModelState.IsValid)
            {
                //This is first record. Add it to database
                if (model.ID == 0)
                {
                    var _result = _service.TblSetting.Add(model);
                    var _complete = _service.Complete();

                    var result = await _result;
                    var complete = await _complete;

                    if (result && complete > 0)
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "خطا در ذخیره اطلاعات.لطفا مجددا تلاش نمایید");
                        return View(model);
                    }
                }

                //Update info in database
                else
                {
                    var result = _service.TblSetting.Update(model);
                    var complete = await _service.Complete();
                    if (result && complete > 0)
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "خطا در اصلاح اطلاعات.لطفا مجددا تلاش نمایید");
                        return View(model);
                    }
                }
            }

            else
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را به درستی وارد نمایید");
                return View(model);
            }
        }
    }
}