using System.Security.Claims;
using App.Domain.Core.MyTaskManager.Duties.App.Domain.Core;
using App.Domain.Core.MyTaskManager.Users.Entities;
using AppEndPoint.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using MyTaskManagerEndPoint.Models.InmemoryDB;

namespace MyTaskManagerEndPoint.Controllers
{
    [Authorize]
    public class DutyController : Controller
    {
        private readonly IDutyAppService _service;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _appsetting;
        private readonly IDutyMethod _dutyMethod;
        public DutyController(IDutyAppService service, UserManager<User> user, IConfiguration configuration, IDutyMethod dutyMethod)
        {
            _service = service;
            _userManager = user;
            _appsetting = configuration;
            _dutyMethod = dutyMethod;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.UserId = userId;
                int NotCompCount = _service.NumberOfNotCompleted(user.Id);
                return View(NotCompCount);
            } 
        }
        [HttpGet]
        public async Task<IActionResult> GetListOfAllUserDuties()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var Duties = await _service.GetAllDuties(user.Id);
                if (Duties == null)
                {
                    TempData["Null Duty list"] = "لیست وظایف شما خالی است";
                    return RedirectToAction("AddDuty");
                }
                return View(Duties);
            }
        }
        [HttpGet]
        public IActionResult AddDuty()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDuty(string Title, string Description)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var CheckLimit = _dutyMethod.CanWriteNewDuty(user.Id);
                if (!CheckLimit)
                {
                    TempData["تکمیل ظرفیت"] = "شما به سفق مجاز وظایف انجام نشده رسیدید لطفا اول وظایف قبلی خود را تکمیل کنید";
                    return RedirectToAction("GetListOfAllUserDuties");
                }
                else
                {
                    var Result = _service.AddDuty(Title, Description, user);
                    if (!Result)
                    {
                        TempData["نام تکراری"] = "این عنوان قبلا برای تسک دیگری انتخاب شده است";
                        return View(Description);
                    }
                    else
                    {
                        TempData["موفقیت در ایجاد وظیفه"] = "وظیفه با موفقیت به لیست شما اضافه شد";
                        return RedirectToAction("GetListOfAllUserDuties");
                    }
                }
            }
        }
        [HttpGet]
        public IActionResult EditDuty(int DutyId)
        {
            var Duty = _service.GetDutyById(DutyId);
            if (Duty == null)
            {
                TempData["عدم یافتن شناسه ی تسک"] = "شناسه ی درخواست شده در دیتا بیس موجود نمیباشد";
                return RedirectToAction("GetListOfAllUserDuties");
            }
            else
            {
                Inmemmorydb.TempDuty = Duty;
                return View(Duty);
            }
        }
        [HttpPost]
        public IActionResult EditDuty(string Title, string Description)
        {
            Inmemmorydb.TempDuty.Title = Title;
            Inmemmorydb.TempDuty.Description = Description;
            var Result = _service.UpdateDuty(Inmemmorydb.TempDuty, Inmemmorydb.TempDuty.Id);
            if (Result)
            {
                TempData["موفقت در ویرایش"] = "وظیفه با موفقیت ویرایش شد";
                return RedirectToAction("GetListOfAllUserDuties");
            }
            else
            {
                TempData["شکست در ویرایش"] = "در فرایند ویرایش این وظیفه خطایی رخ داده است";
                return View();
            }
        }
        [HttpGet]
        public IActionResult RemoveDuty(int DutyId)
        {
            var Result = _service.DeleteDuty(DutyId);
            if (Result)
            {
                TempData["موفقیت در حذف وظیفه"] = "وظیفه با موفقیت حذف شد";
                return RedirectToAction("GetListOfAllUserDuties");
            }
            else
            {
                TempData["خطا در حذف وظیفه"] = "خطایی در هنگام حذف این وظیفه رخ داد لطفا با اپراتور تماس حاصل فرمایید";
                return RedirectToAction("GetListOfAllUserDuties");
            }
        }
        [HttpGet]
        public IActionResult MarkAsCompleted(int id)
        {
            var Result = _service.MarkAsCompleted(id);
            if (!Result)
            {
                TempData["خطا در تغییر وضعیت وظیفه"] = "(!!در هنگام تغییر وضعیت وظیفه خطایی رخ داده است (نیاز به برسی";
                return RedirectToAction("GetListOfAllUserDuties");
            }
            else
            {
                TempData["موفقیت در تغییر وضعیت وظیفه"] = "وضعیت وظیفه با موفقیت تغییر یافت";
                return RedirectToAction("GetListOfAllUserDuties");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCompleted()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var Duties=await _service.GetListOfCompletedDuties(user.Id);
                if(Duties is null)
                {
                    TempData["عدم وجود وظیفه ی تکمیل شده"] = "وظیفه ی تکمیل شده ای برای شما یافت نشد";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Duties);
                }

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNotCompleted()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var Duties = await _service.GetListOfNotComletedDuties(user.Id);
                if (Duties is null)
                {
                    TempData["عدم وجود وظیفه ی تکمیل نشده"] = "وظیفه ی تکمیل نشده ای برای شما یافت نشد";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Duties);
                }

            }
        }
        [HttpGet]
        public IActionResult FindByTitle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FindByTitle(string title)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var Duty = _service.GetDutyByTitle(user.Id,title);
                if (Duty is null)
                {
                    TempData["موردی یافت نشد"] = "موردی با این عنوان یافت نشد";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(Duty);
                }
            }

        }
    }
}
