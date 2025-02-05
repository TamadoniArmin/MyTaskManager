using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Core.MyTaskManager.Users.App.Domain.Core;
using App.Domain.Core.MyTaskManager.Users.DTOs;
using App.Domain.Core.MyTaskManager.Users.Entities;
using Microsoft.AspNetCore.Identity;


namespace MyTaskManagerAppService.MyTaskManager.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserAppService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Login(string UserName, string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(UserName, Password, true, false);
            return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> Register(User user, string Password, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(user, Password);
            return result.Succeeded ? IdentityResult.Success :IdentityResult.Failed();
        }
    }
}
