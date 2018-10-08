using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pishkhan_LifeInsurance.Services;
using Pishkhan_LifeInsurance.Models.AgentViewModels;
using Microsoft.AspNetCore.Identity;
using Pishkhan_LifeInsurance.Models;
using Pishkhan_LifeInsurance.Data.DataBase;

namespace Pishkhan_LifeInsurance.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ITblUserRepository user;
        private readonly UserManager<TblUser> _userManager;
        private readonly IService _service;


        public UsersController(ITblUserRepository _user, UserManager<TblUser> userManager, IService service)
        {
            user = _user;
            _userManager = userManager;
            _service = service;
        }

        #region TblUser Methods

        [HttpGet]
        public async Task<IActionResult> AgentListAsync()
        {

            var model = new List<AgentListViewModels>();

            var result = user.GetAgentListAsync();
            var lstUser = await result;

            if (lstUser.Count > 0)
            {
                foreach (var item in lstUser)
                {
                    AgentListViewModels vm = new AgentListViewModels();
                    vm.ID = item.Id;
                    vm.AgentID = item.AgentID;
                    vm.Email = item.Email;
                    vm.Name = item.Name;

                    model.Add(vm);
                }
            }

            return View("AgentList", model);
        }

        [HttpPost]
        public async Task<JsonResult> GetUserByIdAsync(string Id)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return Json(new AgentListViewModels()
                    {
                        AgentID = 0,
                        Email = "Can not Find This User",
                        ID = "",
                        Name = "Can not Find This User"
                    });
                }

                return Json(new AgentListViewModels()
                {

                    AgentID = user.AgentID,
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name
                });
            }
            catch
            {

                return Json(new AgentListViewModels()
                {
                    AgentID = 0,
                    Email = "Can not Find This User",
                    ID = "",
                    Name = "Can not Find This User"
                });
            }

        }

        [HttpPost]
        public async Task<JsonResult> UpdateUserInfoById(AgentListViewModels model)
        {

            var info = await _userManager.FindByIdAsync(model.ID);
            if (info == null)
            {
                return Json(new { pm = "کاربری با این مشخصات وجود ندارد" });

            }

            info.Name = model.Name;
            info.Email = model.Email;
            info.AgentID = model.AgentID;

            var result = user.UpdateUserInfo(info);

            if (result)
            {

                return Json(new { pm = "ok" });
            }
            else
            {
                return Json(new { pm = "خطا در سیستم" });
            }
        }

        #endregion
        //-------------------------------------------------------------------------------------------------------------------------------

        #region TblPishkhan Methods

        [HttpGet]
        public IActionResult AddPishkhan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPishkhan(TblPishkhan model)
        {
            if (ModelState.IsValid)
            {
                var result = _service.TblPishkhan.Add(model);

                if (await result)
                {

                    var save = _service.Complete();

                    if (await save > 0)
                    {

                        ModelState.Clear();
                        model.Name = "";
                        ViewBag.pm = "ok";
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "خطا در ذخیره اطلاعات در دیتا بیس");
                        return View(model);
                    }
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "خطا در ثبت اولیه");
                    return View(model);
                }

            }

            else
            {

                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را به درستی وارد نمایید.");
                return View(model);
            }


        }

        [HttpGet]
        public async Task<IActionResult> PishkhanListAsync()
        {

            var list = _service.TblPishkhan.GetAll();

            var model = await list;

            return View("PishkhanList", model);
        }

        [HttpPost]
        public async Task<JsonResult> GetPishkhanByIdAsync(int id)
        {

            if (id > 0)
            {

                var _info = _service.TblPishkhan.GetByID(id);
                var info = await _info;

                return Json(new TblPishkhan()
                {

                    ID = info.ID,
                    Name = info.Name,
                    Pishkhan_Code = info.Pishkhan_Code
                });
            }
            else
            {

                return Json("");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdatePishkhanInfoById(TblPishkhan model)
        {
            if (ModelState.IsValid)
            {

                var result = _service.TblPishkhan.Update(model);
                var _complete = _service.Complete();
                var complete = await _complete;


                if (result && complete > 0)
                {

                    return Json(new { pm = "ok" });
                }
                else
                {
                    return Json(new { pm = "خطا در سیستم" });
                }
            }
            else
            {
                return Json(new { pm = "لطفا اطلاعات را به درسیتی وارد نمایید" });
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------------------------------------
        #region TblSupervisior Methods

        [HttpGet]
        public IActionResult AddSupervisior()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSupervisior(TblSupervisior model)
        {
            if (ModelState.IsValid) {

                var _add = _service.TblSupervisior.Add(model);
                var _save = _service.Complete();

                var add = await _add;
                var save = await _save;

                if (add && save > 0) {
                    ModelState.Clear();
                    ViewBag.pm = "ok";
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "خطا در ذخیره اطلاعات در داخل دیتا بیس.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات را به درستی وارد نمایید.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SupervisiorListAsync()
        {

            var list = _service.TblSupervisior.GetAll();

            var model = await list;

            return View("SupervisiorList", model);
        }

        [HttpPost]
        public async Task<JsonResult> GetSupervisiorByIdAsync(int id)
        {

            if (id > 0)
            {

                var _info = _service.TblSupervisior.GetByID(id);
                var info = await _info;

                return Json(new TblPishkhan()
                {

                    ID = info.ID,
                    Name = info.Name,
                });
            }
            else
            {

                return Json("");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSupervisiorInfoById(TblSupervisior model)
        {
            if (ModelState.IsValid)
            {

                var result = _service.TblSupervisior.Update(model);
                var _complete = _service.Complete();
                var complete = await _complete;


                if (result && complete > 0)
                {

                    return Json(new { pm = "ok" });
                }
                else
                {
                    return Json(new { pm = "خطا در سیستم" });
                }
            }
            else
            {
                return Json(new { pm = "لطفا اطلاعات را به درسیتی وارد نمایید" });
            }
        }


        #endregion
    }
}

