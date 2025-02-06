using App.Domain.Core.MyTaskManager.Users.App.Domain.Core;
using App.Domain.Core.MyTaskManager.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskManagerEndPoint.Models.InmemoryDB;

namespace MyTaskManagerEndPoint.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;
        
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserAppService userAppService, SignInManager<User> signInManager)
        {
            _userAppService = userAppService;
            _signInManager = signInManager;
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAppService.Login(userModel.UserName,userModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Duty");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(userModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel,CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = registerModel.UserName, Email = registerModel.Email, FirstName=registerModel.FirstName,LastName=registerModel.LastName,NationalCode=registerModel.NationalCode };
                var result = await _userAppService.Register(user, registerModel.Password, cancellationToken);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Duty");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerModel);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
